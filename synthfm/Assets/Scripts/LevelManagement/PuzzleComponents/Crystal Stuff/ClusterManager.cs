using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClusterManager : MonoBehaviour
{

    [Header("Rock mode OR Sequencial Mode")]
    public PuzzleManager.Mode _Mode;        // Editor


    [Header("Engineers ONLY !")]
    [Header("DO NOT TOUCH FOllowings")]
    public bool IsComplete = false;

    public int Num_Of_Actives;     // ROCk Mode
    public int _curSeq;        // Seq Mode

    public RockIt Rock;
    public Crystal[] Crystalls;

    public int numOfCrystalls;

    private int size;

    // Start is called before the first frame update
    void Start()
    {
        size = this.transform.childCount;

        // get Rock
        if(_Mode == PuzzleManager.Mode.Rock)
        {
            Rock = this.transform.Find("Rock").gameObject.GetComponent<RockIt>();
            if (Rock == null) { Debug.Log("Error: Unable to finmd Rock"); }
        }
        else
        {
            Rock = this.transform.Find("Rock").gameObject.GetComponent<RockIt>();
            Rock.gameObject.SetActive(false);
            // destroy
        }

        // get crystalls
        Crystalls = new Crystal[size - 1];
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
        if(_Mode == PuzzleManager.Mode.Rock) // if Rock mode
        {
            // change state to active and Increment the number of active crystalls          
                i_crystal.changeToActive();
            
                 Num_Of_Actives++;
            if(Num_Of_Actives == size-1)
            {
                IsComplete = true;
                // destroy rock
                Rock.DestroyIt();
            }
            
        }
        else if(_Mode == PuzzleManager.Mode.Sequencial) // if squence mode
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
