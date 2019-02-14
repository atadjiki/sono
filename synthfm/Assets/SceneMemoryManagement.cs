using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMemoryManagement : MonoBehaviour
{
    [SerializeField] private GameObject player;

    public float BRsceneDistance;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(GameObject.Find("Procedural1"))
        {
            //print("Distance between Player and Scene Procedural 1" + Vector3.Distance(player.transform.position, (GameObject.Find("Procedural1").transform.position)));
            print(BRsceneDistance);
            //if()
        }
    }
}
