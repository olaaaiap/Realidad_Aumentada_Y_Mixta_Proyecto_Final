using System;
using System.Collections.Generic;
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
    private int _totalCorrect;
    private int _totalWrong;
    
    // Auxilary variables
    private Random _random = new Random();

    
    // METHODS && FUNCTIONS //
    
    private void Awake()
    {
        _recipe = new List<ItemCategory>();
        _totalCorrect = 0;
        _totalWrong = 0;
        
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
        if (_checkRecipeEvent == null) _checkRecipeEvent = new UnityEvent<bool>();

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

    // Method to be called when the user wants to lock in the final sandwich
    public void CheckRecipe(List<ItemCategory> sandwich)
    {
        int nCorrect = 0;
        int nWrong = 0;
        
        {
            for (int i = 0; i < _recipe.Count; i++)
            {
                if (_recipe[i] == sandwich[i])
                {
                    nCorrect++;
                }
                else
                {
                    nWrong++;
                }
            }
        }
        
        _totalCorrect += nCorrect;
        _totalWrong += nWrong;
        
        _checkRecipeEvent?.Invoke(nCorrect == _recipe.Count);
    }

    // Method to subscribe to the checking event 
    public void SubscribeToRecipeCheck(UnityAction<bool> callback)
    {
        _checkRecipeEvent.AddListener(callback);
    }
}
