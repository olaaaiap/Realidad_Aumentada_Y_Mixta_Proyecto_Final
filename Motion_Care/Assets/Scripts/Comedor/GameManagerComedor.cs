using UnityEngine;
using UnityEngine.Audio;

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
    }

    public void CubiertoColocado()
    {
        cantCubiertosColocados++;
        if (cantCubiertosColocados == totalCantCubiertos)
        {
            cantCubiertosColocados = 0;

            switch (pruebaActual) {
                case 1:
                    Debug.Log("iniciando prueba 2");
                    if (presentationAS != null && presentationAS.isPlaying) presentationAS.Stop();
                    if (prueba2AS != null)  prueba2AS.Play();
                    pruebaActual = 2;
                    dificultadFacil.SetActive(false);
                    dificultadMedia.SetActive(true);
                    break;
                case 2:
                    Debug.Log("iniciando prueba 3");
                    if (prueba2AS != null && prueba2AS.isPlaying) prueba2AS.Stop();
                    if (prueba3AS != null) prueba3AS.Play();
                    pruebaActual = 3;
                    dificultadMedia.SetActive(false);
                    dificultadDificil.SetActive(true);
                    break;
                case 3:
                    Debug.Log("iniciando prueba 4");
                    if (prueba3AS != null && prueba3AS.isPlaying) prueba3AS.Stop();
                    if (prueba4AS != null) prueba4AS.Play();
                    pruebaActual = 4;
                    botella.SetActive(true);
                    break;
            }
        }
    }
}
