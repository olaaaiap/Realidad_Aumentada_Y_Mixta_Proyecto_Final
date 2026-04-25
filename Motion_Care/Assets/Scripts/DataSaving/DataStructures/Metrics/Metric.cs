using UnityEngine;

public class Metric
{
    [SerializeField] private string name;

    public virtual string FormattedData()
    {
        return "GENERIC TYPE: NO DATA";
    }
}
