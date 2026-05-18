using System.Collections;
using UnityEngine;

public class ParejasCartasManager : MonoBehaviour
{
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private AudioClip[] audioClips;

    [SerializeField] private GameObject[] cartas;

    public System.Action OnPruebaFinalizada;

    private CardInteractable primeraCarta = null;
    private CardInteractable segundaCarta = null;

    private bool pruebaActiva = false;
    private bool pruebaTerminada = false;

    private int parejasHechas = 0;
    private int totalParejas;

    void Start()
    {
        totalParejas = cartas.Length / 2;
        StartCoroutine(FlujoParejas());
    }

    private IEnumerator FlujoParejas()
    {
        ActivarCartasInicio();

        yield return StartCoroutine(Introduccion());

        pruebaActiva = true;

        while (!pruebaTerminada)
            yield return null;

        pruebaActiva = false;

        yield return StartCoroutine(FinalPrueba());
    }

    private IEnumerator Introduccion()
    {
        audioManager.ReproducirAudio(audioClips[(int)ParejasCartasAudioClipsType.IntroduccionParejas]);
        while (audioManager.EstaReproduciendo())
            yield return null;
    }

    private IEnumerator FinalPrueba()
    {
        audioManager.ReproducirAudio(audioClips[(int)ParejasCartasAudioClipsType.Victoria]);
        while (audioManager.EstaReproduciendo())
            yield return null;

        OnPruebaFinalizada?.Invoke();
    }

    private void ActivarCartasInicio()
    {
        foreach (var carta in cartas)
            carta.GetComponent<AppearEffect>().Appear();
    }

    public void RegistrarCarta(CardInteractable carta)
    {
        if (!pruebaActiva)
            return;

        if (carta == primeraCarta)
            return;

        if (primeraCarta == null)
        {
            primeraCarta = carta;
            primeraCarta.GetComponent<TurnEffect>().Girar();
            return;
        }

        if (segundaCarta == null)
        {
            segundaCarta = carta;
            segundaCarta.GetComponent<TurnEffect>().Girar();
            StartCoroutine(ProcesoPareja());
        }
    }

    private IEnumerator ProcesoPareja()
    {
        yield return new WaitForSeconds(1.5f);

        bool esPareja = SonPareja(primeraCarta.Type, segundaCarta.Type);

        if (esPareja)
        {
            audioManager.ReproducirAudio(audioClips[(int)ParejasCartasAudioClipsType.Correcto]);

            primeraCarta.GetComponent<FadeEffect2>().Disappear();
            segundaCarta.GetComponent<FadeEffect2>().Disappear();

            parejasHechas++;

            if (parejasHechas >= totalParejas)
                pruebaTerminada = true;
        }
        else
        {
            audioManager.ReproducirAudio(audioClips[(int)ParejasCartasAudioClipsType.Error]);

            primeraCarta.GetComponent<TurnEffect>().Girar();
            segundaCarta.GetComponent<TurnEffect>().Girar();
            yield return new WaitForSeconds(1.5f);
        }

        primeraCarta = null;
        segundaCarta = null;
    }

    private bool SonPareja(CardType a, CardType b)
    {
        string baseA = a.ToString().Replace("2", "");
        string baseB = b.ToString().Replace("2", "");
        return baseA == baseB;
    }
}
