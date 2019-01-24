using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    private GameObject player;
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
    public GameObject center;


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
        player = GameObject.Find("Player");

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
            //if the current hub is completed
            if (!currentHub.getStatus())
            {
                //if the current puzzle in the hub is completed
                if (currentPuzzle.GetStatus())
                {
                //move to next puzzle
                   currentPuzzle = currentHub.nextPuzzle();
                
                    //if there is no next puzzle, move to next hub
                    if(currentPuzzle == null)
                    {
                        currentHub = NextHub();
                        if(currentHub == null)
                        {
                            Debug.Log("Level complete!");
                            levelComplete = true;
                            navPoint.target = center;
                        }
                        else
                        {
                            currentPuzzle = currentHub.nextPuzzle();
                        }
                    }

                    updateNavPoint();
   
                }
            }
            else
            {
                currentHub = NextHub();
                if (currentHub == null)
                {
                    levelComplete = true;
                    navPoint.target = center;
                }
                else
                {
                    currentPuzzle = currentHub.nextPuzzle();
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
        if (currentPuzzle == null && !levelComplete)
        {
            navPoint.active = false;
        }
        else if(currentPuzzle == null && levelComplete)
        {
            Debug.Log("New target " + center.name);
            levelComplete = true;
            navPoint.target = center;
        }
        else
        {
            navPoint.active = false;
        }
        
    }
}
