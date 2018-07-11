using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CanEditMultipleObjects]
[CustomEditor(typeof(HealthPack))]
public class HealthPackEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        HealthPack healthPack = (HealthPack)target;
        if (GUILayout.Button("Generate ID"))
        {
            healthPack.healthPackID = System.Guid.NewGuid().ToString();
            EditorUtility.SetDirty(healthPack);
        }
    }

}
