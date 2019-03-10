using UnityEngine;
using System.Collections;
#if UNITY_EDITOR
    using UnityEditor;
#endif

#if UNITY_EDITOR
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
#endif