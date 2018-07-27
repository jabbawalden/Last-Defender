using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CanEditMultipleObjects]
[CustomEditor(typeof(RepairKit))]
public class RepairKitEditor : Editor {

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        RepairKit repairKit = (RepairKit)target;
        if (GUILayout.Button("Generate ID"))
        {
            repairKit.repairKitID = System.Guid.NewGuid().ToString();
            EditorUtility.SetDirty(repairKit);
        }
    }
}
