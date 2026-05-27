using System;
using System.Collections;
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

public class KitchenMinigameManager : Singleton<KitchenMinigameManager>
{
    // VARIABLES //

    // Testing variables
    [Header("Testing")]
    [SerializeField] private bool useTesting;
    [SerializeField] private List<ItemCategory> testRecipe;
    private Stack<ItemCategory> _testRecipe;
    
    // Level design
    [Header("Level Design")] 
    [SerializeField] private int nRounds;
    [SerializeField] private List<int> nRoundItems;
    [SerializeField] private int playDelay;
    [SerializeField] private Difficulty startDifficulty;
    [SerializeField] private int timeToPass;
    [SerializeField] private List<Transform> easySpawnPoints;
    [SerializeField] private List<Transform> mediumSpawnPoints;
    [SerializeField] private List<Transform> hardSpawnPoints;
    
    private Difficulty _currentDifficulty;
    private int _currentRound;
    
    // Score
    [Header("Scoring Points")]
    [SerializeField] private int correctPoints;
    [SerializeField] private int wrongPoints;
    [SerializeField] private int overPoints;
    [SerializeField] private int underPoints;
    
    // Sound
    [Header("Audio")]
    [SerializeField] private string introClipName;
    [SerializeField] private string nextRoundClipName;
    [SerializeField] private string endGameClipName;
    
    // Sandwich characteristics
    public Dictionary<ItemCategory, string> itemNames;
    private List<ItemCategory> _recipe;
    
    // Events
    private UnityEvent<List<ItemCategory>, int> _startRoundEvent;
    private UnityEvent<ComparisonResult> _checkRecipeEvent;
    private bool _hasStarted;
    private bool _hasChecked;
    private bool _hasEnded;

    // Metrics
    private ComparisonResult _results;
    private int _levelTime;
    private int _roundStartTime;
    private int _roundEndTime;
    
    // Auxilary variables
    private Random _random = new Random();

    
    // GETTERS && SETTERS //
    public bool HasStarted() { return _hasStarted; }
    public bool HasChecked() { return _hasChecked; }
    public bool HasEnded() { return _hasEnded; }
    public int GetCurrentRound() { return _currentRound; }
    public Difficulty GetCurrentDifficulty() { return _currentDifficulty; }
    public List<ItemCategory> GetRecipe() { return _recipe; }
    public ComparisonResult GetResults() { return _results; }
    public int GetCorrectPoints() { return correctPoints; }
    public int GetWrongPoints() { return wrongPoints; }
    public int GetOverPoints() { return overPoints; }
    public int GetUnderPoints() { return underPoints; }
    
    
    // METHODS && FUNCTIONS //
    
    private void Awake()
    {
        itemNames = new Dictionary<ItemCategory, string>();
        itemNames[ItemCategory.BREAD] = "Pan";
        itemNames[ItemCategory.SALAMI] = "Salami";
        itemNames[ItemCategory.CHEESE] = "Queso";
        itemNames[ItemCategory.LETTUCE] = "Lechuga";
        itemNames[ItemCategory.TOMATO] = "Tomate";
        
        _recipe = new List<ItemCategory>();
        _results = new ComparisonResult();
        
        if (_checkRecipeEvent == null) _checkRecipeEvent = new UnityEvent<ComparisonResult>();
        if (_startRoundEvent == null) _startRoundEvent = new UnityEvent<List<ItemCategory>, int>();

        _hasStarted = false;
        _hasChecked = false;
        _hasEnded = false;
        
        _currentDifficulty = startDifficulty;
        _currentRound = 0;
    }

    private void Start()
    {
        DataManager.instance.StartSession();
        
        MinigameAudioManager.instance.PlayClip(introClipName);
    }
    
