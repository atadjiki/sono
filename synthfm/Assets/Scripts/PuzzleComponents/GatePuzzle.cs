using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatePuzzle : MonoBehaviour
{

    public List<GateTrigger> gates;
    private List<GateTrigger> currentList;
    private bool isCurrentEntered;
    private int gateLength;
    private int currentIndex;
    public bool complete;
    private bool inProgress;

    public Puzzle parent;

    // Start is called before the first frame update
    void Awake()
    {
        isCurrentEntered = false;
        gateLength = gates.Count;
        currentList = gates;
        currentIndex = 0;
        complete = false;
        inProgress = false;
        Debug.Log("Found " + gateLength + " gates");
       

    }

    public void GateTriggered(GateTrigger trigger)
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
                    inProgress = true;
                }
                else
                {
                    //if the player is touching the gates in order, increment 
                    currentIndex++;
                    if (index == currentIndex)
                    {
                        Debug.Log("Gate " + index + " hit");
                        
                        if (currentIndex == gateLength -1)
                        {
                            complete = true;
                            Debug.Log("Gate puzzle complete!");
                            DeleteGates();
                            parent.SetStatus(complete);
                        }
                    }
                    else
                    {
                        //if this gate was triggered in the wrong order, reset the puzzle
                        Debug.Log("Puzzle reset");
                        currentIndex = 0;
                        inProgress = false;
                    }
                }

            }
        }
    }

    void UpdateList(int index)
    {
        currentList = new List<GateTrigger>(gates.Count);
        currentList.AddRange(gates.GetRange(index, gateLength - index));
        currentList.AddRange(gates.GetRange(0, index));
    }

    void DeleteGates()
    {
        foreach(GateTrigger gate in gates)
        {
            GameObject.Destroy(gate.gameObject);
        }
    }


}
