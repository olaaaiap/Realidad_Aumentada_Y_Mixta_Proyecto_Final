using System.Collections;
using UnityEngine;

public class BottlePour : MonoBehaviour
{

    public float startPourAngle = 45f;

    [SerializeField] private GameObject dropPrefab;
    [SerializeField] private Transform tipTransform;
    [SerializeField] private AudioSource victoriaAS;
    public CupVolume cup;

    private int totalDrops = 300;
    private int waterDropped = 0;
    private bool waterFinished = false;
    private Coroutine pourCoroutine;

   
    void Update()
    {
        float angle = Vector3.Angle(transform.up, Vector3.up);

        if (angle > startPourAngle)
        {
            if (pourCoroutine == null)
                pourCoroutine = StartCoroutine(PourRoutine());
        }
        else
        {
            StopPour();
        }
    }

    IEnumerator PourRoutine()
    {
        float duration = 5f;
        float delay = duration / totalDrops;

        while (waterDropped < totalDrops)
        {
            waterDropped++;

            var drop = Instantiate(dropPrefab);
            drop.transform.position = tipTransform.position + Random.insideUnitSphere * 0.02f;
            drop.transform.rotation = transform.rotation;

            yield return new WaitForSeconds(delay + Random.Range(-0.01f, 0.01f));
        }

        waterFinished = true;
        pourCoroutine = null;

        yield return new WaitForSeconds(5f);

        if (DataManager.instance)
        {
            DataManager.instance.SavePouringMetric("pouring", TaskType.pouring, Mathf.RoundToInt(cup.currentVolume));
            DataManager.instance.EndSession();
        }
        else
        {
            Debug.LogError("DataManager no encontrado!");
        }

        yield return new WaitForSeconds(1f);

        victoriaAS.Play();
        FindFirstObjectByType<GeneralGameManager>().NextGame();
    }

    void StopPour()
    {
        if (pourCoroutine != null)
        {
            StopCoroutine(pourCoroutine);
            pourCoroutine = null;
        }
    }
}