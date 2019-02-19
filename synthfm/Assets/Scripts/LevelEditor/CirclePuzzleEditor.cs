using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(CirclePuzzle), true)]
public class CirclePuzzleEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        CirclePuzzle myScript = (CirclePuzzle)target;
        if (GUILayout.Button("Build Puzzle"))
        {
            myScript.DoSetup();
        }
    }
}
