using UnityEngine;

public class RespawnWhenFarComedor : MonoBehaviour
{

    private Vector3 initialPosition;
    private Quaternion initialRotation;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        initialPosition = transform.position;
        initialRotation = transform.rotation;
    }
    
    private void Update()
    {
        if (transform.position.y < -20)
        {
            rb.isKinematic = true;

            transform.position = initialPosition;
            transform.rotation = initialRotation;

            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

            rb.isKinematic = false;
        
        }
    }
}
