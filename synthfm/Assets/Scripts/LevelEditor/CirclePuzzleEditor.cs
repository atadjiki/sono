using UnityEngine;
using System.Collections;
#if UNITY_EDITOR
    using UnityEditor;
#endif


#if UNITY_EDITOR
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
#endif