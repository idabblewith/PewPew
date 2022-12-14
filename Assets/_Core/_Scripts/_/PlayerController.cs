using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Cam
    private Camera Cam;

    public float jumpForce = 8f, gravityMod = 2.5f;
    
    public Transform groundCheckPoint;
    public bool isGrounded;
    public LayerMask groundLayers;

    public GameObject bulletImpact;
    // public float timeBetweenShots = .1f;
    private float shotCounter;
    public float muzzleDisplayTime;
    private float muzzleCounter;

    public float maxHeatValue = 10f, /*heatPerShot = 1f,*/ coolRate = 4f, overheatCoolRate = 5f;
    private float heatCounter;
    private bool hasOverheated;

    public Gun[] allGuns;
    private int selectedGun;

    // Mouse Var
    public Transform viewPoint;
    public float mouseSensitivity = 5f;
    private float verticalRotationStore;
    private Vector2 mouseInput;

    // Keyboard Var
    public float moveSpeed = 5f, runSpeed = 8f;
    private float activeMoveSpeed;
    private Vector3 moveDir, movement;
    
    public CharacterController charCont;

    private void Awake() {
        charCont = this.GetComponent<CharacterController>();
    }

    void Start()
    {
        // Framerate
        Application.targetFrameRate = 120;

        Cam = Camera.main;
        UIController.instance.weaponTempSlider.maxValue = maxHeatValue;

        Cursor.lockState = CursorLockMode.Locked;
        SwitchGun();
    }

    void Update()
    {
        // Rotation
            // Set mouse Input from InputManager and multiply by sensitivity
            mouseInput = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y")) 
                * mouseSensitivity;

            // Set rotation for horizontal
            transform.rotation = Quaternion.Euler(
                transform.rotation.eulerAngles.x, 
                transform.rotation.eulerAngles.y + mouseInput.x, 
                transform.rotation.eulerAngles.z
            );

            // Assists with clamping
            verticalRotationStore += mouseInput.y;
            verticalRotationStore = Mathf.Clamp(verticalRotationStore, -60f, 60f);

            // Set rotation for vertical
            viewPoint.rotation = Quaternion.Euler(
                -verticalRotationStore, 
                viewPoint.rotation.eulerAngles.y,
                viewPoint.rotation.eulerAngles.z
            );
    
        // Movement
            // Set to Input Manager Hor & Ver
            moveDir = new Vector3(
                Input.GetAxisRaw("Horizontal"), 
                0, 
                Input.GetAxisRaw("Vertical")
            );

            // Sprint
            if (Input.GetKey(KeyCode.LeftShift))
            {
                activeMoveSpeed = runSpeed;
            }
            else 
            {
                activeMoveSpeed = moveSpeed;
            }

            // Gravity 1
            float yVel = movement.y;

            // Utilizes players Forward (so does not just move statically, but based on rotation)
            // Prevent speed boost with normalized
            movement = (transform.forward * moveDir.z + transform.right * moveDir.x).normalized * activeMoveSpeed;
            
            // Gravity 2
            movement.y = yVel;
            if (charCont.isGrounded)
            {
                movement.y = 0f;
            }

            // Determine if grounded based on raycast shot downward/checking for groundlayers
            isGrounded = Physics.Raycast(
                groundCheckPoint.position, 
                Vector3.down, 
                0.25f, 
                groundLayers)
            ;

            // Jump
            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                movement.y = jumpForce;
            }

            // Gravity
            movement.y += Physics.gravity.y * Time.deltaTime * gravityMod;




            // Set Pos to calculation of framerate (independent), direction & speed
            // transform.position += movement * moveSpeed * Time.deltaTime;
            charCont.Move(movement * Time.deltaTime);



            // Shoot
            if(allGuns[selectedGun].muzzleFlash.activeInHierarchy)
            {
                muzzleCounter -= Time.deltaTime;
                if (muzzleCounter <= 0)
                {
                    allGuns[selectedGun].muzzleFlash.SetActive(false);
                }
            }

            if (!hasOverheated)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Shoot();
                }

                // Hold down shoot
                if (Input.GetMouseButton(0) && allGuns[selectedGun].isAutomatic)
                {
                    shotCounter -= Time.deltaTime;
                    if (shotCounter <= 0) 
                    {
                        Shoot();
                    }
                }

                heatCounter -= coolRate * Time.deltaTime;
            } 
            else
            {
                heatCounter -= overheatCoolRate * Time.deltaTime;
                if (heatCounter <= 0)
                {
                    hasOverheated = false;
                    UIController.instance.overheatedMSG.gameObject.SetActive(false);
                }
            }

            if (heatCounter < 0)
            {
                heatCounter = 0;
            }
            UIController.instance.weaponTempSlider.value = heatCounter;


            // Weapon Swap
            if(Input.GetAxisRaw("Mouse ScrollWheel") > 0)
            {
                selectedGun++;

                if (selectedGun >= allGuns.Length)
                {
                    selectedGun = 0;
                }
                SwitchGun(); 
            }
            else if(Input.GetAxisRaw("Mouse ScrollWheel") < 0) 
            {
                selectedGun--;

                if (selectedGun < 0)
                {
                    selectedGun = allGuns.Length -1;
                } 
                SwitchGun();
            }


            // Free Mouse 

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Cursor.lockState = CursorLockMode.None;
            }
            else if (Cursor.lockState == CursorLockMode.None) 
            {
                if(Input.GetMouseButtonDown(0))
                {
                    Cursor.lockState = CursorLockMode.Locked;
                }
            }
    }

    private void Shoot()
    {
        // Determines a point halfway between x, halfway between y on camera view to shoot ray from
        Ray ray = Cam.ViewportPointToRay(new Vector3(.5f,.5f,0));
        ray.origin = Cam.transform.position;

        // If raycast hits something, assign that to hit
        if(Physics.Raycast(ray, out RaycastHit hit))
        {
            Debug.Log($"We hit {hit.collider.gameObject.name}");
            GameObject bImp = Instantiate(bulletImpact, hit.point + (hit.normal*0.002f), Quaternion.LookRotation(hit.normal, Vector3.up));
            Destroy(bImp, 4f);
        }

        shotCounter = allGuns[selectedGun].timeBetweenShots;
        heatCounter += allGuns[selectedGun].heatPerShot;
        if(heatCounter >= maxHeatValue)
        {
            heatCounter = maxHeatValue;
            hasOverheated = true;
            UIController.instance.overheatedMSG.gameObject.SetActive(true);
        }

        allGuns[selectedGun].muzzleFlash.SetActive(true);
        muzzleCounter = muzzleDisplayTime;
    }

    // Camera (Late update ensures camera doesn't detach from player view)
    private void LateUpdate()
    {
        Cam.transform.position = viewPoint.position;
        Cam.transform.rotation = viewPoint.rotation;
    }

    private void SwitchGun()
    {
        foreach (Gun g in allGuns)
        {
            g.gameObject.SetActive(false);
        }
        allGuns[selectedGun].gameObject.SetActive(true);
        //Deactivate muzzle when swapping
        allGuns[selectedGun].muzzleFlash.SetActive(false);
    }
}
