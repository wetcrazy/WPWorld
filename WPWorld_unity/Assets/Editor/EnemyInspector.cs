using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(Enemy))]
public class EnemyInspector : Editor {

	public override void OnInspectorGUI()
	{
		serializedObject.Update();

		// TO DO, MAKE THIS SERIALIZABLE, AKA, SAVES THE DATA THE PLAYER PUTS IN
		// IDEAS, SET DIRTY & APPLY MODIFIED PROPERTIES?
		var EnemyRef = target as Enemy;

		EnemyRef.CurrType = (ENEMYTYPES)EditorGUILayout.EnumPopup("Enemy Type", EnemyRef.CurrType);

		if (EnemyRef.CurrType != ENEMYTYPES.DEAD)
		{
			EnemyRef.MovementSpeed = EditorGUILayout.FloatField("Movement Speed", EnemyRef.MovementSpeed);
		}
		else
			EditorGUILayout.LabelField("It's dead, Leave it alone");

		if (EnemyRef.CurrType != ENEMYTYPES.DEAD
			&& EnemyRef.CurrType != ENEMYTYPES.WALK
			&& EnemyRef.CurrType != ENEMYTYPES.PATROL)
		{
			EnemyRef.JumpSpeed = EditorGUILayout.FloatField("Jump Speed", EnemyRef.JumpSpeed);
		}

		if(EnemyRef.CurrType == ENEMYTYPES.PATROL
			|| EnemyRef.CurrType == ENEMYTYPES.PATROLJUMP
			|| EnemyRef.CurrType == ENEMYTYPES.HIDDENPATROLJUMP)
		{
			EnemyRef.PatrolPointA = EditorGUILayout.Vector3Field("Patrol Point A", EnemyRef.PatrolPointA);
			EnemyRef.PatrolPointB = EditorGUILayout.Vector3Field("Patrol Point B", EnemyRef.PatrolPointB);
		}

		if (EnemyRef.CurrType != ENEMYTYPES.DEAD)
		{
			EnemyRef.IsImmortal = EditorGUILayout.Toggle("Is Immortal", EnemyRef.IsImmortal);

			if (!EnemyRef.IsImmortal)
			{
				EnemyRef.ScoreAmount = EditorGUILayout.IntField("Score Amount", EnemyRef.ScoreAmount);
				EnemyRef.ScoreUI = (GameObject)EditorGUILayout.ObjectField("Score UI", EnemyRef.ScoreUI, typeof(GameObject), true);
			}
		}

		if(GUI.changed)
		{
			EditorUtility.SetDirty(EnemyRef);
			serializedObject.ApplyModifiedProperties();
		}
	}
}
