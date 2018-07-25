using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CanEditMultipleObjects]
[CustomEditor(typeof(RangeDemon))]
public class RangeDemonEditor : Editor {

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        //casts target to DemonController
        RangeDemon enemy3 = (RangeDemon)target;
        if (GUILayout.Button("Generate ID"))
        {
            enemy3.enemyID = System.Guid.NewGuid().ToString();
            EditorUtility.SetDirty(enemy3);
        }

    }
}

