using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleSequenceFX : MonoBehaviour
{

    public static TitleSequenceFX instance;

    public bool unlockAmberFX = false;
    private bool dirty = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        if (dirty) { return; }

        if(collision.gameObject == GameObject.Find("Player"))
        {
            FXToggle.instance.ToggleFX(FragmentController.world.AMBER);
            unlockAmberFX = true;
            dirty = true;
        }

        
    }
}
