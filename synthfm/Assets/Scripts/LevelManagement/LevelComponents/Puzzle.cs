using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

/*
 * Extends the set piece class, with some additional logic
 * Puzzles contain flags that mark if they have been completed, as well
 * as references to the fragment they are protecting.
 * 
 */
[ExecuteInEditMode]
public class Puzzle : SetPiece
{
    public bool complete = false;
    public bool disableCameraOnComplete = true;
    public FragmentCase fragmentCase;
    public FragmentController fragment;
    public  GameObject forceField;

    public void ReleaseCage()
    {

        Debug.Log("Releasing cage");
        //lower the force field and turn off its noise
        forceField.GetComponent<AudioSource>().enabled = false;
        forceField.GetComponent<PointEffector2D>().enabled = false;
        ParticleSystem[] particles = forceField.GetComponentsInChildren<ParticleSystem>();

        foreach (ParticleSystem particle in particles)
        {
            particle.Stop(); //Stop the animations instead of destroying them for the dissipation effect 
        }
    }

    public void DoPuzzleSetup()
    {
        base.DoSetup();

        forceField = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/Puzzles/Forcefield"));
        forceField.transform.parent = this.transform;
        forceField.transform.position = Vector3.zero;
        GameObject fragment_case = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/Puzzles/FragmentCase"));
        fragment_case.transform.parent = this.transform;
        fragment_case.transform.position = Vector3.zero;
        fragment_case.GetComponent<CircleCollider2D>().isTrigger = true;
        fragmentCase = fragment_case.GetComponent<FragmentCase>();

        fragment = fragmentCase.getFragment();
        fragment.currentState = FragmentController.states.IDLE;
        Debug.Log("Fragment : " + fragment.name);

    }

    public void SetStatus(bool status)
    {
        complete = status;
    }

    public bool GetStatus()
    {
        return complete;
    }

    void Update()
    {
        if (Application.isPlaying && fragment != null)
        {
            print("Complete: "+complete);
                if (complete && disableCameraOnComplete)
                {
                    Debug.Log("Puzzle Complete" + gameObject.name);
                    player.gameObject.GetComponent<Navpoint>().CheckForNewTarget();
                    StartCoroutine("DeletePuzzle");

                }
            }
        }

    

    IEnumerator DeletePuzzle()
    {
        yield return new WaitForSecondsRealtime(3);
        Destroy(this.gameObject);
       
    }

    public virtual void GateTriggered(GateTrigger trigger)
    {
    }

}
