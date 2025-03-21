using System;
using UnityEngine;

namespace RealisticSunMovement
{
    public class SunMovement : MonoBehaviour
    {
        public Clock clock;

        [Header("Properties")]
        [SerializeField, Range(-90, 90)]
        private float latitude = 50;
		public float Latitude 
        { 
            get => latitude; 
            set => latitude = value; 
        }

		[SerializeField, Range(-180, 180)]
        private float axialTilt = 23.4f;
		public float AxialTilt 
        { 
            get => axialTilt; 
            set => axialTilt = value; 
        }

        [SerializeField, Range(1, 12)]
        private int month = 6;

        private Vector3 orbitAxis;
        private Vector3 polesAxis;
        private float time;

		private void Update()
        {
            time = clock.GetTime();
            CalculateRotation();
        }

        private void CalculateRotation()
        {
            float yearAngle = month * 30 - 180;
            float latAngle = 90 - latitude;
            float tiltedAngle = latAngle + axialTilt;
            float dayAngle = 360 * time / (Clock.SecondsInMinute * Clock.MinutesInHour * Clock.HoursInDay) + 180;

            orbitAxis = Quaternion.AngleAxis(tiltedAngle, Vector3.right) * Vector3.up;
            polesAxis = Quaternion.AngleAxis(latAngle, Vector3.right) * Vector3.up;

            transform.rotation =
                Quaternion.AngleAxis(dayAngle - yearAngle, polesAxis) *
                Quaternion.AngleAxis(yearAngle, orbitAxis) *
                Quaternion.AngleAxis(tiltedAngle, Vector3.right);
        }

#if UNITY_EDITOR
        public void Validate()
        {
            time = clock.GetTime();
            CalculateRotation();
        }

        private void OnValidate()
        {
            Validate();
        }
#endif
    }
}
