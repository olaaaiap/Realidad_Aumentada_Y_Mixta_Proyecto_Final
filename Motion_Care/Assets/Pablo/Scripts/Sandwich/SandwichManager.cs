using System;
using System.Collections.Generic;
using UnityEngine;

public class SandwichManager : MonoBehaviour
{
    // VARIABLES //

    public static SandwichManager instance;
    
    private List<ItemController> _sandwich;
    
    
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
        
        _sandwich = new List<ItemController>();
    }

    public List<ItemController> GetSandWich()
    {
        return this._sandwich;
    }

    public void AddItem(ItemController item)
    {
        _sandwich.Add(item);
    }

    public void RemoveItem()
    {
        _sandwich.RemoveAt(_sandwich.Count - 1);
    }

    public void EmptySandwich()
    {
        _sandwich.Clear();
    }

    public void ShowSandwich()
    {
        string content = "bottom to top: \n";
        
        foreach (ItemController item in _sandwich)
        {
            content += item.GetItemCategory().ToString() + "\n";
        }
        
        Debug.Log(content);
    }
}
