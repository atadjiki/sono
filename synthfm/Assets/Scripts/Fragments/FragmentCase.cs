using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FragmentCase : MonoBehaviour
{
    public int fragmentNumber;
    public FragmentController fragment;

    private void Start()
    {
       // fragment = Instantiate(LevelManager.instance.fragmentPrefab, transform).GetComponent<FragmentController>();
       if(fragmentNumber < LevelManager.instance.audioFragments.Length && fragmentNumber > 0)
        {
            fragment.SetClip(LevelManager.instance.audioFragments[fragmentNumber]);
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Fragment case collided with " + collision.gameObject.name);
        //TO DO: Make this not grabbable after it is deposited
        if(collision.gameObject == LevelManager.instance.getPlayer())
        {
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
