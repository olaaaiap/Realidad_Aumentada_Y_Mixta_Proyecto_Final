using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class SimonDiceManager : MonoBehaviour
{
    [SerializeField] private GameObject[] gameObjects;
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private AudioClip[] audioClips;
    public Action OnPruebaFinalizada;

    private List<SimonDiceType> secuencia = new List<SimonDiceType>();
    [SerializeField] private Transform hiddenPoint;
    private Vector3[] originalPositions;


    private int indice = 0;
    private int fallos = 0;
    private bool pruebaActiva = false;
    private System.Func<IEnumerator> secuenciaActual;

    void Start()
    {
        originalPositions = new Vector3[gameObjects.Length];
        FixAppearAnimation();
        StartCoroutine(FlujoSimonDice());

        DataManager.instance.StartSession();
        ChronometerManager.instance.StartTimer();
    }

    private void FixAppearAnimation()
    {

        for (int i = 0; i < gameObjects.Length; i++)
        {
            GameObject obj = gameObjects[i];

            // Guardar posición original de cada objeto
            originalPositions[i] = obj.transform.position;

            obj.GetComponent<Rigidbody>().useGravity=false;
            obj.GetComponent<Rigidbody>().isKinematic = true;

            // Mover al punto oculto
            obj.transform.position = hiddenPoint.position;

            // Activar para que Meta registre bien la escala
            obj.SetActive(true);
        }
    }
    private void FixAppearAnimation2()
    {
        foreach (var type in secuencia)
        {
            int index = (int)type;

            gameObjects[index].transform.position = originalPositions[index];
            gameObjects[index].GetComponent<Rigidbody>().useGravity = true;
            gameObjects[index].GetComponent<Rigidbody>().isKinematic = false;
        }
    }


    private IEnumerator FlujoSimonDice()
    {
        yield return StartCoroutine(Introduccion());
        yield return StartCoroutine(Prueba1());

        DataManager.instance.SaveSequenceMetric("secuencia1", TaskType.sequence, 67, 67, 67);
        ChronometerManager.instance.StartTimer();

        yield return StartCoroutine(Prueba2());

        DataManager.instance.SaveSequenceMetric("secuencia2", TaskType.sequence, 67, 67, 67);
        ChronometerManager.instance.StartTimer();

        yield return StartCoroutine(Prueba3());

        DataManager.instance.SaveSequenceMetric("secuencia3", TaskType.sequence, 67, 67, 67);

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
        yield return new WaitForSeconds(5.0f);

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

        FixAppearAnimation2();

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
        FixAppearAnimation2();

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
        FixAppearAnimation2();

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

            audioManager.ReproducirAudio(audioClips[(int)SimonDiceAudioClipsType.Error]);
            while (audioManager.EstaReproduciendo())
                yield return null;

            if (fallos == 3)
            {
                audioManager.ReproducirAudio(audioClips[(int)SimonDiceAudioClipsType.FalloFinal]);
                pruebaActiva = false;
                yield break;
            }
            pruebaActiva = false;
            audioManager.ReproducirAudio(audioClips[(int)SimonDiceAudioClipsType.Fallo]);
            indice = 0;
            pruebaActiva = true;
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