    // Function that initializes each round
    private void InitRecipe()
    {
        // Empty the last recipe
        _recipe.Clear();
        
        // Creation of the recipe and call to event listeners
        if (useTesting)
        {
            _recipe = testRecipe;
        }
        else
        {
            _recipe.Add(ItemCategory.BREAD);
            
            for (int i = 0; i < nRoundItems[(int) _currentDifficulty]; i++)
            {
                int item = _random.Next(2, 6);

                ItemCategory itemCategory = (ItemCategory) item;
                
                _recipe.Add(itemCategory);
            }
            
            _recipe.Add(ItemCategory.BREAD);
        }
    }
    
    // Functions to start the round
    public void StartRoundCoroutine()
    {
        MinigameAudioManager.instance.StopAudio();
        StartCoroutine(RoundCoroutine());
    }

    private IEnumerator RoundCoroutine()
    {
        _currentRound++;
        
        _hasChecked = false;
        _hasStarted = true;

        SandwichManager.instance.EmptySandwich();
        _results.Reset();
        
        InitRecipe();
        
        _startRoundEvent.Invoke(_recipe, playDelay);
        yield return new WaitForSeconds(playDelay);

        switch (_currentDifficulty)
        {
            case Difficulty.EASY:
                ItemSpawnManager.instance.SpawnItemsInOrder(_recipe, easySpawnPoints, true);
                break;
            case Difficulty.MEDIUM:
                ItemSpawnManager.instance.SpawnItemsInOrder(_recipe, mediumSpawnPoints, true);
                break;
            case Difficulty.HARD:
                ItemSpawnManager.instance.SpawnItemsInOrder(_recipe, hardSpawnPoints, true);
                break;
        }
        
        ChronometerManager.instance.StartTimer();
    }

    // Method to be called when the user wants to lock in the final sandwich
    public void CheckRecipe()
    {
        _results.SetTime(ChronometerManager.instance.StopTimer());
        Debug.Log($"Time: {_results.GetTime()}");
        
        _hasStarted = false;
        _hasChecked = true;
        
        int nCorrect = 0;
        int nWrong = 0;

        List<ItemController> sandwich = SandwichManager.instance.GetSandWich();

        int min = Math.Min(sandwich.Count, _recipe.Count);
        
        for (int i = 0; i < min; i++)
        {
            ItemCategory currentItem = sandwich[i].GetItemCategory();     // The stack must be read from top to bottom, so we need to reverse the order of access
            
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
        
        CheckDifficultyProgression();
        
        _checkRecipeEvent?.Invoke(_results);

        CheckEndGame();
    }

    // Method to check if the difficulty should be increased for the next round
    private void CheckDifficultyProgression()
    {
        if (_results.GetCorrect() == _recipe.Count && _results.GetTime() <= timeToPass)
        {
            switch (_currentDifficulty)
            {
                case Difficulty.EASY:
                    _currentDifficulty = Difficulty.MEDIUM;
                    break;
                case Difficulty.MEDIUM:
                    _currentDifficulty = Difficulty.HARD;
                    break;
            }
        }
    }
    
    // Method to check if the game has to end
    private void CheckEndGame()
    {
        if (_currentRound == nRounds)
        {
            _hasEnded = true;
            StartCoroutine(EndGame());
        }
        else
        {
            MinigameAudioManager.instance.PlayClip(nextRoundClipName);
        }
    }

    private IEnumerator EndGame()
    {
        MinigameAudioManager.instance.StopAudio();
        MinigameAudioManager.instance.PlayClip(endGameClipName);
        
        yield return new WaitForSeconds(MinigameAudioManager.instance.GetClipLength(endGameClipName));
        
        DataManager.instance.EndSession();
        FindFirstObjectByType<GeneralGameManager>().NextGame();
    }

    // Methods to subscribe/unsubscribe to the events 
    public void SubscribeToStartRound(UnityAction<List<ItemCategory>, int> callback)
    {
        _startRoundEvent.AddListener(callback);
    }
    
    public void UnsubscribeToStartRound(UnityAction<List<ItemCategory>, int> callback)
    {
        _startRoundEvent.RemoveListener(callback);
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