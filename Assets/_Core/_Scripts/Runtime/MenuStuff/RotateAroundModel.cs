using UnityEngine;

public class RotateAroundModel : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] private float rotateSpeed = 20.0f;
    private Vector3 point;//the coord to the point where the camera looks at
    private Vector3 newPos;

    void Start () 
    {
        newPos = new Vector3(target.transform.position.x, target.transform.position.y + 2, target.transform.position.z);
        point = newPos;
        transform.LookAt(point);
    }

    void Update () 
    {
        transform.RotateAround(
            newPos, Vector3.up, rotateSpeed * Time.deltaTime
        );  
    }
}
