using System;
using System.Linq;
using Oculus.Interaction;
using Oculus.Interaction.HandGrab;
using Unity.VisualScripting;
using UnityEngine;

public enum ItemType
{
    PLATE,
    
    FOOD,
    
    TRASH
}

public enum ItemCategory
{
    PLATE, BREAD, TOMATO, LETTUCE, HAM, CHEESE,
    
    FISHBONE, SPONGE, GLOVE
}

public class ItemController : MonoBehaviour
{
    // VARIABLES //
    
    [Header("Item Characteristics")]
    [SerializeField] private ItemType type;
    [SerializeField] private ItemCategory category;
    
    [Header("Snap Interactable Reference")]
    [SerializeField] private SnapInteractable snapInteractable;
    
    private bool _hasSnapped;
    
    
    // METHODS && FUNCTIONS //

    private void Awake()
    {
        snapInteractable ??= GetComponent<SnapInteractable>();
        snapInteractable ??= GetComponentInChildren<SnapInteractable>();
        snapInteractable ??= gameObject.AddComponent<SnapInteractable>();

        _hasSnapped = false;
    }

    private void Update()
    {
        if (snapInteractable.InteractorViews.Any() && !_hasSnapped)
        {
            _hasSnapped = true;
            
            SnapInteractor view = snapInteractable.InteractorViews.First() as SnapInteractor;
            ItemController item = view.transform.GetComponentInParent<ItemController>();
            SandwichManager.instance.AddItem(item);
            
            SandwichManager.instance.ShowSandwich();
        } 
        else if (!snapInteractable.InteractorViews.Any() && _hasSnapped)
        {
            _hasSnapped = false;
            
            // Being a stack, we can only remove the last item added, which is the one being removed from the socket
            SandwichManager.instance.RemoveItem();
            
            SandwichManager.instance.ShowSandwich();
        }
    }

    public ItemCategory GetItemCategory()
    {
        return this.category;
    }

    public ItemType GetItemType()
    {
        return this.type;
    }
}
