using System;
using System.Linq;
using Oculus.Interaction;
using UnityEngine;

[RequireComponent(typeof(SnapInteractable))]
public class RecipeCheckSocketController : MonoBehaviour
{
    // VARIABLES //

    [Header("Snap Interactable")]
    [SerializeField] private SnapInteractable snapInteractable;
    
    [Header("SFX")]
    [SerializeField] private int audioSourceIndex;
    [SerializeField] private string clipName;


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
            MinigameAudioManager.instance.PlaySpatialClip(clipName, audioSourceIndex);
            KitchenMinigameManager.instance.CheckRecipe();
        }
    }
}
