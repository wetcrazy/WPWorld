using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LeverScript))]
public class LeverInspector : Editor {

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        var LeverRef = target as LeverScript;

        LeverRef.IsButton = EditorGUILayout.Toggle("Is Button", LeverRef.IsButton);
        
        if(LeverRef.IsButton)
        {
            LeverRef.ButtonTimeDelay = EditorGUILayout.FloatField("Time Delay", LeverRef.ButtonTimeDelay);
        }

        if (GUI.changed)
        {
            EditorUtility.SetDirty(LeverRef);
            serializedObject.ApplyModifiedProperties();
        }
    }
}
