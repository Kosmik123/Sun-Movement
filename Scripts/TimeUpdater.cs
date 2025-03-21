using UnityEngine;

namespace RealisticSunMovement
{
	public class TimeUpdater : MonoBehaviour
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

		[SerializeField]
		private SunMovement sunMovement;

		[Header("States")]
		[SerializeField]
		private float rawSeconds;
		[SerializeField]
		private float dayCount;

		private void OnEnable()
		{
			float yearProgress = sunMovement.Clock.YearProgress;
			float dayProgress = sunMovement.Clock.DayProgress;

			rawSeconds = dayProgress * timeSettings.SecondsInDay;
			dayCount = yearProgress * timeSettings.DaysInYear;
		}

		private void Update()
		{
			rawSeconds += Time.deltaTime * timeSpeed;
			ValidateRawValues();
			UpdateSunClock();
		}

		private void ValidateRawValues()
		{
			if (timeSettings == null)
				return;

			float daysInYear = timeSettings.DaysInYear;
			float secondsInDay = timeSettings.SecondsInDay;
			if (rawSeconds >= secondsInDay || rawSeconds < 0)
			{
				int fullDays = Mathf.FloorToInt(rawSeconds / secondsInDay);
				float secodsRemaining = rawSeconds - fullDays * secondsInDay;
				dayCount += fullDays;
				rawSeconds = secodsRemaining;
			}

			if (dayCount < 0 || dayCount >= daysInYear)
			{
				int fullYears = Mathf.FloorToInt(dayCount / daysInYear);
				float daysRemaining = dayCount - fullYears * daysInYear;
				dayCount = daysRemaining;
			}
		}

		private void UpdateSunClock()
		{
			float yearProgress = dayCount / timeSettings.DaysInYear;
			float dayProgress = rawSeconds / timeSettings.SecondsInDay;

			sunMovement.Clock.YearProgress = yearProgress;
			sunMovement.Clock.DayProgress = dayProgress;
		}

		private void OnValidate()
		{
			ValidateRawValues();
		}
	}
}