using Oculus.Interaction;
using Oculus.Interaction.HandGrab;
using UnityEngine;

public class ReleaseDetector : MonoBehaviour
{
    HandGrabInteractable interactable;

    bool previousState;

    TableObject tableObj;

    void Start()
    {
        interactable =
            GetComponent<HandGrabInteractable>();

        tableObj =
            GetComponent<TableObject>();
    }

    void Update()
    {
        bool currentState =
            interactable.State ==
            InteractableState.Select;

        if (previousState && !currentState)
        {
            Released();
        }

        previousState = currentState;
    }

    void Released()
    {
        Debug.Log("Objeto soltado");

        PlacementManager.Instance.CheckPlacement(tableObj);
    }

    
}