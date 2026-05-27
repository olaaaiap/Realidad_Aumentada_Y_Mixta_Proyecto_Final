using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FeedbackController : MonoBehaviour
{
    // VARIABLES //
    
    [SerializeField] private GameObject correctFeedback;
    [SerializeField] private GameObject wrongFeedback;
    
    [Header("Recipe Feedback")]
    [SerializeField] private List<TextMeshProUGUI> recipeTexts;
    
    [Header("Results Feedback")]
    [SerializeField] private TextMeshProUGUI resultText;
    
    
    // METHODS && FUNCTIONS //

    private void Start()
    {
        KitchenMinigameManager.instance.SubscribeToStartRound(StartShowRecipeForTime);
        KitchenMinigameManager.instance.SubscribeToRecipeCheck(ShowResults);
    }

    private void OnDestroy()
    {
        KitchenMinigameManager.instance.UnsubscribeToStartRound(StartShowRecipeForTime);
        KitchenMinigameManager.instance.UnsubscribeToRecipeCheck(ShowResults);
    }

    private void ShowRecipie(List<ItemCategory> recipe)
    {
        for (int i = 0; i < recipe.Count; i++)
        {
            recipeTexts[i].text = $"{i}. {KitchenMinigameManager.instance.itemNames[recipe[i]]}";
        }
    }

    private void StartShowRecipeForTime(List<ItemCategory> recipe, int time)
    {
        StartCoroutine(ShowRecipeForTimeCoroutine(recipe, time));
    }

    private IEnumerator ShowRecipeForTimeCoroutine(List<ItemCategory> recipe, int time)
    {
        // Show the recipe
        for (int i = 0; i < recipe.Count; i++)
        {
            recipeTexts[i].text = $"{i}. {KitchenMinigameManager.instance.itemNames[recipe[i]]}";
        }
        
        resultText.text = "";
        
        // Wait for the time
        yield return new WaitForSeconds(time);
        
        // Hide the recipe
        for (int i = 0; i < recipe.Count; i++)
        {
            recipeTexts[i].text = "";
        }
    }
    
    private void ShowResults(ComparisonResult result)
    {
        ShowRecipie(KitchenMinigameManager.instance.GetRecipe());
        
        resultText.text = $"Aciertos: {result.GetCorrect()} " +
                          $"\n\nFallos: {result.GetTotalWrong()} " +
                          $"\n\nPuntuación: \n {result.ComputeScore(KitchenMinigameManager.instance.GetCorrectPoints(), KitchenMinigameManager.instance.GetWrongPoints(), KitchenMinigameManager.instance.GetOverPoints(), KitchenMinigameManager.instance.GetUnderPoints())}" +
                          $" / {KitchenMinigameManager.instance.GetRecipe().Count * 10}";
    }
}
