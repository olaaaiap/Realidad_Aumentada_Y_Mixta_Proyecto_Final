using Oculus.Interaction;
using Oculus.Interaction.HandGrab;
using UnityEngine;

public class TableObject : MonoBehaviour
{
    public string objectID;

    public void LockObject()
    {
        // 1. bloquear interacci�n Meta
        var grab = GetComponent<HandGrabInteractable>();
        if (grab != null)
            grab.enabled = false;


        Transform pourSpace = transform.Find("PourSpace");

        if (pourSpace != null)
        {
            Debug.Log("olaia. poursace encontrado");
            CapsuleCollider capsule = pourSpace.GetComponent<CapsuleCollider>();

            if (capsule != null)
            {
                Debug.Log("olaia. capsule enabled");
                capsule.enabled = true;
            }
        }


        var grabbable = GetComponent<Grabbable>();
        if (grabbable != null)
            grabbable.enabled = false;

        // 2. bloquear f�sica
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.isKinematic = true;
        }
    }
}
