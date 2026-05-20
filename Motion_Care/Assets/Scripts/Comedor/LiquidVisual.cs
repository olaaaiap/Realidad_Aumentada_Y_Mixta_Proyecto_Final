using UnityEngine;

public class LiquidVisual : MonoBehaviour
{
    public CupVolume cup;

    public Transform liquidMesh;

    public float minY = 0.23f;
    public float maxY = 0.33f;

    void Update()
    {
        float llenoPorcentaje = Mathf.Clamp01(cup.FillPercent);

        Vector3 pos = liquidMesh.localPosition;
        pos.y = Mathf.Lerp(minY, maxY, llenoPorcentaje);
        liquidMesh.localPosition = pos;

    }
}