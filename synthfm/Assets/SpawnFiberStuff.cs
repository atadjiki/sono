using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFiberStuff : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            GameObject.Find("Player").GetComponent<Navpoint>().enteredFiberWorld = true;
            gameObject.GetComponent<FiberWorld>().enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameObject.Find("Player").GetComponent<Navpoint>().enteredFiberWorld = false;
            gameObject.GetComponent<FiberWorld>().enabled = false;

        }
    }
}
