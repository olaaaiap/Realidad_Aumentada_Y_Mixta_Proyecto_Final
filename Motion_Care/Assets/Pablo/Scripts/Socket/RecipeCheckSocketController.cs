using System;
using System.Linq;
using Oculus.Interaction;
using UnityEngine;

[RequireComponent(typeof(SnapInteractable))]
public class RecipeCheckSocketController : MonoBehaviour
{
    // VARIABLES //

    [SerializeField] private SnapInteractable snapInteractable;


    // METHODS && FUNCTIONS //

    private void Awake()
    {
        if (snapInteractable == null)
        {
            snapInteractable = GetComponent<SnapInteractable>();
        }
    }

    private void Update()
    {
        if (snapInteractable.InteractorViews.Any() && !KitchenMinigameManager.instance.HasChecked())
        {
            KitchenMinigameManager.instance.CheckRecipe();
        }
    }
}
