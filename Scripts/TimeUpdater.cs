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

		private float rawSeconds;
		private float dayCount;

		private void Update()
		{
			float secondsInDay = timeSettings.SecondsInDay;
			rawSeconds += Time.deltaTime * timeSpeed;
		}
	}
}