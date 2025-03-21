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

    public class SunMovement : MonoBehaviour
    {
        public Clock clock;
        [SerializeField]
        private TimeSettings timeSettings;
        [SerializeField]
        private PlanetSettings planetSettings;

		[SerializeField, Range(-90, 90)]
        private float latitude = 50;
		public float Latitude 
        { 
            get => latitude; 
            set => latitude = value; 
        }

        [Header("Time Settings")]
        [SerializeField, Range(1, 12)]
        private float month = 6;
        public float Month
		{
			get => month;
			set => month = value;
		}

        [SerializeField]
        private Vector3 orbitAxis;
        [SerializeField]
        private Vector3 polesAxis;
        [SerializeField]
        private float time;

        private void CalculateRotation()
        {
            float yearAngle = month * 30 - 180;
            float sunAngleForLatitude = 90 - latitude;
            float tiltedAngle = sunAngleForLatitude + planetSettings.AxialTilt;
            float dayAngle = 360 * time / (timeSettings.SecondsInMinute * timeSettings.MinutesInHour * timeSettings.HoursInDay) + 180;

            orbitAxis = Quaternion.AngleAxis(tiltedAngle, Vector3.right) * Vector3.up;
            polesAxis = Quaternion.AngleAxis(sunAngleForLatitude, Vector3.right) * Vector3.up;

            transform.rotation =
                Quaternion.AngleAxis(dayAngle - yearAngle, polesAxis) *
                Quaternion.AngleAxis(yearAngle, orbitAxis) *
                Quaternion.AngleAxis(tiltedAngle, Vector3.right);
        }

		private void OnValidate()
		{
            CalculateRotation();
		}

		private void OnDrawGizmosSelected()
		{
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(transform.position - orbitAxis, transform.position + orbitAxis);
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position, transform.position + polesAxis);
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, transform.position - polesAxis);
		}
	}
}
