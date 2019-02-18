using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerTest : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private GameObject player;
    [SerializeField] private GameObject memoryManager;
    private GameObject BRscene;
    private SceneMemoryManagement smm;

    private float BRsceneDistance;
    private bool findDistance;
    void Start()
    {
        findDistance = false;
        smm = memoryManager.GetComponent<SceneMemoryManagement>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            //UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(1, UnityEngine.SceneManagement.LoadSceneMode.Additive);

        }

        if(findDistance == true)
        {
            BRscene = GameObject.Find("Procedural1");
            smm.BRsceneDistance = Vector3.Distance(player.transform.position, BRscene.transform.position);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("HERE");
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(1,UnityEngine.SceneManagement.LoadSceneMode.Additive);
        findDistance = true;
    }
}
