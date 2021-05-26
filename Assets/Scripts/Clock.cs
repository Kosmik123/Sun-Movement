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
    private int _hour;
    private int _minute;
    private int _second;

    private float time;

    public int hour { get { return _hour; } set { _hour = value; } }
    public int minute { get { return _minute; } set { _minute = value; } }
    public int second { get { return _second; } set { _second = value; ResetTime(); } }



    private void Update()
    {
        time += Time.deltaTime * timeSpeed;
        Refresh();
    }

    private void Refresh()
    {
        _hour = (int)(time / (secondsInMinute * minutesInHour)) % hoursInDay;
        _minute = (int)(time / secondsInMinute) % minutesInHour;
        _second = (int)time % secondsInMinute;
    }

    private void ResetTime()
    {
        while (_second < 0)
        {
            _second += secondsInMinute;
            _minute--;
        }
        while (_second >= secondsInMinute)
        {
            _second -= secondsInMinute;
            _minute++;
        }

        while (_minute < 0)
        {
            _minute += minutesInHour;
            _hour--;
        }
        while (_minute >= minutesInHour)
        {
            _minute -= minutesInHour;
            _hour++;
        }

        _hour = (_hour + hoursInDay) % hoursInDay;
        time = (_hour * minutesInHour + _minute) * secondsInMinute + _second;
    }

    public float GetTime()
    {
        return time;
    }

#if UNITY_EDITOR

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



            UpdateSun();
        
        }
        void UpdateSun()
        {
            SunMovement sunMovement = clock.GetComponent<SunMovement>();
            if (sunMovement != null)
                sunMovement.Validate();
        }


    }

#endif
}
