using System;
using System.Collections.Generic;
using System.Linq;
using Oculus.Interaction;
using UnityEngine;

[RequireComponent(typeof(SnapInteractable))]
public class GrabbableSocketController : MonoBehaviour
{
    // VARIABLES //

    [SerializeField] private SnapInteractable snapInter;
    [SerializeField] private List<MonoBehaviour> grabComponents;

    private bool _isGrabbed = false;


    // METHODS && FUNCTIONS //

    private void Awake()
    {
        if (!snapInter)
        {
            snapInter = GetComponent<SnapInteractable>();
        }
    }

    private void Update()
    {
        if (snapInter.InteractorViews.Any())
        {
            foreach (MonoBehaviour component in grabComponents)
            {
                component.enabled = false;
            }
        }
        else
        {
            foreach (MonoBehaviour component in grabComponents)
            {
                component.enabled = true;
            }
        }
    }

    // // VARIABLES //
    //
    // [SerializeField] private SnapInteractable snapInter;
    // // Mascara de Enumerado
    // [SerializeField] private List<int> validMask;
    // [SerializeField] private bool isExlusive;
    //
    // private bool _isGrabbing = false;
    //
    //
    // // METHODS && FUNCTIONS //
    //
    // private void Awake()
    // {
    //     if (!snapInter)
    //     {
    //         snapInter = GetComponent<SnapInteractable>();
    //     }
    // }
    //
    // private void Update()
    // {
    //     if (!_isGrabbing && snapInter.InteractorViews.Any())
    //     {
    //         SnapInteractor interactor = snapInter.InteractorViews.First() as SnapInteractor;
    //         
    //         ItemController itemCont =  interactor.GetComponent<ItemController>();
    //         
    //         if (itemCont.GetItemClass() == ItemCategory.TRASH)
    //         {
    //             // TODO: Implementar GameManager y llamar a error
    //         }
    //     }
    // }
}
