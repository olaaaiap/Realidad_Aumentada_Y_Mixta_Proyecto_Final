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

    // private void Awake()
    // {
    //     correctFeedback.SetActive(false);
    //     wrongFeedback.SetActive(false);
    // }

    private void Start()
    {
        KitchenMinigameManager.instance.SubscribeToStartLevel(ShowRecipie);
        KitchenMinigameManager.instance.SubscribeToRecipeCheck(ShowResults);
    }

    private void OnDestroy()
    {
        KitchenMinigameManager.instance.UnsubscribeToStartLevel(ShowRecipie);
        KitchenMinigameManager.instance.UnsubscribeToRecipeCheck(ShowResults);
    }

    public void ShowRecipie(List<ItemCategory> recipe)
    {
        for (int i = 0; i < recipe.Count; i++)
        {
            recipeTexts[i].text = KitchenMinigameManager.instance.itemNames[recipe[i]];
        }

        resultText.text = "";
    }
    
    private void ShowResults(ComparisonResult result)
    {
        ShowRecipie(KitchenMinigameManager.instance.GetRecipe());
        
        resultText.text = $"Aciertos: {result.GetCorrect()} \n\nFallos: {result.GetTotalWrong()} \n\nPuntuación: {result.ComputeScore()}";
    }
}
