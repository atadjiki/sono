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
    public Navpoint navPoint;

    public Hub[] hubList;
    private Queue<Hub> hubs;

    private Hub currentHub;
    private Puzzle currentPuzzle;
    private bool puzzlesComplete;
    private bool levelComplete = false;
    public GameObject finalZone;


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
        if(navPoint != null)
        {
            Debug.Log("Found nav point " + navPoint.name);
        }
       
        hubs = new Queue<Hub>(hubList);
        if(hubs.Count > 0)
        {
            currentHub = hubs.Dequeue();
        }
        
      //  Debug.Log("Found " + hubs.Count + " hubs");
        if(currentHub != null)
        {
            Debug.Log("First hub has " + currentHub.puzzleList.Length + " puzzles");
            currentPuzzle = currentHub.nextPuzzle();
            Debug.Log("Current puzzle: " + currentPuzzle.name);
        }
        
        if(navPoint != null)
        {
            updateNavPoint();
        }


    }

    private void Update()
    {

        if (!puzzlesComplete)
        {
            //if the current hub is completed
            if (currentHub != null && !currentHub.getStatus())
            {
                //if the current puzzle in the hub is completed
                if (currentPuzzle != null && currentPuzzle.GetStatus())
                {
                //move to next puzzle
                   currentPuzzle = currentHub.nextPuzzle();
                    

                    //if there is no next puzzle, move to next hub
                    if (currentPuzzle == null)
                    {
                        currentHub = NextHub();
                        if(currentHub == null)
                        {
                            Debug.Log("Level complete!");
                            puzzlesComplete = true;
                        }
                        else
                        {
                            currentPuzzle = currentHub.nextPuzzle();
                            Debug.Log("Current puzzle: " + currentPuzzle.name);
                        }
                    }

                    if (navPoint != null)
                    {
                        updateNavPoint();
                    }

                }
            }
            else
            {
                currentHub = NextHub();
                if (currentHub == null)
                {
                    puzzlesComplete = true;
                    if (navPoint != null)
                    {
                        updateNavPoint();
                    }
                }
                else
                {
                    currentPuzzle = currentHub.nextPuzzle();
                    Debug.Log("Current puzzle: " + currentPuzzle.name);
                }
            }
        }
        
    }

    public GameObject getPlayer()
    {
        return player;
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
        if (currentPuzzle == null && !puzzlesComplete)
        {
          // navPoint.active = false;
        }
        else if(currentPuzzle == null && puzzlesComplete)
        {
            Debug.Log("No target");
            puzzlesComplete = true;
            if(navPoint != null)
            {
              //  navPoint.target = finalZone;
            }

        }
        else if(currentPuzzle != null && !puzzlesComplete)
        {
            Debug.Log("New Target - " + currentPuzzle);
          //  navPoint.target = currentPuzzle.gameObject;
           // navPoint.active = true;
        }
        if (levelComplete)
        {
           // navPoint.active = false;
        }
    }
    public void Completed()
    {
        levelComplete = true;
    }
}
