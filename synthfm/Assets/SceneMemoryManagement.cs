using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMemoryManagement : MonoBehaviour
{
    [SerializeField] private GameObject player;

    [Tooltip("The distance between the player and the scene where we want the scene to unload.")]
    [SerializeField] private float unloadDistance;

    public float BRsceneDistance;

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("ParasiteVoid"))
        {
            if (BRsceneDistance > unloadDistance)
            {
                SceneManager.UnloadSceneAsync("ParasiteVoid");
            }
        }
        else if (GameObject.Find("Procedural1"))
        {
            if (BRsceneDistance > unloadDistance)
            {
                SceneManager.UnloadSceneAsync("Procedural1");
            }
        }
    }
}
