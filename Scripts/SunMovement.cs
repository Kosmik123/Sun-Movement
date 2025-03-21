using System;
using UnityEngine;

namespace RealisticSunMovement
{
	public class SunMovement : MonoBehaviour
	{
		private const float FullAngle = 360;
		private const int XAngleAtZenith = 90;
		
		[SerializeField]
		private PlanetSettings planetSettings;
		
		[SerializeField]
		private Clock clock;
		public Clock Clock
		{
			get => clock;
			set => clock = value;
		}

		[SerializeField, Range(-90, 90)]
		[Tooltip("Positive is north, negative is south")]
		private float latitude = 50;
		public float Latitude
		{
			get => latitude;
			set => latitude = Mathf.Clamp(value, -90, 90);
		}

		private Vector3 orbitAxis;
		private Vector3 polesAxis;

		private void CalculateRotation()
		{
			if (clock == null || planetSettings == null)
				return;

			float latitudeOfZenith = planetSettings.AxialTilt * GetTiltAmountFromYearProgress(clock.YearProgress);
			float sunHeightAtNoon = XAngleAtZenith - latitude + latitudeOfZenith;

			polesAxis = Quaternion.AngleAxis(-latitude, Vector3.right) * Vector3.forward;
			orbitAxis = Quaternion.AngleAxis(sunHeightAtNoon, Vector3.right) * Vector3.up;

			float dayAngle = FullAngle * clock.DayProgress + 180;

			var sunRotationRelativeToDayTime = Quaternion.AngleAxis(dayAngle, polesAxis);
			var sunRotationAtNoon = Quaternion.AngleAxis(sunHeightAtNoon, Vector3.right);

			transform.rotation = sunRotationRelativeToDayTime * sunRotationAtNoon;
		}

		private static float GetTiltAmountFromYearProgress(float yearProgress) => -Mathf.Cos(yearProgress * 2 * Mathf.PI);


		private void Update()
		{
			CalculateRotation();
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
