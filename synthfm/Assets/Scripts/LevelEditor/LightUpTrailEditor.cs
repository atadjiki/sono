using UnityEngine;
using System.Collections;
#if UNITY_EDITOR
using UnityEditor;
#endif

#if UNITY_EDITOR
[CustomEditor(typeof(LightUpTrail))]
public class LightUpTrailEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        LightUpTrail myScript = (LightUpTrail)target;
        if (GUILayout.Button("Build Trail"))
        {
            myScript.Setup();
        }
    }
}
#endif