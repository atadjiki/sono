using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * An example gate puzzle inheriting from Puzzle.cs
 * The player can enter the gate system at any point, but must complete
 * the puzzle in sequential order from then on.
 * On completion, the puzzle will lower its force field 
 * and allow the player to collect a fragment
 */ 
public class GatePuzzle : Puzzle
{
    public float timeUntilReset = 10;
    private float timeLeft = 0;
    private bool timer = false;
    public List<GateTrigger> gates;
    private List<GateTrigger> currentList;
    private int currentIndex;
    private bool inProgress;
    public bool useTimer = false;
    public bool inOrder = true;
    private int gatesHit = 0;// for out of order puzzles

    public new void DoSetup()
    {
        base.DoPuzzleSetup();

    }

    private void Start()
    {
    
        currentList = gates;
        currentIndex = 0;
        inProgress = false;
        SetStatus(false);
    }

    private void Update()
    {

        timeLeft -= Time.deltaTime;
        if (timeLeft < 0 && timer && useTimer)
        {
            ResetPuzzle();
        }


        if (gates.Count == 0)
        {
            Debug.Log("Gate puzzle complete!");
            DeleteGates();
            SetStatus(complete);
        }
    }

    void DoGatesInOrder(GateTrigger trigger)
    {
        if (!complete)
        {
            //check which gate was triggered

            if (gates.Contains(trigger))
            {

                int index = currentList.IndexOf(trigger);
                //if the start index has not been set
                if (!inProgress)
                {
                    Debug.Log("Puzzle started at index " + index);

                    Debug.Log("Gate " + index + " hit");

                    UpdateList(index); //re-order the list around the new starting index
                    Debug.Log("Current list: " + PrintGateList());
                    trigger.PlayAudioClip(AssetManager.instance.gateTones[currentIndex]);
                    inProgress = true;

                    StartTimer();

                }
                else
                {
                    //if the player is touching the gates in order, increment 
                    Debug.Log("Gate " + index + " hit");
                    currentIndex++;

                    if (index == currentIndex)
                    {
                        trigger.PlayAudioClip(AssetManager.instance.gateTones[currentIndex]);
                        Debug.Log("Index " + index + " Current Index " + currentIndex + " Gate Length " + (currentList.Count - 1));
                        if (index == currentList.Count-1)
                        {
                            Debug.Log("Complete!");
                            CompletePuzzle();
                        }
                    }
                    else
                    {
                        //if this gate was triggered in the wrong order, reset the puzzle
                        trigger.PlayAudioClip(AssetManager.instance.gateTones[6]);
                        ResetPuzzle();
                    }
                }

            }
        }
    }

    void DoGatesOutOfOrder(GateTrigger trigger)
    {
        if (!complete)
        {
            //check which gate was triggered

            if (gates.Contains(trigger))
            {

                int index = currentList.IndexOf(trigger);
                //if the start index has not been set
                if (!inProgress)
                {
                    Debug.Log("Puzzle started at index " + index);
                    Debug.Log("Gate " + index + " hit");


                    trigger.PlayAudioClip(AssetManager.instance.gateTones[currentIndex]);
                    inProgress = true;
                    gatesHit++;
                    Debug.Log(gates.Count - gatesHit + " gates left!");
                    StartTimer();

                }
                else
                {
                    //if the player touched another gate, update accordingly 
                    Debug.Log("Gate " + index + " hit");
                    gatesHit++;
                    Debug.Log(gates.Count - gatesHit + " gates left!");
                    trigger.PlayAudioClip(AssetManager.instance.gateTones[currentIndex]);
                    if (gatesHit == currentList.Count)
                    {
                        CompletePuzzle();
                    }

                }

            }
        }
    }

    public void CompletePuzzle()
    {
        Debug.Log("Gate puzzle complete!");
        complete = true;
        DeleteGates();
        SetStatus(complete);
        timer = false;
    }

    public override void GateTriggered(GateTrigger trigger)
    {

        //check if we only have one gate
        if(gates.Count == 1)
        {
            CompletePuzzle();
        }

        if (inOrder)
        {
            DoGatesInOrder(trigger);
        }
        else
        {
            DoGatesOutOfOrder(trigger);
        }
    }

    /*
     * Split the current list of gates based on what index 
     * the player happened to enter. 
     */
    void UpdateList(int index)
    {
        currentList = new List<GateTrigger>(gates.Count);
        if (gates.Count % 2 == 0)
        {
            currentList.AddRange(gates.GetRange(index, gates.Count - index));
            currentList.InsertRange(gates.Count - index, gates.GetRange(0, index-1));
        }
        else
        {
            currentList.AddRange(gates.GetRange(index, gates.Count - index));
            currentList.InsertRange(gates.Count - index, gates.GetRange(0, index));
        }

    }

    void DeleteGates()
    {
        foreach(GateTrigger gate in gates)
        {
            Destroy(gate.gameObject); //TODO: Replace this with something more elegant :^)
        }

        base.ReleaseCage();
    }

    void StartTimer()
    {
        timeLeft = timeUntilReset;
        timer = true;

    }

    void ResetPuzzle()
    {
        Debug.Log("Puzzle reset");
        currentIndex = 0;
        UpdateList(currentIndex);
        inProgress = false;
        timer = false;
    }

    string PrintGateList()
    {
        string result = "{ ";

        foreach(GateTrigger gate in currentList)
        {
            result += gate.name;
            result += ", ";
        }

        result += " }";
        return result;
    }

}
