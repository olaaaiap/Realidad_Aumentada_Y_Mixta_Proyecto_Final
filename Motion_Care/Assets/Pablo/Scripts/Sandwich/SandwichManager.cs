using System;
using System.Collections.Generic;
using UnityEngine;

public class SandwichManager : MonoBehaviour
{
    // VARIABLES //

    public static SandwichManager instance;
    
    private Stack<ItemController> _sandwich;
    
    
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
        
        _sandwich = new Stack<ItemController>();
    }

    public Stack<ItemController> GetSandWich()
    {
        return this._sandwich;
    }

    public void AddItem(ItemController item)
    {
        _sandwich.Push(item);
    }

    public void RemoveItem()
    {
        _sandwich.Pop();
    }

    public void EmptySandwich()
    {
        _sandwich.Clear();
    }
}
