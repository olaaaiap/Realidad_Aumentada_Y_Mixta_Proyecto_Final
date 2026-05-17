using System.Collections;
using UnityEngine;

public class FeedbackController : MonoBehaviour
{
    // VARIABLES //
    
    [SerializeField] private GameObject correctFeedback;
    [SerializeField] private GameObject wrongFeedback;
    
    
    // METHODS && FUNCTIONS //

    private void Awake()
    {
        correctFeedback.SetActive(false);
        wrongFeedback.SetActive(false);
        
        KitchenMinigameManager.instance.SubscribeToRecipeCheck(ShowFeedback);
    }
    
    private void ShowFeedback(bool isCorrect)
    {
        StopAllCoroutines();
        StartCoroutine(FeedbackCoroutine(isCorrect));
    }

    private IEnumerator FeedbackCoroutine(bool isCorrect)
    {
        if (isCorrect)
        {
            correctFeedback.SetActive(true);
        }
        else
        {
            wrongFeedback.SetActive(true);
        }
        
        yield return new WaitForSeconds(3.0f);
        
        correctFeedback.SetActive(false);
        wrongFeedback.SetActive(false);
    }
}
