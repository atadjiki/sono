﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FXToggle : MonoBehaviour
{
    public static FXToggle instance;

    [Header("Particle World Effects")]
    public GameObject AmberFX;
    public GameObject FiberFX;
    public GameObject LatteFX;

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

    public void ToggleFX(FragmentController.world world)
    {
        if (world == FragmentController.world.AMBER)
        {
            LatteFX.SetActive(false);
            FiberFX.SetActive(false);
            AmberFX.SetActive(true);

            foreach (ParticleSystem particleSystem in AmberFX.GetComponentsInChildren<ParticleSystem>())
            {
                particleSystem.Play();
            }
        }
        else if (world == FragmentController.world.FIBER)
        {
            LatteFX.SetActive(false);
            AmberFX.SetActive(false);
            FiberFX.SetActive(true);

            foreach (ParticleSystem particleSystem in FiberFX.GetComponentsInChildren<ParticleSystem>())
            {
                particleSystem.Play();
            }
        }
        else if (world == FragmentController.world.LATTE)
        {
           // Debug.Log("Toggle Latte World");

            FiberFX.SetActive(false);
            AmberFX.SetActive(false);
            LatteFX.SetActive(true);

            foreach (ParticleSystem particleSystem in LatteFX.GetComponentsInChildren<ParticleSystem>())
            {
                particleSystem.Play();
            }
        }

        Debug.Log("Toggling FX - " + world.ToString());
    }

    public void TogglePlayerFog(GameObject puzzle, bool enteredPuzzle)
    {
       // Debug.Log("Toggle Player Fog");
        if (enteredPuzzle)
        {
            foreach (ParticleSystem particleSystem in LatteFX.GetComponentsInChildren<ParticleSystem>())
            {
                particleSystem.Stop();
            }
        }
        else
        {
            foreach (ParticleSystem particleSystem in LatteFX.GetComponentsInChildren<ParticleSystem>())
            {
                particleSystem.Play();
            }
        }
    }

    public void AllFXOff()
    {
        //FiberFX.SetActive(false);
        //AmberFX.SetActive(false);
        //LatteFX.SetActive(false);
        foreach (ParticleSystem particleSystem in AmberFX.GetComponentsInChildren<ParticleSystem>())
        {
            particleSystem.Stop();
        }
        foreach (ParticleSystem particleSystem in FiberFX.GetComponentsInChildren<ParticleSystem>())
        {
            particleSystem.Stop();
        }
        foreach (ParticleSystem particleSystem in LatteFX.GetComponentsInChildren<ParticleSystem>())
        {
            particleSystem.Stop();
        }

    }
}
