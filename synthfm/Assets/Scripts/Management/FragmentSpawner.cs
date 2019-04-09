using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FragmentSpawner : MonoBehaviour
{

    public GameObject fragmentPrefab;
    public GameObject fragmentCasePrefab;
    public Transform player;
    public Camera mainCamera;
    public bool active = true;
    public FragmentController.states SpawnState;


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

    }

    public Vector3 SpawnFragment()
    {

        if(SpawnState == FragmentController.states.IDLE)
        {

            return SpawnFragmentCase();
            
        }

        if (FragmentManager.instance.AttachedFragments().Count >= 3)
        {
            return Vector3.zero;
        }

        GameObject fragment = Instantiate<GameObject>(fragmentPrefab);
        fragment.transform.parent = this.transform;


        Vector3 spawnLocation = player.position; //get position of player
        float xMod = 1, yMod = 1;
        if (Random.Range(-1, 1) < 0)
        {
            xMod *= -1;
        }
        if (Random.Range(-1, 1) < 0)
        {
            yMod *= -1;
        }
        spawnLocation += new Vector3(xMod *= Screen.width, yMod * Screen.height, 0);
        fragment.transform.position = spawnLocation;

        fragment.GetComponent<FragmentController>().followTarget = player;
        fragment.GetComponent<FragmentController>().currentState = SpawnState;

        FragmentManager.instance.AddFragment(fragment.GetComponent<FragmentController>());

        return spawnLocation;
    }

    public Vector3 SpawnFragmentCase()
    {

        if (FragmentManager.instance.AttachedFragments().Count >= 3)
        {
            return Vector3.zero;
        }

        GameObject fragmentCase = Instantiate<GameObject>(fragmentCasePrefab);
        fragmentCase.transform.parent = this.transform;


        Vector3 spawnLocation = player.position; //get position of player
        float xMod = 1, yMod = 1;
        if (Random.Range(-1, 1) < 0)
        {
            xMod *= -1;
        }
        if (Random.Range(-1, 1) < 0)
        {
            yMod *= -1;
        }
        spawnLocation += new Vector3(xMod *= Screen.width, yMod * Screen.height, 0);
        fragmentCase.transform.position = spawnLocation;

        fragmentCase.GetComponentInChildren<FragmentController>().followTarget = player;
        fragmentCase.GetComponentInChildren<FragmentController>().currentState = SpawnState;

        FragmentManager.instance.AddFragment(fragmentCase.GetComponentInChildren<FragmentController>());

        return spawnLocation;
    }

}
