using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParasiteSpawner : MonoBehaviour
{

    public GameObject parasitePrefab;
    public Transform player;
    public Camera mainCamera;
    public int maxParasites;
    public float secsUntilNextSpawn;
    private List<GameObject> parasites;
    public bool active = true;


    private void Start()
    {
        player = GameObject.Find("Player").transform;

    }
    private void Awake()
    {
        player = GameObject.Find("Player").transform;

        if (player == null)
            player = GameObject.Find("Player").transform;
        if (mainCamera == null)
            mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();

        parasites = new List<GameObject>();

        StartCoroutine("DoSpawn");

    }

    Vector3 SpawnNextParasite()
    {
        GameObject parasite = Instantiate<GameObject>(parasitePrefab);
        parasite.transform.parent = this.transform;


        Vector3 spawnLocation = player.position; //get position of player
        float xMod = 1, yMod = 1;
        if(Random.Range(-1, 1) < 0)
        {
            xMod *= -1;
        }
        if (Random.Range(-1, 1) < 0)
        {
            yMod *= -1;
        }
        spawnLocation += new Vector3(xMod *= Screen.width, yMod * Screen.height, 0);
        parasite.transform.position = spawnLocation;

        parasite.GetComponent<ParasiteController>().followTarget = player;
        parasite.GetComponent<ParasiteController>().currentState = ParasiteController.states.FOLLOW;

        parasites.Add(parasite);

        return spawnLocation;
    }


    IEnumerator DoSpawn()
    {
        while (active)
        {
            if (parasites.Count < maxParasites)
            {
              //  Debug.Log("Spawning Parasite");
                SpawnNextParasite();
                yield return new WaitForSeconds(secsUntilNextSpawn);
            }
        }
    }
    
    public void KillParasites()
    {
        List<GameObject> toDestroy = new List<GameObject>(parasites);
        parasites.Clear();

        foreach (GameObject parasite in toDestroy)
        {
            parasite.GetComponent<ParasiteController>().Kill();
        }

        active = false;
    }
}
