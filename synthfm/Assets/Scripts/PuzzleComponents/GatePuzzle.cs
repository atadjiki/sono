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

    public GameObject forceField;

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
                    trigger.PlayAudioClip(AssetManager.instance.gateTones[index]);
                    currentIndex = index; //make this the first gate in the list
                    UpdateList(index); //re-order the list around the new starting index
                    inProgress = true;
                }
                else
                {
                    //if the player is touching the gates in order, increment 
                    Debug.Log("Gate " + index + " hit");
                    trigger.PlayAudioClip(AssetManager.instance.gateTones[index]);
                    currentIndex++;
                    if (index == currentIndex)
                    {
                       
                        if (currentIndex == gateLength - 1)
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
                        UpdateList(currentIndex);
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

        forceField.GetComponent<PointEffector2D>().enabled = false;
        forceField.GetComponent<AudioSource>().enabled = false;
        ParticleSystem[] particles = forceField.GetComponentsInChildren<ParticleSystem>();
        
        foreach(ParticleSystem particle in particles)
        {
            particle.Stop();
        }
    }


}
