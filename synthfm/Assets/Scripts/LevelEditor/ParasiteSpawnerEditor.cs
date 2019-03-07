using UnityEngine;
using System.Collections;
#if UNITY_EDITOR
    using UnityEditor;
#endif


#if UNITY_EDITOR
    [CustomEditor(typeof(ParasiteSpawner))]
    public class ParasiteSpawnerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            ParasiteSpawner myScript = (ParasiteSpawner)target;
            if (GUILayout.Button("Kill Parasites"))
            {
                myScript.KillParasites();
            }
        }
    }
#endif