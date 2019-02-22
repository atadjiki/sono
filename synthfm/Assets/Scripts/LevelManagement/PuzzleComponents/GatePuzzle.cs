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

    public List<GateTrigger> gates;
    private List<GateTrigger> currentList;
    private int gateLength;
    private int currentIndex;
    private bool inProgress;

    public new void DoSetup()
    {
        base.DoPuzzleSetup();
        gateLength = gates.Count;
        currentList = gates;
        currentIndex = 0;
        inProgress = false;
    }

    public override void GateTriggered(GateTrigger trigger)
    {
        if (!complete)
        {
            //check which gate was triggered
            
            if(gates.Contains(trigger))
            {

                int index = currentList.IndexOf(trigger);
                //if the start index has not been set
                if (!inProgress)
                {
                    Debug.Log("Puzzle started at index " + index);
                    Debug.Log("Gate " + index + " hit");
                    
                    currentIndex = index; //make this the first gate in the list
                    UpdateList(index); //re-order the list around the new starting index
                    currentIndex = 0;
                    trigger.PlayAudioClip(AssetManager.instance.gateTones[currentIndex]);
                    inProgress = true;
                }
                else
                {
                    //if the player is touching the gates in order, increment 
                    Debug.Log("Gate " + index + " hit");
                    currentIndex++;
                    
                    if (index == currentIndex)
                    {
                        trigger.PlayAudioClip(AssetManager.instance.gateTones[currentIndex]);
                        if (currentIndex == gateLength - 1)
                        {
                            complete = true;
                            Debug.Log("Gate puzzle complete!");
                            DeleteGates();
                            SetStatus(complete);
                        }
                    }
                    else
                    {
                        //if this gate was triggered in the wrong order, reset the puzzle
                        Debug.Log("Puzzle reset");
                        trigger.PlayAudioClip(AssetManager.instance.gateTones[6]);
                        currentIndex = 0;
                        UpdateList(currentIndex);
                        inProgress = false;
                    }
                }

            }
        }
    }

    private void Update()
    {
        if(gates.Count == 0)
        {
            complete = true;
            Debug.Log("Gate puzzle complete!");
            DeleteGates();
            SetStatus(complete);
        }
    }

    /*
     * Split the current list of gates based on what index 
     * the player happened to enter. 
     */
    void UpdateList(int index)
    {
        currentList = new List<GateTrigger>(gates.Count);
        currentList.AddRange(gates.GetRange(index, gateLength - index));
        currentList.InsertRange(gateLength - index, gates.GetRange(0, index));
    }

    void DeleteGates()
    {
        foreach(GateTrigger gate in gates)
        {
            Destroy(gate.gameObject); //TODO: Replace this with something more elegant :^)
        }

        base.ReleaseCage();
    }


}
