using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;


    public GameObject player;
    [HideInInspector]
    public AudioSource playerAudioSource;

    public GameObject fragmentPrefab;
    public AudioClip[] audioFragments;
    public GameObject visualizerRing;

    private Puzzle currentPuzzle;
    private bool puzzlesComplete;
    private bool levelComplete = false;

    private List<Puzzle> hubPuzzles; 
    public GameObject finalZone;


    private void Awake()
    {
        if(SavedData.instance == null)
        {
            SavedData.instance = new SavedData();
        }

        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {

        playerAudioSource = player.GetComponent<AudioSource>();
        playerAudioSource.clip = audioFragments[0];
        playerAudioSource.Play();

        setuphubPuzzles();
    }


    public GameObject getPlayer()
    {
        return player;
    }


    public void Completed()
    {
        levelComplete = true;
    }

    private void setuphubPuzzles()
    {
        hubPuzzles = new List<Puzzle>();
        hubPuzzles.Clear();
        hubPuzzles.AddRange(FindObjectsOfType<Puzzle>());

        SavedData.instance.hubLevels = hubPuzzles;

        string json = JsonUtility.ToJson(SavedData.instance);
        PlayerPrefs.SetString("SavedData", json);



    }
}
