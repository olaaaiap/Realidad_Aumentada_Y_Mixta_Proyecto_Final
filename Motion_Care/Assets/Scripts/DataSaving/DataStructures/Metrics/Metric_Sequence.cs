using UnityEngine;

public class Metric_Sequence : Metric
{
    [SerializeField] private int steps_total;
    [SerializeField] private int steps_correct;
    [SerializeField] private int steps_incorrect;

    public Metric_Sequence(int steps_total, int steps_correct, int steps_incorrect)
    {
        this.steps_total = steps_total;
        this.steps_correct = steps_correct;
        this.steps_incorrect = steps_incorrect;
    }

    public override string FormattedData()
    {
        return $"Steps Total: {steps_total}, Steps Correct: {steps_correct}, Steps Incorrect: {steps_incorrect}";
    }
}
