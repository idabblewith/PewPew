using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField] InputReader InputReader;
    private float m_MouseX, m_MouseY;
    [SerializeField] float m_MouseSensitivity = 0.5f;

    private Camera m_Cam;
    [SerializeField] public Transform m_ViewPoint;
    [SerializeField] float m_XClamp = 85f;
    float m_VerticalRotStore;
    float m_XRotation = 0f;

    private void OnEnable() 
    {
        InputReader.OnMouseMovement += OnMouseMove;
        m_MouseSensitivity = 0.5f; // Check Player Prefs later
    }

    private void OnDisable() 
    {
        InputReader.OnMouseMovement -= OnMouseMove;
    }

    private void Start() 
    {    
        m_Cam = Camera.main;
    }

    private void Update() 
    {
        Vector2 mouseInput = new Vector2(m_MouseX, m_MouseY) * m_MouseSensitivity;
        transform.rotation = Quaternion.Euler(
            transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + m_MouseX, transform.rotation.eulerAngles.z);

        m_VerticalRotStore += mouseInput.y;
        m_VerticalRotStore = Mathf.Clamp(m_VerticalRotStore, -60f, 60f);
        m_ViewPoint.rotation = Quaternion.Euler(-m_VerticalRotStore, m_ViewPoint.rotation.eulerAngles.y, m_ViewPoint.rotation.eulerAngles.z);
    }

    private void LateUpdate()
    {
        m_Cam.transform.position = m_ViewPoint.position;
        m_Cam.transform.rotation = m_ViewPoint.rotation;
    }

    public void OnMouseMove(Vector2 _MouseMovement)
    {
        m_MouseX = _MouseMovement.x;
        m_MouseY = _MouseMovement.y;
    }
}
