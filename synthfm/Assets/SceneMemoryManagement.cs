using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMemoryManagement : MonoBehaviour
{
    [SerializeField] private GameObject player;

    public float BRsceneDistance;
    public float initialBRsceneDistance;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        print(BRsceneDistance);
        if(GameObject.Find("Procedural1"))
        {
            if(BRsceneDistance > 350f)
            {
                SceneManager.UnloadSceneAsync(1);
            }
        }
        else if(GameObject.Find("Procedural2"))
        {
            if(BRsceneDistance > 350f)
            {
                SceneManager.UnloadSceneAsync(2);
            }

        }
        else if (GameObject.Find("Procedural3"))
        {
            if (BRsceneDistance > 350f)
            {
                SceneManager.UnloadSceneAsync(3);
            }
        }
        else if (GameObject.Find("Procedural4"))
        {
            if (BRsceneDistance > 350f)
            {
                SceneManager.UnloadSceneAsync(4);
            }
        }
    }
}
