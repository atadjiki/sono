using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiberWorld : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject Psystem;
    void Start()
    {
        Psystem.SetActive(true);

        killParasites();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void killParasites()
    {
        GameObject.FindGameObjectWithTag("PSpawner").GetComponent<ParasiteSpawner>().KillParasites();
    }
}
