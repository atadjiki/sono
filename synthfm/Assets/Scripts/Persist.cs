using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Persist : MonoBehaviour
{
    private List<Puzzle> fiberPuzzles;
    private List<Puzzle> amberPuzzles;
    private List<FragmentController> fiberFragments;
    private List<FragmentController> amberFragments;

    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        if(GameObject.Find("FiberWorld"))
        {
            SetupFiberPuzzles();
            SetupFiberFragments();
        }
        else if(GameObject.Find("AmberWorld"))
        {
            SetupAmberPuzzles();
            SetupAmberFragments();
        }


    }

    private void SetupAmberPuzzles()
    {
        amberPuzzles = new List<Puzzle>();
        amberPuzzles.Clear();
        amberPuzzles.AddRange(FindObjectsOfType<Puzzle>());
        SavedData.instance.amberLevels = amberPuzzles;
        string json = JsonUtility.ToJson(SavedData.instance);
        PlayerPrefs.SetString("SavedData", json);
    }

    private void SetupAmberFragments()
    {
        amberFragments = new List<FragmentController>();
        amberFragments.Clear();
        amberFragments.AddRange(FindObjectsOfType<FragmentController>());
        SavedData.instance.amberFragments = amberFragments;
        string json = JsonUtility.ToJson(SavedData.instance);
        PlayerPrefs.SetString("SavedData", json);
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
