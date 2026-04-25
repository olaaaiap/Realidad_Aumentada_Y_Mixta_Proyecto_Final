using System.IO;
using UnityEngine;

public class DataManager : Singleton<DataManager>
{
    private string filePath;
    private UserData currentUser;

    private void Awake()
    {
        InitializeUserData();
        print("Nombre:" + currentUser.GetName() + " Edad: " +currentUser.GetAge());
        print("Condicion " + currentUser.GetProfile().GetCondition() + " Notes: "+currentUser.GetProfile().GetNotes());
    }

    void Start()
    {
        filePath = Application.persistentDataPath + "/MotionCare_UserData.txt";
        DontDestroyOnLoad(this);
    }

    //// Formato HH:MM:SS
    //// return string.Format("{0:00}:{1:00}:{2:00}", hours, minutes, seconds);

    //int hours = Mathf.FloorToInt(gameTimer.elapsedTime / 3600f);
    //int minutes = Mathf.FloorToInt((gameTimer.elapsedTime % 3600f) / 60f);
    //int seconds = Mathf.FloorToInt(gameTimer.elapsedTime % 60f);

    //string timeString = string.Format("{0:00}:{1:00}:{2:00}", hours, minutes, seconds);


    public void SaveData()
    {
        string dataString = "";
        File.AppendAllText(filePath, dataString + "\n");

        Debug.Log("Tiempo guardado en: " + filePath);
    }

    public void InitializeUserData()
    {
        string userId = "user_000";
        int age = Random.Range(60, 110);

        currentUser = new UserData(
            userId,
            NameList.GetRandomName(),
            age,
            DateFormatter.FormatCurrentDate(),
            ConditionList.GetRandomCondition(),
            null
            );
    }

}