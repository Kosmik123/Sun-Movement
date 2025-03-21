using UnityEngine;
using UnityEditor;

namespace RealisticSunMovement.Editor
{
	[CustomEditor(typeof(Clock))]
	public class ClockEditor : UnityEditor.Editor
	{
		private Clock clock;

		//public override void OnInspectorGUI()
		//{
		//	clock = target as Clock;

		//	float initialLabelWidth = EditorGUIUtility.labelWidth;
		//	EditorGUILayout.LabelField("Time", EditorStyles.boldLabel);
		//	GUILayout.BeginHorizontal();
		//	EditorGUIUtility.labelWidth = 12;
		//	clock.Hour = EditorGUILayout.IntField((clock.Hour + Clock.HoursInDay) % Clock.HoursInDay);
		//	clock.Minute = EditorGUILayout.IntField(":", (clock.Minute + Clock.MinutesInHour) % Clock.MinutesInHour);
		//	clock.Second = EditorGUILayout.IntField(":", (clock.Second + Clock.SecondsInMinute) % Clock.SecondsInMinute);
		//	GUILayout.EndHorizontal();

		//	EditorGUIUtility.labelWidth = 0;
		//	EditorGUILayout.LabelField("Properties", EditorStyles.boldLabel);
		//	clock.timeSpeed = EditorGUILayout.FloatField("Time Speed", clock.timeSpeed);
		//	EditorGUIUtility.labelWidth = initialLabelWidth;
		//}
	}
}