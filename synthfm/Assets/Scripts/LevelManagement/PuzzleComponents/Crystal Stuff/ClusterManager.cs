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
 
    Vector3 initPos;

    // Start is called before the first frame update
    void Start()
    {
        initPos = fragment.transform.position;
        fragment.transform.position = new Vector3(initPos.x,initPos.y,2);
        IsComplete = false;
        // get Rock
        if(_Mode == ClusterManager.Mode.Rock)
        {
            Rocks = new RockIt[_NumOfRocks];
            for (int i = 0; i < Rocks.Length; i++)
            {
                Rocks[i] = this.transform.GetChild(i).gameObject.GetComponent<RockIt>();
                if (Rocks[i] == null) { Debug.Log("Error: Unable to find Rock"); }
            }
        }
        Crystalls = transform.GetComponentsInChildren<Crystal>();

    }

    // called from colorIt .. do based on modes
    public void _Notify(bool iState)
    {
        if(_Mode == ClusterManager.Mode.Rock) // if Rock mode
        {
            if (iState)
            {
                Num_Of_Actives++;
            }
            else
            {
                Num_Of_Actives--;
            }

            if (Num_Of_Actives == Crystalls.Length)  // puzzle complete
            {
                IsComplete = true;
                //// destroy rock -- Arash - keep rocks for now until we delete puzzles and drop artifact
                foreach (RockIt R in Rocks) 
                {
                    R.ActivateIt();
                    R.gameObject.GetComponent<Collider2D>().enabled = false;
                    
                }

                // change crystal variable
                foreach(Crystal cr in Crystalls )
                {
                    cr.IsPuzzleComplete = true;
                }
            }
            
        }
        else
        {
            Debug.Log("Invalid Mode Notification");
        }

    }


    // Not using now
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
