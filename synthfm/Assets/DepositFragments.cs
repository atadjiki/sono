using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DepositFragments : MonoBehaviour
{
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
        print("Tag of object collided with hub: " + collision.gameObject.tag);
        print("Name of object collided with hub: " + collision.gameObject.name);
        if(collision.gameObject.tag == "Player")
        {

            FragmentController[] fragments = GameObject.FindObjectsOfType<FragmentController>();
            foreach (FragmentController fragment in fragments)
            {
                //fragment.Collect(this.transform);
               if(fragment.isAttached == true)
                {
                    fragment.Deposit(gameObject.transform);

                }
            }
        }

    }
}
