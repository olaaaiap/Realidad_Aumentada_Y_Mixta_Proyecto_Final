using Oculus.Interaction;
using Oculus.Interaction.HandGrab;
using UnityEngine;

public class TableObject : MonoBehaviour
{
    public string objectID;

    public void LockObject()
    {
        // 1. bloquear interacción Meta
        var grab = GetComponent<HandGrabInteractable>();
        if (grab != null)
            grab.enabled = false;

        var grabbable = GetComponent<Grabbable>();
        if (grabbable != null)
            grabbable.enabled = false;

        // 2. bloquear física
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.isKinematic = true;
        }
    }
}
