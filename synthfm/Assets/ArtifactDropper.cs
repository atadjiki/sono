﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtifactDropper : MonoBehaviour
{
    public static ArtifactDropper instance;


    public GameObject amber_artifact;
    public GameObject fiber_artifact;
    public GameObject latte_artifact;

    private bool amber_dropped = false;
    private bool fiber_dropped = false;
    private bool latte_dropped = false;

    public enum World { Latte, Amber, Fiber};

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

    }

    public void DropArtifact(World world)
    {
        if (world == World.Amber && !amber_dropped)
        {
            if (amber_artifact != null)
            {
                GameObject artifact = Instantiate<GameObject>(amber_artifact);
                artifact.transform.position = PuzzleProgressManager.instance.GetLastPuzzleLocation().position;
                Vector3 newpos = new Vector3(0, 0, 30);
                artifact.transform.position += newpos;
                amber_dropped = true;
            }

        }
        else if (world == World.Fiber && !fiber_dropped)
        {
            if (amber_artifact != null)
            {
                GameObject artifact = Instantiate<GameObject> (fiber_artifact);
                artifact.transform.position = PuzzleProgressManager.instance.GetLastPuzzleLocation().position;
                Vector3 newpos = new Vector3(0, 0, 30);
                artifact.transform.position += newpos;
                fiber_dropped = true;
            }

        }
        else if (world == World.Latte && !latte_dropped)
        {
            if (amber_artifact != null)
            {
                GameObject artifact = Instantiate<GameObject>(latte_artifact);
                artifact.transform.position = PuzzleProgressManager.instance.GetLastPuzzleLocation().position;
                Vector3 newpos = new Vector3(0, 0, 30);
                artifact.transform.position += newpos;
                latte_dropped = true;
            }

        }
    }

}
