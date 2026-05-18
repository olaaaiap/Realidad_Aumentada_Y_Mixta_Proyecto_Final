using UnityEngine;

public class PlacementManager : MonoBehaviour
{
    public static PlacementManager Instance;
    [SerializeField] private AudioSource aciertoAS;
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
                if (aciertoAS != null) aciertoAS.Play();

                obj.transform.position = slot.transform.position;

                obj.transform.rotation = slot.transform.rotation;
                obj.LockObject();
                GameManagerComedor.Instance.CubiertoColocado();
            }
            else
            {
                Debug.Log("Demasiado lejos");
            }

            break;
        }
    }
}