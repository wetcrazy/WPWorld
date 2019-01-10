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

        ButtonRef.CurrType = (BUTTONTYPE)EditorGUILayout.EnumPopup("Button Type", ButtonRef.CurrType);

        if(ButtonRef.CurrType == BUTTONTYPE.TIMER)
        {
            ButtonRef.TimeToReset = EditorGUILayout.FloatField("Time To Reset", ButtonRef.TimeToReset);
        }

        ButtonRef.ButtonSFX = EditorGUILayout.TextField("Button SFX", ButtonRef.ButtonSFX);

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