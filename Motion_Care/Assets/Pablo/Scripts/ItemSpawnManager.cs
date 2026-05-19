using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class ItemSpawnManager : MonoBehaviour
{
    // VARIABLES //
    
    public static ItemSpawnManager instance;
    
    [Header("Item Prefabs")]
    [SerializeField] private List<GameObject> itemPrefabs;
    
    private Dictionary<int, GameObject> _itemPrefabDict;
    
    Random _random = new Random();
    
    
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
        
        // Fill the enum to prefab dictionary
        _itemPrefabDict = new Dictionary<int, GameObject>();
        
        foreach (GameObject itemPrefab in itemPrefabs)
        {
            ItemController cont = itemPrefab.GetComponent<ItemController>();
            if (cont != null)
            {
                _itemPrefabDict.Add((int) cont.GetItemCategory(), itemPrefab);
            }
        }
    }
    
    public bool SpawnItem(GameObject prefab, Transform point)
    {
        GameObject instance = Instantiate(prefab, point.position, point.rotation);

        return (!instance == null);
    }

    public bool SpawnManyItems(GameObject prefab, List<Transform> points)
    {
        bool result = true;
        
        foreach (var point in points)
        {
            GameObject instance = Instantiate(prefab, point.position, point.rotation);
            
            if (instance == null && result)
            {
                result = false;
            }
        }

        return result;
    }

    public bool SpawnItemsInOrder(List<ItemCategory> categories, List<Transform> points, bool fill)
    {
        // Copy the points
        List<Transform> pointsToFill = new List<Transform>();
        CopyList(points, pointsToFill);
        
        // Check if there are more points than categories
        if (categories.Count < points.Count)
        {
            // For each category in the list, spawn the corresponding item in a random point and remove that point from the list
            foreach (var category in categories)
            {
                int rand = _random.Next(pointsToFill.Count);
                GameObject instance = Instantiate(_itemPrefabDict[(int) category], pointsToFill[rand].position, pointsToFill[rand].rotation);
                
                pointsToFill.RemoveAt(rand);

                if (instance == null) return false;
            }
            
            // If fill is true, fill the remaining points with random items
            if (fill)
            {
                foreach (var point in pointsToFill)
                {
                    int rand = _random.Next(2, 6);
                    GameObject instance = Instantiate(_itemPrefabDict[rand], point.position, point.rotation);

                    if (instance == null) return false;
                }
            }
        }
        else
        {
            Debug.Log("Warning: Less points than items to spawn. Some items won't be spawned.");

            for (int i = 0; i < pointsToFill.Count; i++)
            {
                GameObject instance = Instantiate(_itemPrefabDict[(int) categories[i]], pointsToFill[i].position, pointsToFill[i].rotation);

                if (instance == null) return false;
            }
        }

        return true;
    }
    
    private void CopyList<T>(List<T> from, List<T> to)
    {
        to.Clear();
        
        foreach (var item in from)
        {
            to.Add(item);
        }
    }
}
