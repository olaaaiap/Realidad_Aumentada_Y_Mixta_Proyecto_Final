using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Task
{

    [SerializeField] private string task_id;
    [SerializeField] private string task_name;
    [SerializeField] private TaskType task_type;
    [SerializeField] private string started_at;
    [SerializeField] private string ended_at;
    [SerializeField] private List<Metric> metrics;

    public Task(string task_id, string task_name, TaskType task_type, string started_at, string ended_at, List<Metric> metrics)
    {
        this.task_id = task_id;
        this.task_name = task_name;
        this.task_type = task_type;
        this.started_at = started_at;
        this.ended_at = ended_at;
        this.metrics = metrics;
    }

    public string FormattedData()
    {
        string data = $"Task ID: {task_id}\n";
        data += $"Task Name: {task_name}\n";
        data += $"Task Type: {task_type}\n";
        data += $"Started At: {started_at}\n";
        data += $"Ended At: {ended_at}\n";
        data += "Metrics:\n";
        foreach (var metric in metrics)
        {
            data += metric.FormattedData() + "\n";
        }
        return data;
    }

}
