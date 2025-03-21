using UnityEngine;

namespace RealisticSunMovement
{
	[CreateAssetMenu(menuName = "Realistic Sun Movement/Time Settings")]
	public class TimeSettings : ScriptableObject
	{
		[field: SerializeField]
		public float SecondsInDay { get; set; } = 60 * 60 * 24;

		[field: SerializeField]
		public float DaysInYear { get; set; } = 365.242199f;
	}
}
