using UnityEditor;
using UnityEngine;


namespace RealisticSunMovement
{
    public class Clock : MonoBehaviour
    {
        public static int HoursInDay = 24;
        public static int MinutesInHour = 60;
        public static int SecondsInMinute = 60;

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
            _hour = (int)(time / (SecondsInMinute * MinutesInHour)) % HoursInDay;
            _minute = (int)(time / SecondsInMinute) % MinutesInHour;
            _second = (int)time % SecondsInMinute;
        }

        private void ResetTime()
        {
            while (_second < 0)
            {
                _second += SecondsInMinute;
                _minute--;
            }
            while (_second >= SecondsInMinute)
            {
                _second -= SecondsInMinute;
                _minute++;
            }

            while (_minute < 0)
            {
                _minute += MinutesInHour;
                _hour--;
            }
            while (_minute >= MinutesInHour)
            {
                _minute -= MinutesInHour;
                _hour++;
            }

            _hour = (_hour + HoursInDay) % HoursInDay;
            time = (_hour * MinutesInHour + _minute) * SecondsInMinute + _second;
        }

        public float GetTime()
        {
            return time;
        }
    }
}