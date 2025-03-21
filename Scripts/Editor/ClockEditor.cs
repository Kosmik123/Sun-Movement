using UnityEngine;
using UnityEditor;

namespace RealisticSunMovement.Editor
{
	[CustomEditor(typeof(Clock))]
	public class ClockEditor : UnityEditor.Editor
	{
		private Clock clock;

		public override void OnInspectorGUI()
		{
			clock = target as Clock;

			EditorGUILayout.LabelField("Time", EditorStyles.boldLabel);
			GUILayout.BeginHorizontal();
			EditorGUIUtility.labelWidth = 12;
			clock.hour = EditorGUILayout.IntField((clock.hour + Clock.HoursInDay) % Clock.HoursInDay);
			clock.minute = EditorGUILayout.IntField(":", (clock.minute + Clock.MinutesInHour) % Clock.MinutesInHour);
			clock.second = EditorGUILayout.IntField(":", (clock.second + Clock.SecondsInMinute) % Clock.SecondsInMinute);
			GUILayout.EndHorizontal();

			EditorGUIUtility.labelWidth = 0;
			EditorGUILayout.LabelField("Properties", EditorStyles.boldLabel);
			clock.timeSpeed = EditorGUILayout.FloatField("Time Speed", clock.timeSpeed);

			UpdateSun();
		}

		private void UpdateSun()
		{
			SunMovement sunMovement = clock.GetComponent<SunMovement>();
			if (sunMovement != null)
				sunMovement.Validate();
		}
	}
}