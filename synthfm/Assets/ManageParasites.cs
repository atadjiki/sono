using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageParasites : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject parasiteSpawner;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Player")
        {
            parasiteSpawner.GetComponent<ParasiteSpawner>().enabled = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.name == "Player")
        {
            parasiteSpawner.GetComponent<ParasiteSpawner>().KillParasites();
        }
    }
}
