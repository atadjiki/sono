using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalZone : MonoBehaviour
{

    public GameObject portal;
    public LevelManager levelManager;

    private void OnTriggerEnter2D(Collider2D collision)
    {

        levelManager.Completed();

        if (collision.gameObject.CompareTag("Player"))
        {
            Instantiate(portal, this.transform);

            FragmentController[] fragments = GameObject.FindObjectsOfType<FragmentController>();
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
