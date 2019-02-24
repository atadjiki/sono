using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PuzzleManager : MonoBehaviour
{

    public enum State { OFF, ON, Error };

    public enum Mode { Rock, Sequencial};

    [Header("Number of Clusters")]
    public int N;       // Editor

    [Header("Rock mode OR Sequencial Mode")]
    [Header("Enter size as equals to N")]
    public Mode[] _Mode;        // Editor

    [Header("Drag Clusters Objects Here")]
    public GameObject Obj_Clusters_Parent;

    [Header("Cluster Parents (Each Puzzle)")]
    public GameObject[] Obj_clusters; // based on N

    [Header("Indiacte if the puzzle is complete OR Not")]
    public bool[] IsComplete; // based on N

    [Header("The Number of Crystals active Now")]
    public int[] _activeCrystalls; // based on N
                                        //if int[i] is equal to number of crystalls int the puzzle,
                                               // then puzzle[i] is complete
    
    private static int size;
    private ColorIt _colorIt;

    private RockIt[] _rocks = new RockIt[4];

    // Start is called before the first frame update
    void Start()
    {
        size = N;
        Obj_clusters = new GameObject[size];
        IsComplete = new bool[size];

        // get all rocks
        for (int i = 0; i < 4; i++)
        {

            //_rocks[i] = clusterParent[i].transform.GetChild(0).GetComponent<RockIt>();
        }
        
    }

    public void Notify(NotifierP i_notifier)
    {
        int seq = i_notifier.seqNo;   // check sequencce number // very Importanat

        // if it is rock then disable all
        if (i_notifier.gameObject.name == "Rock")
        {
            
            for (int i=1; i<clusterParent[seq].transform.childCount; i++)       // deactivate all crystalls
            {   //0th is Rock ALWAYS
                _colorIt = clusterParent[seq].transform.GetChild(i).GetComponent<ColorIt>();
                _colorIt.changeToFail();
            }

            // reset active numbers to zero
            _activeCrystalls[seq] = 0;
            
        }
        else
        {
            // increment the number of active crystals
            _activeCrystalls[seq]++;

            _colorIt = i_notifier.gameObject.transform.GetComponent<ColorIt>();
            _colorIt.changeToActive();

            // if puzzle is complete
            if (_activeCrystalls[seq] == 9)
            {
                // destroy rock
                RockIt _rock = _rocks[seq];
                _rock.DestroyIt();

                // chnage puzzle state
                IsComplete[seq] = true;
            }
        }
        
        // check if all activated then set puzzle complete = true


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
