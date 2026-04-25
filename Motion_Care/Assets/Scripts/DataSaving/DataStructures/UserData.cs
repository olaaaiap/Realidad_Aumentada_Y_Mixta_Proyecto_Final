using System.Collections.Generic;
using UnityEngine;

public class UserData
{
    [SerializeField] private string user_id;
    [SerializeField] private string name;
    [SerializeField] private int age;
    [SerializeField] private string created_at;
    [SerializeField] private Profile profile;
    [SerializeField] private List<Sessions> sessions;

    public UserData(string user_id, string name, int age, string created_at, Profile profile, List<Sessions> sessions)
    {
        this.user_id = user_id;
        this.name = name;
        this.age = age;
        this.created_at = created_at;
        this.profile = profile;
        this.sessions = sessions;
    }

    public UserData(string user_id, string name, int age, string created_at)
    {
        this.user_id = user_id;
        this.name = name;
        this.age = age;
        this.created_at = created_at;
    }

    #region Getters

    public string GetUserId()
    {
        return user_id;
    }

    public string GetName()
    {
        return name;
    }

    public int GetAge()
    {
        return age;
    }

    public string GetCreatedAt()
    {
        return created_at;
    }

    public Profile GetProfile()
    {
        return profile;
    }

    public List<Sessions> GetSessions()
    {
        return sessions;
    }

    #endregion

    public string FormattedData()
    {
        string data = $"User ID: {user_id}\n";
        data += $"Name: {name}\n";
        data += $"Age: {age}\n";
        data += $"Created At: {created_at}\n";
        data += "Profile:\n" + profile.FormattedData() + "\n";
        data += "Sessions:\n";
        foreach (var session in sessions)
        {
            data += session.FormattedData() + "\n";
        }
        return data;
    }

}


