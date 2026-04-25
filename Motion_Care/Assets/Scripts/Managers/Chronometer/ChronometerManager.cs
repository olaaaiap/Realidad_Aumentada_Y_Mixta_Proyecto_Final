using UnityEngine;
using UnityEngine.SceneManagement;

public class ChronometerManager : Singleton<ChronometerManager>
{
    private bool counting;

    private float timer;
    private float sceneTimer;

    private void Update()
    {

        timer += Time.deltaTime;

        if (!counting) return;

        sceneTimer += Time.deltaTime;
    }

    public void StartTimer()
    {
        counting = true;
    }

    public void StopTimer()
    {
        counting = false;
        SaveBasedOnScene();
    }

    public void SaveBasedOnScene()
    {
        string taskTime = ((int)sceneTimer).ToString();

        switch (SceneManager.GetActiveScene().name)
        {
            case "Tutorial":

                break;

            case "Sala1_Cocina":

                break;

            case "Sala2_Comedor":

                break;

            case "Sala3_salaDeEstar":

                break;

        }
    }

    public void SaveGlobalTime()
    {
        string globalTime = ((int)timer).ToString();
    }
}
