using UnityEngine;

public static class DateFormatter
{
    public static string FormatCurrentDate()
    {
        System.DateTime now = System.DateTime.Now;
        return now.ToUniversalTime().ToString("yyyy-MM-dd'T'HH:mm:ss'Z'");
    }
}
