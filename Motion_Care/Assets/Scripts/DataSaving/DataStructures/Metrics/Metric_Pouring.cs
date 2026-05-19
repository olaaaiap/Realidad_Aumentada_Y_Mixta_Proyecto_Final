using UnityEngine;

[System.Serializable]
public class Metric_Pouring : Metric
{
    [SerializeField] private int liqued_spilled_ml;

    public Metric_Pouring(int liqued_spilled_ml)
    {
        this.liqued_spilled_ml = liqued_spilled_ml;
    }

    //public override string FormattedData()
    //{
    //    return $"Liqued Spilled (ml): {liqued_spilled_ml}";
    //}
}
