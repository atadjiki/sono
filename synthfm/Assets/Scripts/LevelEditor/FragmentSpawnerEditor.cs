using UnityEngine;
using System.Collections;
#if UNITY_EDITOR
using UnityEditor;
#endif


#if UNITY_EDITOR
[CustomEditor(typeof(FragmentSpawner))]
public class FragmentSpawnerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        FragmentSpawner myScript = (FragmentSpawner)target;
        if (GUILayout.Button("Spawn Fragment"))
        {
            myScript.SpawnFragment();
        }
    }
}
#endif