using UnityEditor;
using UnityEngine;

public class Clock : MonoBehaviour
{
    public static int hoursInDay = 24;
    public static int minutesInHour = 60;
    public static int secondsInMinute = 60;

    [Header("Settings")]
    public float timeSpeed = 1;

    [Header("States")]
    public int hour;
    public int minute;
    public int second;

    private float time;

    private void Update()
    {
        time += Time.deltaTime * timeSpeed;
        Refresh();
    }

    private void Refresh()
    {
        second = Mathf.FloorToInt(time) % secondsInMinute;
        minute = Mathf.FloorToInt(time / secondsInMinute) % minutesInHour;
        hour = Mathf.FloorToInt(time / (minutesInHour * secondsInMinute)) % hoursInDay;
    }

    public void SetTime(int hours, int minutes = 0, int seconds = 0)
    {
        hours = (hours + hoursInDay) % hoursInDay;
        minutes = (minutes + minutesInHour) % minutesInHour;
        seconds = (seconds + secondsInMinute) % secondsInMinute;
        time = (hours * minutesInHour + minutes) * secondsInMinute + seconds;
    }

    public float GetTime()
    {
        return time;
    }

#if UNITY_EDITOR
    public void OnValidate()
    {
        hour = (hour + hoursInDay) % hoursInDay;
        minute = (minute + minutesInHour) % minutesInHour;
        second = (second + secondsInMinute) % secondsInMinute;
        SetTime(hour, minute, second);
    }

    [CustomEditor(typeof(Clock))]
    public class ClockEditor : Editor
    {
        Clock clock;

        public override void OnInspectorGUI()
        {
            clock = target as Clock;

            EditorGUILayout.LabelField("Time", EditorStyles.boldLabel);      
            GUILayout.BeginHorizontal();
            EditorGUIUtility.labelWidth = 12;
            clock.hour = EditorGUILayout.IntField((clock.hour + hoursInDay) % hoursInDay);
            clock.minute = EditorGUILayout.IntField(":", (clock.minute + minutesInHour) % minutesInHour);
            clock.second = EditorGUILayout.IntField(":", (clock.second + secondsInMinute) % secondsInMinute);
            GUILayout.EndHorizontal();

            EditorGUIUtility.labelWidth = 0;
            EditorGUILayout.LabelField("Properties", EditorStyles.boldLabel);
            clock.timeSpeed = EditorGUILayout.FloatField("Time Speed", clock.timeSpeed);
        }
    }

#endif
}
