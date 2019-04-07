using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Persist : MonoBehaviour
{
    private List<Puzzle> fiberPuzzles;
    private List<FragmentController> fiberFragments;

    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        SetupFiberPuzzles();
        SetupFiberFragments();
    }

    private void SetupFiberFragments()
    {
        fiberFragments = new List<FragmentController>();
        fiberFragments.Clear();
        fiberFragments.AddRange(FindObjectsOfType<FragmentController>());
        SavedData.instance.fiberFragments = fiberFragments;
        string json = JsonUtility.ToJson(SavedData.instance);
        PlayerPrefs.SetString("SavedData", json);

        print(json);

    }

    private void SetupFiberPuzzles()
    {
        fiberPuzzles = new List<Puzzle>();
        fiberPuzzles.Clear();
        fiberPuzzles.AddRange(FindObjectsOfType<Puzzle>());
        SavedData.instance.fiberLevels = fiberPuzzles;
        string json = JsonUtility.ToJson(SavedData.instance);
        PlayerPrefs.SetString("SavedData", json);

        print(json);
    }
}
