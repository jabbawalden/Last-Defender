using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CanEditMultipleObjects]
[CustomEditor(typeof(DemonController))]
public class DemonControllerEditor : Editor {

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        //casts target to DemonController
        DemonController demonController = (DemonController)target;
        if (GUILayout.Button("Generate ID"))
        {
            demonController.enemyID = System.Guid.NewGuid().ToString();
            EditorUtility.SetDirty(demonController);
        }

    }

}
