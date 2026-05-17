using UnityEngine;

public class PlacementManager : MonoBehaviour
{
    public static PlacementManager Instance;

    void Awake()
    {
        Instance = this;
    }

    public void CheckPlacement(TableObject obj)
    {
        PlacementSlot[] slots = FindObjectsByType<PlacementSlot>(FindObjectsSortMode.None);
        foreach (PlacementSlot slot in slots)
        {
            if (slot.objectID != obj.objectID)
                continue;

            float distance =
            Vector3.Distance(
                obj.transform.position,
                slot.transform.position);

            float cm = distance * 100;

            Debug.Log(
                obj.objectID +
                " desviación: " +
                cm +
                " cm"
            );

            if (distance < slot.radioMargen)
            {
                Debug.Log("Correcto");
                obj.transform.position = slot.transform.position;

                obj.transform.rotation = slot.transform.rotation;
                obj.LockObject();
            }
            else
            {
                Debug.Log("Demasiado lejos");
            }

            break;
        }
    }
}