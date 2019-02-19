using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(ProceduralGatePuzzle), true)]
public class ProceduralGateEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        ProceduralGatePuzzle myScript = (ProceduralGatePuzzle)target;
        if (GUILayout.Button("Build Puzzle"))
        {
            myScript.DoSetup();
        }
    }
}
