using UnityEngine;
using UnityEngine.SceneManagement;

public class ChronometerManager : Singleton<ChronometerManager>
{
    private bool counting;

    private float timer;

    private void Update()
    {
        if (!counting) return;

        timer += Time.deltaTime;

    }

    public void StartTimer()
    {
        timer = 0;
        counting = true;
    }

    public int StopTimer()
    {
        counting = false;
        return (int)timer;
    }

    public void SaveGlobalTime()
    {
        string globalTime = ((int)timer).ToString();
    }
}
