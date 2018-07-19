using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CanEditMultipleObjects]
[CustomEditor(typeof(StrongDemon))]
public class DemonControllerEditor : Editor {

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        //casts target to DemonController
        StrongDemon enemy = (StrongDemon)target;
        if (GUILayout.Button("Generate ID"))
        {
            enemy.enemyID = System.Guid.NewGuid().ToString();
            EditorUtility.SetDirty(enemy);
        }

    }

}
