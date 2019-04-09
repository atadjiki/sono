using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClusterManager : MonoBehaviour
{
    public enum State { OFF, ON, Error };

    public enum Mode { Rock, Sequencial };

    [Header("Rock mode OR Sequencial Mode")]
    public Mode _Mode;        // Editor

    [Header("Engineers ONLY !")]
    [Header("DO NOT TOUCH FOllowings")]
    public bool IsComplete = false;

    [Header("The number of rocks only for Rock mode")]
    public int _NumOfRocks;

    public int Num_Of_Actives;     // ROCk Mode
    public int _curSeq;        // Seq Mode

    public RockIt[] Rocks;
    public Crystal[] Crystalls;

    public int numOfCrystalls;
    public int size;

    // Start is called before the first frame update
    void Start()
    {
        size = this.transform.childCount;

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
            if(Num_Of_Actives == Crystalls.Length)
            {
                IsComplete = true;
                // destroy rock
                foreach (RockIt R in Rocks)
                    R.DestroyIt();
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
