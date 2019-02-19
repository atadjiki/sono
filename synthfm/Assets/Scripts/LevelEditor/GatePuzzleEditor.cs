using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(GatePuzzle), true)]
public class GatePuzzleEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        GatePuzzle myScript = (GatePuzzle)target;
        if (GUILayout.Button("Build Puzzle"))
        {
            myScript.DoSetup();
        }
    }
}
