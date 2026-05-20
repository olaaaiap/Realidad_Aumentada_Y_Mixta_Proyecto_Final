using UnityEngine;

public class BottlePour : MonoBehaviour
{
    public ParticleSystem waterFX;

    public float startPourAngle = 45f;

    void Update()
    {
        float angle =
            Vector3.Angle(
                transform.up,
                Vector3.up);

        if (angle > startPourAngle)
        {
            StartPour();
        }
        else
        {
            StopPour();
        }
    }

    void StartPour()
    {
        var emission =
            waterFX.emission;

        emission.rateOverTime = 80;
    }

    void StopPour()
    {
        var emission =
            waterFX.emission;

        emission.rateOverTime = 0;
    }
}