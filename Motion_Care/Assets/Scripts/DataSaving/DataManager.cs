using System.IO;
using UnityEngine;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : Singleton<DataManager>
{
    private string filePath;

    // Usuario actual
    private UserData currentUser;

    // Lista completa de usuarios
    private UserDataList usersDatabase = new UserDataList();

    // Sesión actual
    private Sessions currentSession;

    // Tasks actuales
    private List<Task> currentTasks = new List<Task>();

    // =====================================================
    // USER
    // =====================================================

    private void Awake()
    {
        filePath = Application.persistentDataPath + "/userdata.json";

        LoadUsersData();

        InitializeUserData();
    }

    private void OnApplicationQuit()
    {
        SaveCurrentUser();
    }

    public void InitializeUserData()
    {
        string userId = "user_00"+usersDatabase.users.Count;
        int age = Random.Range(60, 110);

        currentUser = new UserData(
            userId,
            NameList.GetRandomName(),
            age,
            DateFormatter.FormatCurrentDate(),
            ConditionList.GetRandomCondition(),
            new List<Sessions>()
        );
    }

    // =====================================================
    // SESSION
    // =====================================================

    public void StartSession()
    {
        currentTasks = new List<Task>();

        currentSession = new Sessions(
            GenerateID("session"),
            DateFormatter.FormatCurrentDate(),
            0,
            currentTasks
        );
    }

    public void EndSession()
    {
        currentSession.tasks = currentTasks;
        CountSessionTime();
        currentUser.GetSessions().Add(currentSession);

        Debug.Log("Sesion añadida al usuario");
    }

    public void CountSessionTime()
    {
        int seconds = 0;
        foreach (Task task in currentSession.tasks) seconds += task.time_to_complete;
        currentSession.duration_seconds = seconds;
    }

    // =====================================================
    // SAVE METRICS
    // =====================================================

    public void SaveSequenceMetric(
        string taskName,
        TaskType taskType,
        int total,
        int correct,
        int incorrect
    )
    {
        Metric_Sequence metric = new Metric_Sequence(
            total,
            correct,
            incorrect
        );

        CreateTask(taskName, taskType, metric);
    }

    public void SavePouringMetric(
        string taskName,
        TaskType taskType,
        int spilledML
    )
    {
        Metric_Pouring metric = new Metric_Pouring(spilledML);

        CreateTask(taskName, taskType, metric);
    }

    public void SaveMemoryMetric(
        string taskName,
        TaskType taskType,
        int correct,
        int incorrect
    )
    {
        Metric_Memory metric = new Metric_Memory(
            correct,
            incorrect
        );

        CreateTask(taskName, taskType, metric);
    }

    public void SaveMovementMetric(
        string taskName,
        TaskType taskType,
        bool panPositionFixed,
        Deviation deviation,
        List<Sample> samples
    )
    {
        Metric_Movement metric = new Metric_Movement(
            panPositionFixed,
            deviation,
            samples
        );

        CreateTask(taskName, taskType, metric);
    }

    // =====================================================
    // TASK CREATION
    // =====================================================

    private void CreateTask(
        string taskName,
        TaskType taskType,
        Metric metric
    )
    {
        List<Metric> metrics = new List<Metric>();
        metrics.Add(metric);

        Task task = new Task(
            GenerateID("task"),
            taskName,
            taskType,
            DateFormatter.FormatCurrentDate(),
            DateFormatter.FormatCurrentDate(),
            metrics
        );

        task.time_to_complete = ChronometerManager.instance.StopTimer();

        currentTasks.Add(task);

        Debug.Log("Task añadida a la sesión");
    }

    // =====================================================
    // HELPERS
    // =====================================================

    private string GenerateID(string prefix)
    {
        return prefix + "_" + System.Guid.NewGuid().ToString("N");
    }

    // =====================================================
    // DEBUG
    // =====================================================

    public void PrintCurrentUserData()
    {
        Debug.Log(currentUser.FormattedData());
    }

    // =====================================================
    // SAVE CURRENT USER
    // =====================================================

    public void SaveCurrentUser()
    {
        StartSession();
        SaveMemoryMetric("Wajaka", TaskType.memory, 1, 2);
        SavePouringMetric("Yipeeeee", TaskType.pouring, 10000);
        EndSession();

        // Añadir usuario actual a la lista
        usersDatabase.users.Add(currentUser);

        // Convertir a JSON
        string json = JsonUtility.ToJson(usersDatabase, true);

        // Guardar archivo
        File.WriteAllText(filePath, json);

        Debug.Log("Base de datos guardada");
        Debug.Log(filePath);
    }

    // =====================================================
    // LOAD DATABASE
    // =====================================================

    public void LoadUsersData()
    {
        if (!File.Exists(filePath))
        {
            usersDatabase = new UserDataList();
            return;
        }

        string json = File.ReadAllText(filePath);

        usersDatabase = JsonUtility.FromJson<UserDataList>(json);

        Debug.Log("Usuarios cargados: " + usersDatabase.users.Count);
    }

    // =====================================================
    // GETTERS
    // =====================================================

    public List<UserData> GetAllUsers()
    {
        return usersDatabase.users;
    }

    public UserData GetCurrentUser()
    {
        return currentUser;
    }


    [System.Serializable]
    public class UserDataList
    {
        public List<UserData> users = new List<UserData>();
    }
}
