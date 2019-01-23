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
    private Navpoint navPoint;

    public Hub[] hubList;
    private Queue<Hub> hubs;

    private Hub currentHub;
    private Puzzle currentPuzzle;
    private bool levelComplete;


    private void Awake()
    {
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

        //initialize vars
        navPoint = GetComponent<Navpoint>();
        Debug.Log("Found nav point " + navPoint.name);
        hubs = new Queue<Hub>(hubList);
        currentHub = hubs.Dequeue();
        currentPuzzle = currentHub.nextPuzzle();

        updateNavPoint();

    }

    private void Update()
    {
        if (!levelComplete)
        {
            //check if the current puzzle has been completed
            if (currentPuzzle != null && currentPuzzle.GetStatus())
            {
                //check if the hub has any more puzzles

                if (currentHub.getStatus())
                {
                    currentPuzzle = currentHub.nextPuzzle();
                    //go to next hub if this one is done
                    NextHub();
                    Debug.Log("Moving to next hub");
                    currentPuzzle = currentHub.nextPuzzle(); //get the first puzzle in the hub
                }
                else
                {
                    currentPuzzle = currentHub.nextPuzzle(); //if the hub still has puzzles, move to next one
                }

                updateNavPoint();
            }
        }
        else
        {

        }
        
    }

    Hub NextHub()
    {
        if(hubs.Count == 0)
        {
            return null;
        }
        else
        {
            return hubs.Dequeue();
        }
        
    }

    void updateNavPoint()
    {
        if(currentPuzzle == null)
        {
            navPoint.active = false;
        }
        else
        {
            navPoint.target = currentPuzzle.gameObject;
        }
        
    }
}
