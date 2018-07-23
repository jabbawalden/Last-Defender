using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CanEditMultipleObjects]
[CustomEditor(typeof(FastDemon))]
public class FastDemonEditor : Editor
{ 
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        //casts target to DemonController
        FastDemon enemy = (FastDemon)target;
        if (GUILayout.Button("Generate ID"))
        {
            enemy.enemyID = System.Guid.NewGuid().ToString();
            EditorUtility.SetDirty(enemy);
        }

    }
}

