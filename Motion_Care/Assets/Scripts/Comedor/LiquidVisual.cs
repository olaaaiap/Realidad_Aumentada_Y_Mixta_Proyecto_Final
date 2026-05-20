using UnityEngine;

public class LiquidVisual : MonoBehaviour
{
    [Header("Referencia al sistema de llenado")]
    public CupVolume cup;

    [Header("Objeto visual del líquido")]
    public Transform liquidMesh;

    [Header("Altura del líquido dentro del vaso")]
    public float minY = 0.23f;
    public float maxY = 0.33f;

    void Update()
    {
        float fill = Mathf.Clamp01(cup.FillPercent);

        Vector3 pos = liquidMesh.localPosition;
        pos.y = Mathf.Lerp(minY, maxY, fill);
        liquidMesh.localPosition = pos;

    }
}