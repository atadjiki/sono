using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.VFX;

public class FragmentCase : MonoBehaviour
{
    public int fragmentNumber;
    public FragmentController fragment;

    public bool setPing;
    public bool setPickup;
    public GameObject Ping;
    public GameObject Pickup;
    private FragmentController.world world;




    private void Start()
    {
       // fragment = Instantiate(LevelManager.instance.fragmentPrefab, transform).GetComponent<FragmentController>();
       if(fragmentNumber < LevelManager.instance.audioFragments.Length && fragmentNumber > 0)
        {
         //   fragment.SetClip(LevelManager.instance.audioFragments[fragmentNumber]);
        }
        setPing = true;
        setPickup = false;

        world = GetComponentInChildren<FragmentController>().currentWorld;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       // Debug.Log("Fragment case collided with " + collision.gameObject.name);

        /*string p = PlayerPrefs.GetString("SavedData");
        SavedData s = JsonUtility.FromJson<SavedData>(p);
        foreach(Puzzle f in s.hubLevels)
        {
            print(f.complete);
        }*/



        //TO DO: Make this not grabbable after it is deposited
        if (collision.gameObject == LevelManager.instance.getPlayer())
        {
            setPing = false;
            setPickup = true;
            if (setPickup == true)
            {
               // Debug.Log("PingOff");
                Pickup.SetActive(true);
                Ping.SetActive(false);
            }
            else if (setPing == true)
            {
                Ping.SetActive(true);
                Pickup.SetActive(false);
                //Debug.Log("PingOn");
            }
            if (fragment.currentState != FragmentController.states.DEPOSIT)
            {

                if (GetComponentInParent<LattePuzzle>() != null || world == FragmentController.world.LATTE)
                {
                    if(this.gameObject != PuzzleProgressManager.instance.GetLastPuzzle())
                    {
                        PuzzleProgressManager.instance.NotifyCount(PuzzleProgressManager.World.Latte, this.gameObject);
                        if (GetComponentInChildren<FragmentController>().GetComponentInChildren<CapsuleCollider2D>() != null)
                        {
                            GetComponentInChildren<FragmentController>().GetComponentInChildren<CapsuleCollider2D>().enabled = true;
                        }
                        
                    }
                    else
                    {
                        Debug.Log("Already picked up this fragment!");
                    }
                    
                }
                else if (GetComponentInParent<GatePuzzle>() != null || world == FragmentController.world.FIBER)
                {
                    PuzzleProgressManager.instance.NotifyCount(PuzzleProgressManager.World.Fiber, this.gameObject);
                }
                else if (GetComponentInParent<ClusterManager>() != null || world == FragmentController.world.AMBER)
                {
                    PuzzleProgressManager.instance.NotifyCount(PuzzleProgressManager.World.Amber, this.gameObject);
                }

                fragment.Collect(LevelManager.instance.getPlayer().transform);
                if (GetComponentInParent<Puzzle>() == null)
                {
                    Debug.Log("parent doesn't have puzzle component!");
                }
                else
                {
                    GetComponentInParent<Puzzle>().mainCamera.enabled = true;
                    GetComponentInParent<Puzzle>().setPieceCamera.enabled = false;
                }
            }

        }
       
    }

    public FragmentController getFragment()
    {
        return fragment;
    }
}
