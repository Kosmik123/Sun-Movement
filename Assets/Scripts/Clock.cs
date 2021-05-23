﻿using UnityEditor;
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
        SetTime(hour, minute, second);
    }
#endif
}
