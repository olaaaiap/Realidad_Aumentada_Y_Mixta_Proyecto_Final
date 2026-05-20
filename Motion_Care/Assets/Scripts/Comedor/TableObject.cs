using Oculus.Interaction;
using Oculus.Interaction.HandGrab;
using UnityEngine;

public class TableObject : MonoBehaviour
{
    public string objectID;

    public void BloquearPosicion()
    {
        var grab = GetComponent<HandGrabInteractable>();
        if (grab != null) grab.enabled = false;

        Transform pourSpace = transform.Find("PourSpace");

        if (pourSpace != null)
        {
            CapsuleCollider capsuleCollider = pourSpace.GetComponent<CapsuleCollider>();

            if (capsuleCollider != null)
            {
                capsuleCollider.enabled = true;
            }
        }


        var grabbable = GetComponent<Grabbable>();
        if (grabbable != null) grabbable.enabled = false;

        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.isKinematic = true;
        }
    }
}
