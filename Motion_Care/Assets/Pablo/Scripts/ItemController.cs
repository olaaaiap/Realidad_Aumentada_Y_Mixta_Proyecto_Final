using Oculus.Interaction.HandGrab;
using UnityEngine;

public enum ItemType
{
    GOOD, BAD
}

public enum ItemCategory
{
    PLATE,
    
    FOOD,
    
    TRASH
}

[RequireComponent(typeof(HandGrabInteractable))]
public class ItemController : MonoBehaviour
{
    // VARIABLES //
    
    [SerializeField] private ItemType type;
    [SerializeField] private ItemCategory category;
    [SerializeField] private HandGrabInteractable grabInteractable;

    private ItemType _nextItem;
    
    
    // METHODS && FUNCTIONS //

    private void Awake()
    {
        if (!grabInteractable)
        {
            grabInteractable = GetComponent<HandGrabInteractable>();
        }
    }
    
    public ItemCategory GetItemClass()
    {
        return this.category;
    }

    public ItemType GetItemType()
    {
        return this.type;
    }
    
    public void SetNextItem(ItemType nextItem)
    {
        _nextItem = nextItem;
    }

    private void ToggleGrab()
    {
        bool isEnabled = grabInteractable.isActiveAndEnabled;
        
        grabInteractable.enabled = !isEnabled;
    }
}
