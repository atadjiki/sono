using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmberWorld : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject Psystem;
    void Start()
    {
        Psystem.SetActive(true);
        foreach(ParticleSystem particleSystem in Psystem.GetComponentsInChildren<ParticleSystem>())
        {
            particleSystem.Play();
        }

        killParasites();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void killParasites()
    {
        if(GameObject.FindGameObjectWithTag("PSpawner") != null)
            GameObject.FindGameObjectWithTag("PSpawner").GetComponent<ParasiteSpawner>().KillParasites();
    }

    private void OnEnable()
    {
        Psystem.SetActive(true);
        foreach (ParticleSystem particleSystem in Psystem.GetComponentsInChildren<ParticleSystem>())
        {
            particleSystem.Play();
        }

        killParasites();
    }

    private void OnDisable()
    {
        //foreach (ParticleSystem particleSystem in Psystem.GetComponentsInChildren<ParticleSystem>())
        //{
        //    particleSystem.Stop();
        //}

    }
}
