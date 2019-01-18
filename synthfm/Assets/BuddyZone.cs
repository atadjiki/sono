using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuddyZone : MonoBehaviour
{

    public BuddyController[] buddies;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if(collision.gameObject == player)
        {
            //release buddies
           
            foreach (BuddyController buddy in buddies)
            {
                buddy.currentState = BuddyController.states.FOLLOW;
                Debug.Log("Buddy Found!");
            }
        }
       
    }
}
