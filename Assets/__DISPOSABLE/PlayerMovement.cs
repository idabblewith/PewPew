using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private InputReader InputReader;
    [SerializeField] private CharacterController Controller;
    [SerializeField] private GameObject m_ModelContainer;

    // Movement
    private Vector3 movement;
    private Vector2 m_MovementInput;
    [SerializeField] private float m_BaseMoveSpeed = 11f;
    [SerializeField] private float m_RunSpeed = 19f;
    [SerializeField] private float m_ActiveMoveSpeed;

    // Jump, GroundCheck & Gravity
    [SerializeField] private LayerMask m_GroundMask;
    public Transform GroundCheckPoint;
    private bool m_IsGrounded;

    [SerializeField] private float m_JumpHeight = 12f;
    private bool m_Jump;
    private float m_VerticalVelocity;
    private float m_Gravity = Physics.gravity.y;
    private float m_GravityMod = 2.5f;


    private void OnEnable() 
    {
        InputReader.Movement += OnMovementInput;
        InputReader.JumpEvent += OnJump;
        InputReader.SprintEvent += OnSprint;
        InputReader.SprintReleasedEvent += OnSprintReleased;
        InputReader.paused += OnPause;
        InputReader.unpaused += OnUnpause;
    }

    private void OnDisable() 
    {
        InputReader.Movement -= OnMovementInput;
        InputReader.JumpEvent -= OnJump;
        InputReader.SprintEvent -= OnSprint;
        InputReader.SprintReleasedEvent -= OnSprintReleased;
        InputReader.paused -= OnPause;
        InputReader.unpaused -= OnUnpause;
    }

    private void Awake()
    {
        m_ModelContainer = FindGameObjectInChildWithTag(this.gameObject, "ModelContainer");
        m_ActiveMoveSpeed = m_BaseMoveSpeed;
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        InputReader.WhatIsEnabled();
    }

    private void Update() {
        // Moving
        HandleMovement();
 
    }


    #region Movement -------------------------------------------------------------------
    private void OnMovementInput(Vector2 _MovementInput)
    {
        m_MovementInput = _MovementInput;
    }

    private void HandleMovement()
    {
        // Falling
        m_VerticalVelocity = movement.y;
        
        // Base Movement
        Vector3 MoveDir = new Vector3(m_MovementInput.x, 0, m_MovementInput.y);
        movement = (
            (transform.forward * MoveDir.z) //y input
            + (transform.right * MoveDir.x)).normalized * m_ActiveMoveSpeed; //x input

        // Falling
        movement.y = m_VerticalVelocity;
        // Ground check velocity
        HanldeGroundVelocity();


        // Jumping
        HandleJump();

        // Falling
        movement.y += m_Gravity * Time.deltaTime * m_GravityMod;

        Controller.Move((movement) * Time.deltaTime);
    }

    private void OnSprint()
    {
        m_ActiveMoveSpeed = m_RunSpeed;
    }

    private void OnSprintReleased()
    {
        m_ActiveMoveSpeed = m_BaseMoveSpeed;
    }
    #endregion


    #region Pausing -------------------------------------------------------------------
    private void OnPause()
    {
        Cursor.lockState = CursorLockMode.None;
    }

    private void OnUnpause()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    #endregion


    #region Jumping/Falling ================================================================
    private void OnJump()
    {
        Debug.Log(m_IsGrounded);
        m_Jump = true;
        Debug.Log(m_IsGrounded);
    }

    private void HandleJump()
    {
        m_IsGrounded = Physics.Raycast(GroundCheckPoint.position, Vector3.down, 0.25f, m_GroundMask); 
        //pos of ground check point on model, direction of ray, distance isGrounded becomes true, layermask of ground 
        if (m_Jump)
        {
            if (m_IsGrounded)
            {
                movement.y = m_JumpHeight;
            }
            m_Jump = false;
        }
    }



    private void HanldeGroundVelocity()
    {
        if (Controller.isGrounded)
        {
            movement.y = 0f;
        }
    }
    #endregion


    #region Helper -----------------------------------------------
    public static GameObject FindGameObjectInChildWithTag (GameObject parent, string tag)
    {
        Transform t = parent.transform;

        for (int i = 0; i < t.childCount; i++) 
        {
            if(t.GetChild(i).gameObject.tag == tag)
            {
                return t.GetChild(i).gameObject;
            }
                
        }
            
        return null;
    }


    #endregion
}
