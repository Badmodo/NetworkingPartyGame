using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MinMaxSliderInEditor : Editor
{
    private SerializedProperty pRigidbody;
    private SerializedProperty pMin;
    private SerializedProperty pMax;

    void OnEnable()
    {
        pRigidbody = serializedObject.FindProperty("rigidbody");
        pMin = serializedObject.FindProperty("min");
        pMax = serializedObject.FindProperty("max");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(pRigidbody);

        float min = pMin.floatValue;
        float max = pMax.floatValue;
        EditorGUILayout.MinMaxSlider("Range", ref min, ref max, 0, 1);

        pMin.floatValue = min;
        pMax.floatValue = max;

        serializedObject.ApplyModifiedProperties();
    }
}
