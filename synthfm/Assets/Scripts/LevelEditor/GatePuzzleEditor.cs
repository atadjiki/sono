using UnityEngine;
using System.Collections;
#if UNITY_EDITOR
    using UnityEditor;
#endif


#if UNITY_EDITOR
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
#endif