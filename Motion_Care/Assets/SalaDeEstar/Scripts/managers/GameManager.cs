using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private SimonDiceManager simonDice;
    [SerializeField] private ParejasCartasManager parejas;
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private AudioClip audioBienvenida;
    [SerializeField] private AudioClip audioCompletada;


    private void Start()
    {
        simonDice.OnPruebaFinalizada += LanzarParejas;
        parejas.OnPruebaFinalizada += FinDelJuego;

        StartCoroutine(FlujoInicial());

    }

    private IEnumerator FlujoInicial()
    {
        yield return StartCoroutine(BienvenidaSala());

        LanzarSimonDice();
    }


    private void LanzarSimonDice()
    {
        simonDice.gameObject.SetActive(true);
        parejas.gameObject.SetActive(false);
    }

    private void LanzarParejas()
    {

        simonDice.gameObject.SetActive(false);
        parejas.gameObject.SetActive(true);
    }

    private IEnumerator BienvenidaSala()
    {
        audioManager.ReproducirAudio(audioBienvenida);
        while (audioManager.EstaReproduciendo())
            yield return null;
    }

    private IEnumerator SalaCompletada()
    {
        audioManager.ReproducirAudio(audioCompletada);
        while (audioManager.EstaReproduciendo())
            yield return null;
    }
    private void FinDelJuego()
    {
        StartCoroutine(SalaCompletada());
        Debug.Log("TODOS LOS MINIJUEGOS COMPLETADOS");
    }
}
