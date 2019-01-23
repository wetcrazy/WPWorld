using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ButtonScript))]
public class ButtonInspector : Editor
{
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        var ButtonRef = target as ButtonScript;

        EditorGUILayout.LabelField("ID Settings", EditorStyles.boldLabel);
        ButtonRef.ID = EditorGUILayout.IntField("ID", ButtonRef.ID);

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Button Settings", EditorStyles.boldLabel);
        ButtonRef.CurrType = (BUTTONTYPE)EditorGUILayout.EnumPopup("Button Type", ButtonRef.CurrType);

        if(ButtonRef.CurrType == BUTTONTYPE.TOGGLE || ButtonRef.CurrType == BUTTONTYPE.TIMER)
        {
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Animation Settings", EditorStyles.boldLabel);
            ButtonRef.ButtonSpeed = EditorGUILayout.FloatField("Button Speed", ButtonRef.ButtonSpeed);
        }

        if (ButtonRef.CurrType == BUTTONTYPE.TIMER)
        {
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Timer Settings", EditorStyles.boldLabel);
            ButtonRef.TimeToReset = EditorGUILayout.FloatField("Time To Reset", ButtonRef.TimeToReset);
        }

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Sound Settings", EditorStyles.boldLabel);
        ButtonRef.ButtonSFX = EditorGUILayout.TextField("Button SFX", ButtonRef.ButtonSFX);

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Object Settings", EditorStyles.boldLabel);
        SerializedProperty tps = serializedObject.FindProperty("ObjectsToChange");
        EditorGUI.BeginChangeCheck();
        EditorGUILayout.PropertyField(tps, true);
        if (EditorGUI.EndChangeCheck())
            serializedObject.ApplyModifiedProperties();

        if (GUI.changed)
        {
            EditorUtility.SetDirty(ButtonRef);
            serializedObject.ApplyModifiedProperties();
        }
    }
}