using UnityEngine;

[System.Serializable]
public class Metric_Memory : Metric
{
    [SerializeField] private int correct;
    [SerializeField] private int incorrect;

    public Metric_Memory(int correct, int incorrect)
    {
        this.correct = correct;
        this.incorrect = incorrect;
    }

    //public override string FormattedData()
    //{
    //    return $"Correct: {correct}, Incorrect: {incorrect}";
    //}
}
