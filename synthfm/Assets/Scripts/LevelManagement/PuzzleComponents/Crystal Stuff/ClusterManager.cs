using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClusterManager : Puzzle
{
    public enum State { OFF, ON, Error };

    public enum Mode { Rock, Sequencial };

    [Header("Rock mode OR Sequencial Mode")]
    public Mode _Mode;        // Editor

    [Header("The number of rocks only for Rock mode")]
    public int _NumOfRocks;

    [Header("Engineers ONLY !")]
    [Header("DO NOT TOUCH FOllowings")]
    public bool IsComplete = false;
    
    private int Num_Of_Actives;     // ROCk Mode
    private int _curSeq;        // Seq Mode

    private RockIt[] Rocks;
    private Crystal[] Crystalls;

    private int numOfCrystalls;
    private int size;

    Vector3 initPos;

    // Start is called before the first frame update
    void Start()
    {
        initPos = fragment.transform.position;
        fragment.transform.position = new Vector3(initPos.x,initPos.y,2);
        size = this.transform.childCount;
        IsComplete = false;
        // get Rock
        if(_Mode == ClusterManager.Mode.Rock)
        {
            Crystalls = new Crystal[size - _NumOfRocks];
            Rocks = new RockIt[_NumOfRocks];
            for (int i = 0; i < Rocks.Length; i++)
            {
                Rocks[i] = this.transform.GetChild(i).gameObject.GetComponent<RockIt>();
                if (Rocks[i] == null) { Debug.Log("Error: Unable to find Rock"); }
            }
        }
        else
        {
            Crystalls = new Crystal[size - 1];
            Rocks = new RockIt[1];
            Rocks[0] = this.transform.Find("Rock").gameObject.GetComponent<RockIt>();
           
            Rocks[0].gameObject.SetActive(false);
            
        }

        // get crystalls
      
        for(int i=0; i< Crystalls.Length; i++)
        {
           Crystalls = this.transform.GetComponentsInChildren<Crystal>();
          //  Crystalls[i] = this.transform.GetChild(i + 1).gameObject.GetComponent<Crystal>();
        }

        _curSeq = 1; // Seq starts from zero
    }

    // called from colorIt .. do based on modes
    public void _Notify(Crystal i_crystal)
    {
        if(_Mode == ClusterManager.Mode.Rock) // if Rock mode
        {
            // change state to active and Increment the number of active crystalls          
                i_crystal.changeToActive();
            
                 Num_Of_Actives++;
            if (Num_Of_Actives == Crystalls.Length)
            {
                IsComplete = true;
                PuzzleProgressManager.instance.NotifyCount(PuzzleProgressManager.World.Amber);
                //// destroy rock -- Arash - keep rocks for now until we delete puzzles and drop artifact
                foreach (RockIt R in Rocks) // puzzle complete
                {
                    R.ActivateIt();
                    IsComplete = true;

                    fragment.transform.position = new Vector3(initPos.x, initPos.y, 0);
                }
            }
            
        }
        else if(_Mode == ClusterManager.Mode.Sequencial) // if squence mode
        {
            if (i_crystal.sequenceNo == _curSeq)
            {
                // check the sequence number and activate 
                i_crystal.changeToActive();
                _curSeq++;

                // completed ?
                if(_curSeq == size)
                {
                    IsComplete = true;
                   
                }
            }
            // or deactivate each and reset the seq no
            else
            {
                foreach (Crystal cry in Crystalls)
                {
                    cry.changeToFail();
                }
                _curSeq = 1;
            }
            
        }
        else
        {
            Debug.Log("Invalid Mode Notification");
        }

    }


    public void _NotifyFromROck(RockIt i_rock)
    {
        // change all to fail
        foreach(Crystal cry in Crystalls)
        {
            cry.changeToFail();
        }

        // reset active num to zero
        Num_Of_Actives = 0;
    }
}
