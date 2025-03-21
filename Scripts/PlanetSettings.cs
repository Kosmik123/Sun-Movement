using System;
using UnityEngine;

namespace RealisticSunMovement
{
	[CreateAssetMenu(menuName = "Realistic Sun Movement/Planet Settings")]
    public class PlanetSettings : ScriptableObject
    {
        public const float EarthAxialTilt = 23.4f;

		[field: SerializeField, Range(-180, 180)]
		public float AxialTilt { get; set; } = EarthAxialTilt;
	}
}
