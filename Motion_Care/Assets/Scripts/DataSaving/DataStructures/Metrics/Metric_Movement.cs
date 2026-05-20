using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Metric_Movement : Metric
{
    [SerializeField] private Deviation deviation;

    public Metric_Movement(Deviation deviation_x)
    {
        this.deviation = deviation_x;
    }
}

public class Deviation
{
    [SerializeField] private float mean;
    [SerializeField] private float max;

    public Deviation(float mean, float max)
    {
        this.mean = mean;
        this.max = max;
    }

    //public string FormattedData()
    //{
    //    return $"Mean: {mean}, Max: {max}, Std Dev: {std_dev}";
    //}

    public override string ToString()
    {
        return $"Media: {mean}, Max: {max}";
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
