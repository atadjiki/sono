using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
    using UnityEditor;
#endif


#if UNITY_EDITOR
    public class PuzzleSelectPopup : EditorWindow
    {

        private static List<GameObject> puzzleTypes = new List<GameObject>();
        public int index = 0;

      //  [MenuItem("Select Puzzle Type")]
        static void Init()
        {

            LoadPuzzleTypes();


            EditorWindow window = GetWindow(typeof(PuzzleSelectPopup));
            window.Show();

        }

        static void LoadPuzzleTypes()
        {
            Object[] loadedObjects = Resources.LoadAll<GameObject>("Prefabs/Puzzles");
            foreach (Object obj in loadedObjects)
            {
                Debug.Log(obj.name);
                puzzleTypes.Add((GameObject)obj);
            }

            Debug.Log("Found " + loadedObjects.Length + " objects");
        }

        static string[] getStringArray(List<GameObject> list)
        {
            string[] result = new string[list.Count];

            for(int i = 0; i < list.Count; i++)
            {
                result[i] = list[i].name;
            }

            return result;
        }

        void OnGUI()
        {
            index = EditorGUILayout.Popup(index, getStringArray(puzzleTypes));
            if (GUILayout.Button("Create"))
                InstantiatePrimitive();
        }

        void InstantiatePrimitive()
        {
            switch (index)
            {
                case 0:
                    GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    cube.transform.position = Vector3.zero;
                    break;
                case 1:
                    GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    sphere.transform.position = Vector3.zero;
                    break;
                case 2:
                    GameObject plane = GameObject.CreatePrimitive(PrimitiveType.Plane);
                    plane.transform.position = Vector3.zero;
                    break;
                default:
                    Debug.LogError("Unrecognized Option");
                    break;
            }
        }
    }
#endif


