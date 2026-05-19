using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Sessions
{
    [SerializeField] private string session_id;
    [SerializeField] private string date;
    public int duration_seconds;
    public List<Task> tasks;

    public Sessions(string session_id, string date, int duration_seconds, List<Task> tasks)
    {
        this.session_id = session_id;
        this.date = date;
        this.duration_seconds = duration_seconds;
        this.tasks = tasks;
    }

    public string FormattedData()
    {
        string data = $"Session ID: {session_id}\n";
        data += $"Date: {date}\n";
        data += $"Duration (seconds): {duration_seconds}\n";
        data += "Tasks:\n";
        foreach (var task in tasks)
        {
            data += task.FormattedData() + "\n";
        }
        return data;
    }

}
