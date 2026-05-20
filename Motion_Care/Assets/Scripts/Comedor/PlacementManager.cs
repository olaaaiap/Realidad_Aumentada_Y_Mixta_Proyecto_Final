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

            float distancia =
            Vector3.Distance(obj.transform.position, slot.transform.position);

            float cm = distancia * 100;
            if (distancia < slot.radioMargen)
            {
                if (aciertoAS != null) aciertoAS.Play();

                obj.transform.position = slot.transform.position;

                obj.transform.rotation = slot.transform.rotation;
                obj.BloquearPosicion();
                GameManagerComedor.Instance.CubiertoColocado(cm);
            }

            break;
        }
    }
}