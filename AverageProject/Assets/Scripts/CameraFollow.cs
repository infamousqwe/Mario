using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float smoothSpeed = 0.125f;
    public float yOffset = 1f; 
    private Vector3 initialPosition;
    private Transform target;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        initialPosition = transform.position; 
    }

    private void LateUpdate()
    {
        if (target == null) return;

     
        float desiredX = target.position.x;
        Vector3 desiredPosition = new Vector3(desiredX, initialPosition.y + yOffset, initialPosition.z);
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
}