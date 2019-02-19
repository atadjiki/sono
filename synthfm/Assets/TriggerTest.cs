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
    private float collPosition;
    void Start()
    {
        findDistance = false;
        smm = memoryManager.GetComponent<SceneMemoryManagement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (findDistance == true)
         {
             float distX = collPosition - (player.transform.position).magnitude;
             smm.BRsceneDistance = distX;
         }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && gameObject.tag == "Realm1")
        {
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(1, UnityEngine.SceneManagement.LoadSceneMode.Additive);

        }
        else if (collision.gameObject.tag == "Player" && gameObject.tag == "Realm2")
        {
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(2, UnityEngine.SceneManagement.LoadSceneMode.Additive);

        }
        else if (collision.gameObject.tag == "Player" && gameObject.tag == "Realm3")
        {
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(3, UnityEngine.SceneManagement.LoadSceneMode.Additive);

        }
        else if (collision.gameObject.tag == "Player" && gameObject.tag == "Realm4")
        {
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(4, UnityEngine.SceneManagement.LoadSceneMode.Additive);

        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        findDistance = true;
        collPosition = (collision.transform.position).magnitude;
    }
}
