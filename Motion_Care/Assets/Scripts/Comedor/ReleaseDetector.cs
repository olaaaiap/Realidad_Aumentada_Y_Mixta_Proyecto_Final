using Oculus.Interaction;
using Oculus.Interaction.HandGrab;
using UnityEngine;

public class ReleaseDetector : MonoBehaviour
{
    HandGrabInteractable interactable;

    bool estadoAnterior;

    TableObject tableObj;

    void Start()
    {
        interactable = GetComponent<HandGrabInteractable>();
        tableObj = GetComponent<TableObject>();
    }

    void Update()
    {
        bool currentState = interactable.State == InteractableState.Select;

        if (estadoAnterior && !currentState)
        {
            ObjetoSoltado();
        }

        estadoAnterior = currentState;
    }

    void ObjetoSoltado()
    {
        PlacementManager.Instance.CheckPlacement(tableObj);
    }

    
}