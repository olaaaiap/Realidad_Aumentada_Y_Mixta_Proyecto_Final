using System.Collections;
using UnityEngine;

public class BottlePour : MonoBehaviour
{

    public float anguloInicio = 45f;

    [SerializeField] private GameObject dropPrefab;
    [SerializeField] private Transform tipTransform;
    [SerializeField] private AudioSource victoriaAS;

    public CupVolume cup;

    private int dropsTotales = 300;
    private int dropsDerramados = 0;
    private bool aguaTerminada = false;
    private Coroutine pourCoroutine;

   
    void Update()
    {
        //Mirar angulo y echar agua si es mayor que 45 grados
        float angle = Vector3.Angle(transform.up, Vector3.up);
        if (angle > anguloInicio)
        {
            if (pourCoroutine == null)
                pourCoroutine = StartCoroutine(PourRoutine());
        }
        else
        {
            stop();
        }
    }

    IEnumerator PourRoutine()
    {
        float tiempo = 5f;
        float delay = tiempo / dropsTotales;

        while (dropsDerramados < dropsTotales)
        {
            dropsDerramados++;
            var drop = Instantiate(dropPrefab);
            drop.transform.position = tipTransform.position + Random.insideUnitSphere * 0.02f;
            drop.transform.rotation = transform.rotation;

            yield return new WaitForSeconds(delay + Random.Range(-0.01f, 0.01f));
        }

        aguaTerminada = true;
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

    void stop()
    {
        if (pourCoroutine != null)
        {
            StopCoroutine(pourCoroutine);
            pourCoroutine = null;
        }
    }
}