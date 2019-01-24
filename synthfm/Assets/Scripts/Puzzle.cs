using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class Puzzle : SetPiece
{

    public bool complete = false;
    public bool disableCameraOnComplete = true;
    public FragmentCase fragmentCase;
    private FragmentController fragment;

    private void Awake()
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

    private void Update()
    {
        if(fragment.GetState() == FragmentController.states.FOLLOW)
        {
            if (complete && disableCameraOnComplete)
            {
                Debug.Log("Switching camera to " + getMainCamera().name);
                getMainCamera().enabled = true;
                setPieceCamera.enabled = false;
            }
        }
    }

}
