using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CanEditMultipleObjects]
[CustomEditor(typeof(AmmoPack))]
public class AmmoBoxEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        AmmoPack ammoPack = (AmmoPack)target;
        if (GUILayout.Button("Generate ID"))
        {
            ammoPack.ammoID = System.Guid.NewGuid().ToString();
            EditorUtility.SetDirty(ammoPack);
        }
    }

}
