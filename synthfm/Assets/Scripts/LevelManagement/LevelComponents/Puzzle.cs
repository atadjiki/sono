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
public class Puzzle : SetPiece
{

    public bool complete = false;
    public bool disableCameraOnComplete = true;
    public FragmentCase fragmentCase;
    private FragmentController fragment;

    public void Initialize()
    {
        fragment = fragmentCase.getFragment();
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
        if(fragment.GetState() == FragmentController.states.FOLLOW)
        {
            if (complete && disableCameraOnComplete)
            {
                Debug.Log("Switching camera to " + getMainCamera().name);
                getMainCamera().enabled = true;
                setPieceCamera.enabled = false;
                StartCoroutine("DeletePuzzle");
               
            }
        }
    }

    IEnumerator DeletePuzzle()
    {
        yield return new WaitForSecondsRealtime(3);
        Destroy(this.gameObject);
       
    }

}
