using System.Collections;
using UnityEngine;

public class BlinkEffect : MonoBehaviour
{
    private Material mat;
    private Color blinkColor = new Color(29f / 255f, 29f / 255f, 4f / 255f);

    private void Awake()
    {
        mat = gameObject.GetComponent<Renderer>().material;
        mat.EnableKeyword("_EMISSION");
    }
    public void StartBlink()
    {
        StartCoroutine(BlinkRoutine());
    }
    private IEnumerator BlinkRoutine()
    {
        for (int i = 0; i < 3; i++)
        {
            mat.SetColor("_EmissionColor", blinkColor);
            yield return new WaitForSeconds(0.7f);

            mat.SetColor("_EmissionColor", Color.black);
            yield return new WaitForSeconds(0.5f);
        }
    }
}
