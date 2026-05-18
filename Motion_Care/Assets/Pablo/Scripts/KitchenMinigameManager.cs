using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using Random = System.Random;

public class KitchenMinigameManager : MonoBehaviour
{
    // VARIABLES //
    
    // GameManager singleton
    public static KitchenMinigameManager instance;

    // Testing variables
    [SerializeField] private bool useTesting;
    [SerializeField] private List<ItemCategory> testRecipe;
    
    // Sandwich characteristics
    private List<ItemCategory> _recipe;
    private int _nItems = 3;
    
    // Events
    private UnityEvent<bool> _checkRecipeEvent;

    // Metrics
    private ComparisonResult _results;
    
    // Auxilary variables
    private Random _random = new Random();

    
    // METHODS && FUNCTIONS //
    
    private void Awake()
    {
        if (_checkRecipeEvent == null) _checkRecipeEvent = new UnityEvent<bool>();
        
        _recipe = new List<ItemCategory>();

        _results = new ComparisonResult();
        
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Creation of the recipe
        if (useTesting)
        {
            _recipe = testRecipe;
        }
        else
        {
            _recipe.Add(ItemCategory.BREAD);
            
            for (int i = 0; i < _nItems; i++)
            {
                int item = _random.Next(2, 6);

                ItemCategory itemCategory = (ItemCategory) item;
                
                _recipe.Add(itemCategory);
            }
            
            _recipe.Add(ItemCategory.BREAD);
        }
    }
    
    // Function to get the results of each stage
    public ComparisonResult GetResults() { return _results; }

    // Method to be called when the user wants to lock in the final sandwich
    public void CheckRecipe()
    {
        int nCorrect = 0;
        int nWrong = 0;

        Stack<ItemController> sandwich = SandwichManager.instance.GetSandWich();

        int min = Math.Min(sandwich.Count, _recipe.Count);
        
        for (int i = 0; i < min; i++)
        {
            ItemCategory currentItem = sandwich.ElementAt(i).GetItemCategory();
            
            if (_recipe[i] == currentItem)
            {
                nCorrect++;
            }
            else
            {
                nWrong++;
            }
        }
        
        _results.SetCorrect(nCorrect);
        _results.SetWrong(nWrong);

        if (min < _recipe.Count)
        {
            int dif =  _recipe.Count - min;
            _results.SetUnder(dif);
        }

        if (min < sandwich.Count)
        {
            int dif = sandwich.Count - min;
            _results.SetOver(dif);
        }
        
        _results.SaveResultData();
        
        _checkRecipeEvent?.Invoke(nCorrect == _recipe.Count);
        
        SandwichManager.instance.ShowSandwich();
        ShowRecipe();
    }

    // Method to subscribe to the checking event 
    public void SubscribeToRecipeCheck(UnityAction<bool> callback)
    {
        _checkRecipeEvent.AddListener(callback);
    }
    
    // Auxilary method to show the recipe in the console
    public void ShowRecipe()
    {
        string content = "bottom to top: \n";
        
        foreach (ItemCategory item in _recipe)
        {
            content += item.ToString() + "\n";
        }
        
        Debug.Log(content);
    }
}