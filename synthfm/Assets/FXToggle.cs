using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FXToggle : MonoBehaviour
{
    public static FXToggle instance;

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

        DontDestroyOnLoad(this.gameObject);
    }

    [Header("Particle World Effects")]
    public GameObject AmberFX;
    public GameObject FiberFX;
    public GameObject LatteFX;

    public void ToggleFX(FragmentController.world world)
    {
        if (world == FragmentController.world.AMBER)
        {
            LatteFX.SetActive(false);
            FiberFX.SetActive(false);
            AmberFX.SetActive(true);
        }
        else if (world == FragmentController.world.FIBER)
        {
            LatteFX.SetActive(false);
            AmberFX.SetActive(false);
            FiberFX.SetActive(true);
        }
        else if (world == FragmentController.world.LATTE)
        {
            FiberFX.SetActive(false);
            AmberFX.SetActive(false);
            LatteFX.SetActive(true);
        }
    }

    public void AllFXOff()
    {
        FiberFX.SetActive(false);
        AmberFX.SetActive(false);
        LatteFX.SetActive(false);
    }
}
