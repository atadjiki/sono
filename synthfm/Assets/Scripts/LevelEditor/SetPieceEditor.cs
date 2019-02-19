using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(SetPiece))]
public class SetPieceEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        SetPiece myScript = (SetPiece)target;
        if (GUILayout.Button("Build Set Piece"))
        {
            myScript.DoSetup();
        }
    }
}
