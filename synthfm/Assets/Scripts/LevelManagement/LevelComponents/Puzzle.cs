using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.Experimental.VFX;

using InControl;
using PlayerInput;

/*
 * Extends the set piece class, with some additional logic
 * Puzzles contain flags that mark if they have been completed, as well
 * as references to the fragment they are protecting.
 * 
 */
[ExecuteInEditMode]
public class Puzzle : SetPiece
{

    public bool complete = false;
    private bool released = false;
    public bool disableCameraOnComplete = true;
    public FragmentCase fragmentCase;
    public FragmentController fragment;
    public GameObject forceField;
    public bool allowDebugComplete;
    private bool artifactLeft = false;

    InputBindings inputBindings;
    string saveData;

    //vfx stuff
    public GameObject forcefieldVFX;
    public GameObject artifactVFX;

    void OnEnable()
    {
        
        inputBindings = InputBindings.CreateWithDefaultBindings();
        LoadBindings();
    } 

    void OnDisable()
    {
        inputBindings.Destroy();
    }

    void SaveBindings()
    {
        saveData = inputBindings.Save();
        PlayerPrefs.SetString("Bindings", saveData);
    }


    void LoadBindings()
    {
        if (PlayerPrefs.HasKey("Bindings"))
        {
            saveData = PlayerPrefs.GetString("Bindings");
            inputBindings.Load(saveData);
        }
    }


    void OnApplicationQuit()
    {
        PlayerPrefs.Save();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        allowDebugComplete = true;
        base.TriggerEnter(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        allowDebugComplete = false;
        base.TriggerExit(collision);
    }

    public void ReleaseCage()
    {

        Debug.Log("Releasing cage");
        //lower the force field and turn off its noise
        forceField.GetComponent<AudioSource>().enabled = false;
        forceField.GetComponent<PointEffector2D>().enabled = false;
        ParticleSystem[] particles = forceField.GetComponentsInChildren<ParticleSystem>();

        foreach (ParticleSystem particle in particles)
        {
            particle.Stop(); //Stop the animations instead of destroying them for the dissipation effect 
        }

        VisualEffect vfx = forcefieldVFX.GetComponent<VisualEffect>();

        if(vfx != null)
        {
           // Debug.Log("Calling stop on vfx " + vfx.name);
            vfx.SetFloat("Setto-100Onkill", 30);
            vfx.SetFloat("Set2OnKill", 1);

            vfx.SetFloat("EmissionRate", 0);
            vfx.SetFloat("EmissionRate2", 0);
           // Debug.Log("travisTest");
            vfx.Stop();
            StartCoroutine("KillVFX", vfx);
            StartCoroutine("LeaveArtifact");

        }

    }

    IEnumerator LeaveArtifact()
    {
        if (artifactLeft == false)
        {
            GameObject artifact = Instantiate<GameObject>(artifactVFX);
            artifact.transform.position = center.transform.position;
            artifactLeft = true;
        }
        
        yield return new WaitForSeconds(1.0f);
        
    }

    IEnumerator KillVFX(VisualEffect vfx)
    {
        yield return new WaitForSeconds(1.5f);
        vfx.SetFloat("Setto-100Onkill", -70);
        yield return new WaitForSeconds(30f);
        vfx.enabled = false;
    }

    IEnumerator DoFadeOut()
    {

        yield return new WaitForSeconds(0.1f);

    }

    public void DoPuzzleSetup()
    {
        base.DoSetup();

        forceField = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/Puzzles/Forcefield"));
        forceField.transform.parent = this.transform;
        forceField.transform.position = Vector3.zero;
        GameObject fragment_case = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/Puzzles/FragmentCase"));
        fragment_case.transform.parent = this.transform;
        fragment_case.transform.position = Vector3.zero;
        fragment_case.GetComponent<CircleCollider2D>().isTrigger = true;
        fragmentCase = fragment_case.GetComponent<FragmentCase>();

        fragment = fragmentCase.getFragment();
        fragment.currentState = FragmentController.states.IDLE;
        Debug.Log("Fragment : " + fragment.name);

    }

    public void SetStatus(bool status)
    {
        complete = status;
    }

    public bool GetStatus()
    {
        return complete;
    }

    void FixedUpdate()
    {
        if (Application.isPlaying && fragment != null)
        {
            if (complete && disableCameraOnComplete && !released)
            {
                ReleaseCage();
                player.gameObject.GetComponent<Navpoint>().CheckForNewTarget();
                released = true;
            }
        }

        if (allowDebugComplete && inputBindings.Debug_Puzzle_Complete.WasPressed)
        {
            Debug.Log("Debug Puzzle Complete");
            complete = true;
        }
    }

    public virtual void GateTriggered(GateTrigger trigger)
    {
    }

}
