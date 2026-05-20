using System.Linq;
using Oculus.Interaction;
using UnityEngine;

[RequireComponent(typeof(SnapInteractable))]
public class StartRoundSocketController : MonoBehaviour
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
        if (snapInteractable.InteractorViews.Any() && !KitchenMinigameManager.instance.HasStarted() && !KitchenMinigameManager.instance.HasEnded())
        {
            KitchenMinigameManager.instance.StartRoundCoroutine();
        }
    }
}
