using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleProgressManager : MonoBehaviour
{

    public static PuzzleProgressManager instance;

    private int amber_puzzles = 3;
    private int amber_count = 0;
    private bool amber_deleted = false;

    private int latte_puzzles = 3;
    private int latte_count = 0;
    private bool latte_deleted = false;

    private int fiber_puzzles = 3;
    private int fiber_count = 0;
    private bool fiber_deleted = false;

    private GameObject lastCompleted = null;

    Cinemachine.CinemachineVirtualCamera mainCamera;

    public enum World { Amber, Latte, Fiber};

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

        mainCamera = GameObject.Find("CM_Main").GetComponent<Cinemachine.CinemachineVirtualCamera>();
    }

    public bool NotifyCount(World world, GameObject completedPuzzle)
    {

        lastCompleted = completedPuzzle;

        if(world == World.Amber)
        {
            if (amber_deleted) return false;

            amber_count++;
            Debug.Log("Notified - amber");

            if (amber_count >= amber_puzzles)
            {
                ArtifactDropper.instance.DropArtifact(ArtifactDropper.World.Amber);
                foreach (ClusterManager puzzle in GameObject.FindObjectsOfType<ClusterManager>())
                {
                    Destroy(puzzle.gameObject);
                    //TODO: Fade out
                    
                }
                mainCamera.enabled = true;
                amber_deleted = true;
            }
            return true;

        }else if(world == World.Fiber)
        {

            if (fiber_deleted) return false;

            fiber_count++;
            Debug.Log("Notified - Fiber");

            if (fiber_count >= fiber_puzzles)
            {
                ArtifactDropper.instance.DropArtifact(ArtifactDropper.World.Fiber);
                foreach (GatePuzzle puzzle in GameObject.FindObjectsOfType<GatePuzzle>())
                {
                    Destroy(puzzle.gameObject);
                   //TODO: Fade out
                }

                mainCamera.enabled = true;
                fiber_deleted = true;
            }
            return true;
        }
        else
        {
            if (latte_deleted) return false;

            latte_count++;
            Debug.Log("Notified - latte");
            if (latte_count >= latte_puzzles)
            {
                ArtifactDropper.instance.DropArtifact(ArtifactDropper.World.Latte);
                foreach (LattePuzzle puzzle in GameObject.FindObjectsOfType<LattePuzzle>())
                {
                   Destroy(puzzle.gameObject);
                   //TODO: Fade out
                    
                }

                mainCamera.enabled = true;
                latte_deleted = true;
            }

            return true;
        }
    }

    public int GetCount(World world)
    {
        if(world == World.Amber)
        {
            return amber_count;
        }else if(world == World.Fiber)
        {
            return fiber_count;
        }
        else
        {
            return latte_count;
        }
    }

    public GameObject GetLastPuzzle()
    {
        return lastCompleted;
    }

    public Transform GetLastPuzzleLocation()
    {
        return lastCompleted.transform;
    }


}
