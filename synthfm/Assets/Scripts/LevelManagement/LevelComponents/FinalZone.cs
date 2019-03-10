using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalZone : MonoBehaviour
{

    public GameObject portal;
    public LevelManager levelManager;

    private void OnTriggerEnter2D(Collider2D collision)
    { 

        if (collision.gameObject == GameObject.Find("Player"))
        {
            Instantiate(portal, this.transform);

            List<FragmentController> fragments = FragmentManager.instance.AttachedFragments();
            foreach(FragmentController fragment in fragments)
            {

                if (fragment.currentState != FragmentController.states.DEPOSIT)
                {
                    fragment.Collect(this.transform);
                }
            }

        }
    }
}
