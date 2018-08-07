using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CanEditMultipleObjects]
[CustomEditor(typeof(EnemyGroupTrigger))]
public class EnemyTriggerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        EnemyGroupTrigger enemyGroupTrigger = (EnemyGroupTrigger)target;
        if (GUILayout.Button("Generate ID"))
        {
            enemyGroupTrigger.enemyGroupTriggerID = System.Guid.NewGuid().ToString();
            EditorUtility.SetDirty(enemyGroupTrigger);
        }
    }

}
