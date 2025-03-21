using UnityEngine;

namespace RealisticSunMovement
{
	[System.Serializable]
	public class Clock
	{
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
    }
}
