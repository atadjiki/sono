using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalZone : MonoBehaviour
{

    public GameObject portal;
    public Navpoint navpoint;

    private void OnTriggerEnter2D(Collider2D collision)
    {


        navpoint.enabled = false;

        if (collision.gameObject.CompareTag("Player"))
        {
            Instantiate(portal, this.transform);

            FragmentController[] fragments = GameObject.FindObjectsOfType<FragmentController>();
            foreach(FragmentController fragment in fragments)
            {
                fragment.Collect(this.transform);
            }

        }
    }
}
