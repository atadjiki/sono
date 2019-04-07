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



    private void Start()
    {
       // fragment = Instantiate(LevelManager.instance.fragmentPrefab, transform).GetComponent<FragmentController>();
       if(fragmentNumber < LevelManager.instance.audioFragments.Length && fragmentNumber > 0)
        {
         //   fragment.SetClip(LevelManager.instance.audioFragments[fragmentNumber]);
        }
        setPing = true;
        setPickup = false;
        

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
                Debug.Log("PingOff");
                Pickup.SetActive(true);
                Ping.SetActive(false);
            }
            else if (setPing == true)
            {
                Ping.SetActive(true);
                Pickup.SetActive(false);
                Debug.Log("PingOn");
            }
            if (fragment.currentState != FragmentController.states.DEPOSIT)
            {
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
