using UnityEngine;
using System.Collections;

#if UNITY_EDITOR
    using UnityEditor;
#endif

#if UNITY_EDITOR

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
#endif
