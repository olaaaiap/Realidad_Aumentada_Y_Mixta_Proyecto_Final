using UnityEngine;

public class CupVolume : MonoBehaviour
{
    [Header("Contadores")]
    public int insideCount = 0;
    public int outsideCount = 0;

    [Header("Llenado")]
    public float dropVolume = 1f;
    public float currentVolume = 0f;
    public float cupCapacity = 300f;

    public float FillPercent => currentVolume / cupCapacity;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Drop")) return;
        Drop drop = other.GetComponent<Drop>();
        if (drop == null || drop.counted) return;

        Debug.Log("olaia. OnTriggerEnter");
        drop.counted = true;

        insideCount++;
        currentVolume += dropVolume;
        Destroy(other.gameObject);
        //PoolManager.instance.Despawn(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Drop")) return;

        outsideCount++;
    }
}