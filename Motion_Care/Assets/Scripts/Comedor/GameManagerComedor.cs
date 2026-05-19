using DG.Tweening;
using UnityEngine;
using UnityEngine.Audio;
using static OVRPlugin;

public class GameManagerComedor : MonoBehaviour
{
    [SerializeField] private AudioSource presentationAS;
    [SerializeField] private AudioSource prueba2AS;
    [SerializeField] private AudioSource prueba3AS;
    [SerializeField] private AudioSource prueba4AS;
    [SerializeField] private int totalCantCubiertos = 6;
    [SerializeField] private GameObject dificultadFacil;
    [SerializeField] private GameObject dificultadMedia;
    [SerializeField] private GameObject dificultadDificil;
    [SerializeField] private GameObject botella;


    public static GameManagerComedor Instance;


    private int cantCubiertosColocados = 0;
    private int pruebaActual = 1; //1, 2, 3, 4

    void Awake()
    {
        Instance = this;
    }


    void Start()
    {
        if (presentationAS != null)
        {
            presentationAS.Play();
        }

        DataManager.instance.StartSession();
        ChronometerManager.instance.StartTimer();

    }


    public void CubiertoColocado()
    {
        cantCubiertosColocados++;
        if (cantCubiertosColocados == totalCantCubiertos)
        {
            cantCubiertosColocados = 0;

            switch (pruebaActual) {
                case 1:
                    if (presentationAS != null && presentationAS.isPlaying) presentationAS.Stop();
                    if (prueba2AS != null)  prueba2AS.Play();
                    pruebaActual = 2;
                    
                    dificultadFacil.transform.DOScale(0f, 1f).OnComplete(() =>
                    {
                        dificultadFacil.gameObject.SetActive(false);
                    });

                    dificultadMedia.SetActive(true);

                    Deviation deviation = new Deviation(47, 47);
                    DataManager.instance.SaveMovementMetric("cubiertoFacil", TaskType.memory, deviation);
                    ChronometerManager.instance.StartTimer();

                    break;
                case 2:
                    if (prueba2AS != null && prueba2AS.isPlaying) prueba2AS.Stop();
                    if (prueba3AS != null) prueba3AS.Play();
                    pruebaActual = 3;
                    dificultadMedia.transform.DOScale(0f, 1f).OnComplete(() =>
                    {
                        dificultadMedia.gameObject.SetActive(false);
                    });
                    
                    dificultadDificil.SetActive(true);

                    Deviation deviation2 = new Deviation(47, 47);
                    DataManager.instance.SaveMovementMetric("cubiertoMedio", TaskType.memory, deviation2);
                    ChronometerManager.instance.StartTimer();

                    break;
                case 3:
                    if (prueba3AS != null && prueba3AS.isPlaying) prueba3AS.Stop();
                    if (prueba4AS != null) prueba4AS.Play();
                    pruebaActual = 4;
                    botella.SetActive(true);

                    Deviation deviation3 = new Deviation(47, 47);
                    DataManager.instance.SaveMovementMetric("cubiertoDificil", TaskType.memory, deviation3);
                    ChronometerManager.instance.StartTimer();

                    break;
                case 4:

                    DataManager.instance.SavePouringMetric("pouring", TaskType.pouring, 47);
                    DataManager.instance.EndSession();

                    FindFirstObjectByType<GeneralGameManager>().NextGame();
                    break;
            }
        }
    }
}
