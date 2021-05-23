using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public class SunMovement : MonoBehaviour
{
    public Clock clock;

    [Header("Properties")]
    [Range(-90, 90)] // -90 is south, +90 is north
    public float latitude = 50; // for Poland

    [Range(-180, 180)]
    public float axialTilt = 23.4f; // for Earth

    [Range(1, 12)]
    public int month = 6;

    private Vector3 orbitAxis;
    private Vector3 polesAxis;
    private float time;

    private void Update()
    {
        time = clock.GetTime();
        CalculateRotation();
    }

    void CalculateRotation()
    {
        float yearAngle = month * 30 - 180;
        float latAngle = 90 - latitude;
        float tiltedAngle = latAngle + axialTilt;
        float dayAngle = 360 * time / (Clock.secondsInMinute * Clock.minutesInHour * Clock.hoursInDay) + 180;

        orbitAxis = Quaternion.AngleAxis(tiltedAngle, Vector3.right) * Vector3.up;
        polesAxis = Quaternion.AngleAxis(latAngle, Vector3.right) * Vector3.up;

        transform.rotation =
            Quaternion.AngleAxis(dayAngle - yearAngle, polesAxis) *
            Quaternion.AngleAxis(yearAngle, orbitAxis) *
            Quaternion.AngleAxis(tiltedAngle, Vector3.right);
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        time = clock.GetTime();
        CalculateRotation();
    }
#endif
}
