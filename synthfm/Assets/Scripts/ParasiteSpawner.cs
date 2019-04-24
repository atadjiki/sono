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

    public static ParasiteSpawner instance;

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

    private void Start()
    {
        player = GameObject.Find("Player").transform;

        if (player == null)
            player = GameObject.Find("Player").transform;
        if (mainCamera == null)
            mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();

        parasites = new List<GameObject>();

        StartCoroutine("DoSpawn");

    }
   

    public Vector3 SpawnNextParasite()
    {

        if(parasites.Count >= maxParasites)
        {
            //Debug.Log("Already at max parasites");
            return Vector3.zero;
        }

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

        float height = Camera.main.orthographicSize * 2.0f;
        float width = height * Camera.main.aspect;

        spawnLocation += new Vector3(xMod * width, yMod * height, 0);

        //Debug.Log("Spawning parasite " + Vector3.Distance(player.transform.position, spawnLocation) + " away from player");
        parasite.transform.position = spawnLocation;

        parasite.GetComponent<ParasiteController>().followTarget = player;
        parasite.GetComponent<ParasiteController>().currentState = ParasiteController.states.FOLLOW;

        parasites.Add(parasite);

        return spawnLocation;
    }

    public void RunSpawn()
    {
        active = true;
        StartCoroutine("DoSpawn");
    }

    public void StopSpawn()
    {
        active = false;
        StopCoroutine("DoSpawn");
    }


    IEnumerator DoSpawn()
    {
        while (active)
        {
            if (parasites.Count < maxParasites)
            {
                Debug.Log("Spawning Parasite");
                SpawnNextParasite();
                yield return new WaitForSeconds(secsUntilNextSpawn);
            }
        }
    }

    public void KillParasite(ParasiteController parasite, bool respawn)
    {
        parasites.Remove(parasite.gameObject);
        parasite.Kill();
        if(respawn) SpawnNextParasite();
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
