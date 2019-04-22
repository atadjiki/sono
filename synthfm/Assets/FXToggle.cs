using System.Collections;
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
            FiberFX.SetActive(false);
            AmberFX.SetActive(false);
            LatteFX.SetActive(true);

            foreach (ParticleSystem particleSystem in LatteFX.GetComponentsInChildren<ParticleSystem>())
            {
                particleSystem.Play();
            }
        }
    }

    public void TogglePlayerFog(GameObject puzzle, bool enteredPuzzle)
    {
        if (enteredPuzzle)
        {
            LatteFX.SetActive(false);
            //foreach(ParticleSystem p in puzzle.GetComponentsInChildren<ParticleSystem>())
            //{
            //    p.Play();
            //    Debug.Log("Playing particle system " + p.gameObject.name);
            //}
        }
        else
        {
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
