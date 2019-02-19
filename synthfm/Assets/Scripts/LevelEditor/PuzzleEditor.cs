using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(Puzzle), true)]
public class PuzzleEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Puzzle myScript = (Puzzle)target;
        if (GUILayout.Button("Build Puzzle"))
        {
            myScript.DoSetup();
        }
    }
}
