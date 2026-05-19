using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using Random = System.Random;

public enum Difficulty
{
    EASY,
    MEDIUM,
    HARD
}

public class KitchenMinigameManager : MonoBehaviour
{
    // VARIABLES //
    
    // GameManager singleton
    public static KitchenMinigameManager instance;

    // Testing variables
    [Header("Testing")]
    [SerializeField] private bool useTesting;
    [SerializeField] private List<ItemCategory> testRecipe;
    private Stack<ItemCategory> _testRecipe;
    
    // Level design
    [Header("Level Design")] 
    [SerializeField] private int nLevels;
    [SerializeField] private int playDelay;
    [SerializeField] private Difficulty startDifficulty;
    
    // Sandwich characteristics
    [SerializeField] public Dictionary<ItemCategory, string> itemNames;
    private List<ItemCategory> _recipe;
    [SerializeField] private List<int> nLevelItems;
    
    // Events
    private UnityEvent<List<ItemCategory>> _startLevelEvent;
    private UnityEvent<ComparisonResult> _checkRecipeEvent;
    private bool _hasStarted;
    private bool _hasChecked;

    // Metrics
    private ComparisonResult _results;
    
    // Auxilary variables
    private Random _random = new Random();

    
    // GETTERS && SETTERS //
    public bool HasStarted() { return _hasStarted; }
    public bool HasChecked() { return _hasChecked; }
    public List<ItemCategory> GetRecipe() { return _recipe; }
    public ComparisonResult GetResults() { return _results; }
    
    
    // METHODS && FUNCTIONS //
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        
        itemNames = new Dictionary<ItemCategory, string>();
        itemNames[ItemCategory.BREAD] = "Pan";
        itemNames[ItemCategory.HAM] = "Jamón";
        itemNames[ItemCategory.CHEESE] = "Queso";
        itemNames[ItemCategory.LETTUCE] = "Lechuga";
        itemNames[ItemCategory.TOMATO] = "Tomate";
        
        _recipe = new List<ItemCategory>();

        _results = new ComparisonResult();
        
        if (_checkRecipeEvent == null) _checkRecipeEvent = new UnityEvent<ComparisonResult>();
        if (_startLevelEvent == null) _startLevelEvent = new UnityEvent<List<ItemCategory>>();

        _hasStarted = false;
        _hasChecked = false;
    }
    
    // Function that initializes each level
    private void InitRecipe()
    {
        // Creation of the recipe and call to event listeners
        if (useTesting)
        {
            _recipe = testRecipe;
        }
        else
        {
            _recipe.Add(ItemCategory.BREAD);
            
            for (int i = 0; i < nLevelItems[0]; i++)
            {
                int item = _random.Next(2, 6);

                ItemCategory itemCategory = (ItemCategory) item;
                
                _recipe.Add(itemCategory);
            }
            
            _recipe.Add(ItemCategory.BREAD);
        }
    }
    
    public void StartLevel()
    {
        _hasChecked = false;
        _hasStarted = true;

        _results.Reset();
        
        InitRecipe();
        
        _startLevelEvent.Invoke(_recipe);
        
        // TODO: position items on the table (coroutine with delay?)
    }

    // Method to be called when the user wants to lock in the final sandwich
    public void CheckRecipe()
    {
        _hasStarted = false;
        _hasChecked = true;
        
        int nCorrect = 0;
        int nWrong = 0;

        Stack<ItemController> sandwich = SandwichManager.instance.GetSandWich();

        int min = Math.Min(sandwich.Count, _recipe.Count);
        
        for (int i = 0; i < min; i++)
        {
            ItemCategory currentItem = sandwich.ElementAt((min - 1) - i).GetItemCategory();     // The stack must be read from top to bottom, so we need to reverse the order of access
            
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
        
        _checkRecipeEvent?.Invoke(_results);
        
        // SandwichManager.instance.ShowSandwich();
        // ShowRecipe();
    }

    // Methods to subscribe/unsubscribe to the events 
    public void SubscribeToStartLevel(UnityAction<List<ItemCategory>> callback)
    {
        _startLevelEvent.AddListener(callback);
    }
    
    public void UnsubscribeToStartLevel(UnityAction<List<ItemCategory>> callback)
    {
        _startLevelEvent.RemoveListener(callback);
    }
    
    public void SubscribeToRecipeCheck(UnityAction<ComparisonResult> callback)
    {
        _checkRecipeEvent.AddListener(callback);
    }

    public void UnsubscribeToRecipeCheck(UnityAction<ComparisonResult> callback)
    {
        _checkRecipeEvent.RemoveListener(callback);
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