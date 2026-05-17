using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SimonDiceManager : MonoBehaviour
{
    [SerializeField] private GameObject[] gameObjects;
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private AudioClip[] audioClips;
    public Action OnPruebaFinalizada;

    private List<SimonDiceType> secuencia = new List<SimonDiceType>();
    private int indice = 0;
    private int fallos = 0;
    private bool pruebaActiva = false;
    private System.Func<IEnumerator> secuenciaActual;

    void Start()
    {
        StartCoroutine(FlujoSimonDice());
    }

    private IEnumerator FlujoSimonDice()
    {
        yield return StartCoroutine(Introduccion());
        yield return StartCoroutine(Prueba1());
        yield return StartCoroutine(Prueba2());
        yield return StartCoroutine(Prueba3());
        yield return StartCoroutine(FinalPrueba());
    }

    private IEnumerator Introduccion()
    {
        audioManager.ReproducirAudio(audioClips[(int)SimonDiceAudioClipsType.IntroduccionSimonDice]);
        while (audioManager.EstaReproduciendo())
            yield return null;
    }

    private IEnumerator FinalPrueba()
    {
        audioManager.ReproducirAudio(audioClips[(int)SimonDiceAudioClipsType.Victoria]);
        while (audioManager.EstaReproduciendo())
            yield return null;
        audioManager.ReproducirAudio(audioClips[(int)SimonDiceAudioClipsType.ExitoFinal]);
        while (audioManager.EstaReproduciendo())
            yield return null;

        OnPruebaFinalizada?.Invoke();
    }

    private IEnumerator Prueba1()
    {
        secuencia = new List<SimonDiceType>()
        {
            SimonDiceType.Book1,
            SimonDiceType.Book2,
            SimonDiceType.Book3,
            SimonDiceType.Book4
        };

        secuenciaActual = Secuencia1;

        ActivarObjectos(true);

        audioManager.ReproducirAudio(audioClips[(int)SimonDiceAudioClipsType.PrimeraPrueba]);
        while (audioManager.EstaReproduciendo())
            yield return null;

        yield return StartCoroutine(secuenciaActual());
        pruebaActiva = true;

        while (pruebaActiva)
            yield return null;

        ActivarObjectos(false);

        indice = 0;
        fallos = 0;
    }

    private IEnumerator Secuencia1()
    {
        audioManager.ReproducirAudio(audioClips[(int)SimonDiceAudioClipsType.LibroRosa]);
        ActivarBlink(SimonDiceType.Book1);
        yield return new WaitForSeconds(3.5f);

        audioManager.ReproducirAudio(audioClips[(int)SimonDiceAudioClipsType.LibroVerde]);
        ActivarBlink(SimonDiceType.Book2);
        yield return new WaitForSeconds(3.5f);

        audioManager.ReproducirAudio(audioClips[(int)SimonDiceAudioClipsType.LibroAmarillo]);
        ActivarBlink(SimonDiceType.Book3);
        yield return new WaitForSeconds(3.5f);

        audioManager.ReproducirAudio(audioClips[(int)SimonDiceAudioClipsType.LibroAzul]);
        ActivarBlink(SimonDiceType.Book4);
        yield return new WaitForSeconds(3.5f);
    }

    private IEnumerator Prueba2()
    {
        yield return new WaitForSeconds(2.0f);

        secuencia = new List<SimonDiceType>()
        {
            SimonDiceType.Book2,
            SimonDiceType.Mug2,
            SimonDiceType.Book1,
            SimonDiceType.Book4,
            SimonDiceType.Mug3,
            SimonDiceType.Book3,
        };

        secuenciaActual = Secuencia2;

        ActivarObjectos(true);

        audioManager.ReproducirAudio(audioClips[(int)SimonDiceAudioClipsType.SegundaPrueba]);
        while (audioManager.EstaReproduciendo())
            yield return null;

        yield return StartCoroutine(secuenciaActual());
        pruebaActiva = true;

        while (pruebaActiva)
            yield return null;

        ActivarObjectos(false);

        indice = 0;
        fallos = 0;
    }

    private IEnumerator Secuencia2()
    {
        audioManager.ReproducirAudio(audioClips[(int)SimonDiceAudioClipsType.LibroVerde]);
        ActivarBlink(SimonDiceType.Book2);
        yield return new WaitForSeconds(3.5f);

        audioManager.ReproducirAudio(audioClips[(int)SimonDiceAudioClipsType.TazaMorada]);
        ActivarBlink(SimonDiceType.Mug2);
        yield return new WaitForSeconds(3.5f);

        audioManager.ReproducirAudio(audioClips[(int)SimonDiceAudioClipsType.LibroRosa]);
        ActivarBlink(SimonDiceType.Book1);
        yield return new WaitForSeconds(3.5f);

        audioManager.ReproducirAudio(audioClips[(int)SimonDiceAudioClipsType.LibroAzul]);
        ActivarBlink(SimonDiceType.Book4);
        yield return new WaitForSeconds(3.5f);

        audioManager.ReproducirAudio(audioClips[(int)SimonDiceAudioClipsType.TazaRoja]);
        ActivarBlink(SimonDiceType.Mug3);
        yield return new WaitForSeconds(3.5f);

        audioManager.ReproducirAudio(audioClips[(int)SimonDiceAudioClipsType.LibroAmarillo]);
        ActivarBlink(SimonDiceType.Book3);
        yield return new WaitForSeconds(3.5f);
    }

    private IEnumerator Prueba3()
    {
        yield return new WaitForSeconds(2.0f);

        secuencia = new List<SimonDiceType>()
        {
            SimonDiceType.Book2,
            SimonDiceType.Book1,
            SimonDiceType.Mug1,
            SimonDiceType.Book4,
            SimonDiceType.Book3,
            SimonDiceType.Mug4,
        };

        secuenciaActual = Secuencia3;

        ActivarObjectos(true);

        audioManager.ReproducirAudio(audioClips[(int)SimonDiceAudioClipsType.TerceraPrueba]);
        while (audioManager.EstaReproduciendo())
            yield return null;

        yield return StartCoroutine(secuenciaActual());
        pruebaActiva = true;

        while (pruebaActiva)
            yield return null;

        ActivarObjectos(false);

        indice = 0;
        fallos = 0;
    }

    private IEnumerator Secuencia3()
    {
        audioManager.ReproducirAudio(audioClips[(int)SimonDiceAudioClipsType.LibroVerde]);
        ActivarBlink(SimonDiceType.Book2);
        yield return new WaitForSeconds(3.5f);

        audioManager.ReproducirAudio(audioClips[(int)SimonDiceAudioClipsType.LibroRosa]);
        ActivarBlink(SimonDiceType.Book1);
        yield return new WaitForSeconds(3.5f);

        audioManager.ReproducirAudio(audioClips[(int)SimonDiceAudioClipsType.TazaAzul]);
        ActivarBlink(SimonDiceType.Mug1);
        yield return new WaitForSeconds(3.5f);

        audioManager.ReproducirAudio(audioClips[(int)SimonDiceAudioClipsType.LibroAzul]);
        ActivarBlink(SimonDiceType.Book4);
        yield return new WaitForSeconds(3.5f);

        audioManager.ReproducirAudio(audioClips[(int)SimonDiceAudioClipsType.LibroAmarillo]);
        ActivarBlink(SimonDiceType.Book3);
        yield return new WaitForSeconds(3.5f);

        audioManager.ReproducirAudio(audioClips[(int)SimonDiceAudioClipsType.TazaVerde]);
        ActivarBlink(SimonDiceType.Mug4);
        yield return new WaitForSeconds(3.5f);
    }

    private IEnumerator EvaluarSecuencia(SimonDiceType type)
    {
        if (secuencia[indice] != type)
        {
            fallos++;

            if (fallos == 3)
            {
                audioManager.ReproducirAudio(audioClips[(int)SimonDiceAudioClipsType.FalloFinal]);
                pruebaActiva = false;
                yield break;
            }

            audioManager.ReproducirAudio(audioClips[(int)SimonDiceAudioClipsType.Fallo]);
            indice = 0;
            yield return StartCoroutine(secuenciaActual());
            yield break;
        }

        indice++;

        if (indice >= secuencia.Count)
        {
            audioManager.ReproducirAudio(audioClips[(int)SimonDiceAudioClipsType.Correcto]);
            while (audioManager.EstaReproduciendo())
                yield return null;

            audioManager.ReproducirAudio(audioClips[(int)SimonDiceAudioClipsType.ExitoSecuencia]);
            while (audioManager.EstaReproduciendo())
                yield return null;

            pruebaActiva = false;
        }
    }

    private void ActivarBlink(SimonDiceType gameObjectType)
    {
        gameObjects[(int)gameObjectType].GetComponent<BlinkEffect>().StartBlink();
    }

    private void ActivarObjectos(bool enable)
    {
        foreach (var obj in secuencia)
        {
            if (enable)
            {
                gameObjects[(int)obj].GetComponent<AppearEffect>().Appear();
            }
            else
            {
                gameObjects[(int)obj].GetComponent<FadeEffect>().Disappear();
            }
        }
    }

    public void RegistrarObjectoCogido(SimonDiceType type)
    {
        if (pruebaActiva)
        {
            StartCoroutine(EvaluarSecuencia(type));
        }
    }
}
