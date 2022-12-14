using UnityEngine;

// Simple rotate script to ensure game scene is working

public class ObjectRotate : MonoBehaviour
{
    private float speed = 12f;

    private void Update() {
        this.gameObject.transform.Rotate(0, speed * Time.deltaTime, 0);
    }
}