using UnityEngine;

namespace RealisticSunMovement
{
	[CreateAssetMenu(menuName = "Realistic Sun Movement/Time Settings")]
	public class TimeSettings : ScriptableObject
	{
		[field: SerializeField]
		public float SecondsInMinute { get; set; } = 60;

		[field: SerializeField]
		public float MinutesInHour { get; set; } = 60;

		[field: SerializeField]
		public float HoursInDay { get; set; } = 24;
	}

	public class Clock : MonoBehaviour
    {
        [Header("Settings")]
        public float timeSpeed = 1;

        [SerializeField]
        private TimeSettings timeSettings;

        [Header("States")]
        private float hour;
        private float minute;
        private float seconds;

        private float time;

        public float Hour
		{
			get => hour;
			set => hour = value;
		}
		public float Minute
		{
			get => minute;
			set => minute = value;
		}
		public float Second
		{
			get => seconds;
			set 
            { 
                seconds = value; 
                ResetTime();
            }
		}

		private void Update()
        {
            time += Time.deltaTime * timeSpeed;
            Refresh();
        }

        private void Refresh()
        {
            hour = (int)(time / (timeSettings.SecondsInMinute * timeSettings.MinutesInHour) % timeSettings.HoursInDay);
            minute = (int)(time / timeSettings.SecondsInMinute % timeSettings.MinutesInHour);
            seconds = (int)(time % timeSettings.SecondsInMinute);
        }

        private void ResetTime()
        {
            while (seconds < 0)
            {
                seconds += timeSettings.SecondsInMinute;
                minute--;
            }
            while (seconds >= timeSettings.SecondsInMinute)
            {
                seconds -= timeSettings.SecondsInMinute;
                minute++;
            }

            while (minute < 0)
            {
                minute += timeSettings.MinutesInHour;
                hour--;
            }
            while (minute >= timeSettings.MinutesInHour)
            {
                minute -= timeSettings.MinutesInHour;
                hour++;
            }

            hour = (hour + timeSettings.HoursInDay) % timeSettings.HoursInDay;
            time = (hour * timeSettings.MinutesInHour + minute) * timeSettings.SecondsInMinute + seconds;
        }

        public float GetTime()
        {
            return time;
        }
    }
}