using Oculus.Interaction.HandGrab;
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
    
    [SerializeField] private ItemType type;
    [SerializeField] private ItemCategory category;

    private ItemType _nextItem;
    
    
    // METHODS && FUNCTIONS //
    
    public ItemCategory GetItemCategory()
    {
        return this.category;
    }

    public ItemType GetItemType()
    {
        return this.type;
    }
}
