using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.VFX;
public class TravisCollisionTest : MonoBehaviour
{
    [SerializeField] private VisualEffect vfx;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Player")
        {
            //TO DO: For Travis - Instantiate your VFX here!
            vfx.Play();
        }
    }
}
