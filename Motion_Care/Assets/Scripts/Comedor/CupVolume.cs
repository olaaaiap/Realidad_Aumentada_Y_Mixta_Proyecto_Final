using UnityEngine;

public class CupVolume : MonoBehaviour
{
    public int insideCount = 0;
    public int outsideCount = 0;

    public float dropVolume = 1f;
    public float currentVolume = 0f;
    public float cupCapacity = 300f;

    public float FillPercent => currentVolume / cupCapacity;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Drop")) return;
        Drop drop = other.GetComponent<Drop>();
        if (drop == null || drop.contado) return;
        drop.contado = true;
        insideCount++;
        currentVolume += dropVolume;
        Destroy(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Drop")) return;
        outsideCount++;
    }
}