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
    public static FragmentCase fragmentCase;
    private static FragmentController fragment;
    private static bool puzzleInitialized = false;

    private void OnEnable()
    {

        if (!Application.isEditor || Application.isPlaying || puzzleInitialized) { Debug.Log("Puzzle Initialized " + puzzleInitialized); return; }

        base.Initialize();

        GameObject forcefield = Resources.Load<GameObject>("Prefabs/Puzzles/Forcefield");
        GameObject fragment_case = Resources.Load<GameObject>("Prefabs/Puzzles/FragmentCase");
        fragmentCase = fragment_case.GetComponent<FragmentCase>();

        fragment = fragmentCase.getFragment();
        Debug.Log("Fragment : " + fragment.name);

        puzzleInitialized = true;
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
        if (Application.isPlaying)
        {
            if (fragment.GetState() == FragmentController.states.FOLLOW)
            {
                if (complete && disableCameraOnComplete)
                {
                    Debug.Log("Switching camera to " + getMainCamera().name);
                    getMainCamera().enabled = true;
                    setPieceCamera.enabled = false;
                    Debug.Log("Puzzle Complete" + gameObject.name);
                    StartCoroutine("DeletePuzzle");

                }
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
