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
}
