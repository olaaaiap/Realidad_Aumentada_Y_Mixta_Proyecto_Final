using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class Metric_Movement : Metric
{
    [SerializeField] private bool pan_position_y_fixed;
    [SerializeField] private Deviation deviation_x;
    [SerializeField] private List<Sample> samples;

    public Metric_Movement(bool pan_position_y_fixed, Deviation deviation_x, List<Sample> samples)
    {
        this.pan_position_y_fixed = pan_position_y_fixed;
        this.deviation_x = deviation_x;
        this.samples = samples;
    }
    public override string FormattedData()
    {
        string data = $"Pan Position Y Fixed: {pan_position_y_fixed}\n";
        data += deviation_x.FormattedData() + "\n";
        data += "Samples:\n";
        foreach (var sample in samples)
        {
            data += sample.FormattedData() + "\n";
        }
        return data;
    }
}

public class Deviation
{
    [SerializeField] private float mean;
    [SerializeField] private float max;
    [SerializeField] private float std_dev;

    public Deviation(float mean, float max, float std_dev)
    {
        this.mean = mean;
        this.max = max;
        this.std_dev = std_dev;
    }

    public string FormattedData()
    {
        return $"Mean: {mean}, Max: {max}, Std Dev: {std_dev}";
    }
}

[System.Serializable]
public class Sample
{
    [SerializeField] private string date;
    [SerializeField] private float x;

    public Sample(string date, float x)
    {
        this.date = date;
        this.x = x;
    }

    public string FormattedData()
    {
        return $"Date: {date}, X: {x}";
    }   
}
