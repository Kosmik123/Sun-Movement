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
		
		[field: SerializeField]
		public float DaysInYear { get; set; } = 365.242199f;
	}
}
