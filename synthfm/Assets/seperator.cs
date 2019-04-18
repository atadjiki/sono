using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class seperator : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // change fragment state and start pattern
        FragmentController[] fragments = GameObject.FindObjectsOfType<FragmentController>();
        foreach (FragmentController fragment in fragments)
        {
            if (fragment.currentState == FragmentController.states.FOLLOW)
            {
                if (fragment.TrackIndex != 1) // temporary
                {
                    fragment.currentState = FragmentController.states.FLEE;
                }
                Debug.Log("generating patterns");

            }

        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
