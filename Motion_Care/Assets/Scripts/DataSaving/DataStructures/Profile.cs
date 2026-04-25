using UnityEngine;

public class Profile
{
    [SerializeField] private string condition;
    [SerializeField] private string notes;

    public Profile(string condition, string notes)
    {
        this.condition = condition;
        this.notes = notes;
    }

    public string FormattedData()
    {
        return $"Condition: {condition}\nNotes: {notes}";
    }

    public string GetCondition()
    {
        return condition;
    }

    public string GetNotes()
    {
        return notes;
    }
}
