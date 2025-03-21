using UnityEngine;

namespace RealisticSunMovement
{
	public class Clock : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField]
        private float timeSpeed = 1;
        public float TimeSpeed
        {
            get => timeSpeed;
            set
            {
                timeSpeed = value;
            }
        }

        [SerializeField]
        private TimeSettings timeSettings;
        public TimeSettings TimeSettings
        {
            get => timeSettings;
            set
            {
                timeSettings = value;
            }
        }

		[SerializeField, Range(0, 1)]
        [Tooltip("Winter solstice: 0 and 1\nSpring equinox: 0.25\nSummer solstice: 0.5\nAutumn equinox: 0.75\n")]
		private float yearProgress = 0.5f;
		public float YearProgress
		{
			get => yearProgress;
			set
			{
				yearProgress = Mathf.Clamp01(value);
			}
		}

		[SerializeField, Range(0, 1)]
        [Tooltip("Midnight: 0 and 1\nNoon: 0.5")]
		private float dayProgress = 0.5f;
		public float DayProgress
		{
			get => dayProgress;
			set
			{
                dayProgress = Mathf.Clamp01(value);
			}
		}

		private float rawTime;
        private float dayCount;

		private void Update()
        {
			rawTime += Time.deltaTime * timeSpeed;
            Refresh();
        }

        private void Refresh()
        {

        }

        //private void ResetTime()
        //{
        //    while (seconds < 0)
        //    {
        //        seconds += timeSettings.SecondsInMinute;
        //        minute--;
        //    }
        //    while (seconds >= timeSettings.SecondsInMinute)
        //    {
        //        seconds -= timeSettings.SecondsInMinute;
        //        minute++;
        //    }

        //    while (minute < 0)
        //    {
        //        minute += timeSettings.MinutesInHour;
        //        hour--;
        //    }
        //    while (minute >= timeSettings.MinutesInHour)
        //    {
        //        minute -= timeSettings.MinutesInHour;
        //        hour++;
        //    }

        //    hour = (hour + timeSettings.HoursInDay) % timeSettings.HoursInDay;
        //    time = (hour * timeSettings.MinutesInHour + minute) * timeSettings.SecondsInMinute + seconds;
        //}
    }
}