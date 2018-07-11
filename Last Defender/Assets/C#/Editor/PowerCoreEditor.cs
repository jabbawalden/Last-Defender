using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CanEditMultipleObjects]
[CustomEditor(typeof(PowerCore))]
public class PowerCoreEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        PowerCore powerCore = (PowerCore)target;
        if (GUILayout.Button("Generate ID"))
        {
            powerCore.powerCoreID = System.Guid.NewGuid().ToString();
            EditorUtility.SetDirty(powerCore);
        }
    }

}
