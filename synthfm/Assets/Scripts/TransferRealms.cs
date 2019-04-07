using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransferRealms : MonoBehaviour
{
    [SerializeField] private bool IsPlayerInside;

    [SerializeField] private GameObject player;
    [SerializeField] private GameObject memoryManager;

    [Tooltip("Boolean that is true when we exit a realm and want to know how far away we are from it.")]
    private bool findDistance;

    private SceneMemoryManagement smm;
    private float BRsceneDistance;
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
        if (collision.gameObject.tag == "Player")
        {
            if (gameObject.tag == "Realm1")
            {
                GameObject.Find("Player").GetComponent<Navpoint>().enteredAmberWorld = true;
                UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("AmberWorld", UnityEngine.SceneManagement.LoadSceneMode.Additive);
                gameObject.GetComponent<AmberWorld>().enabled = true;
                ScoreManager._instance.Crossfade();
                GameObject.Find("Player").GetComponent<Navpoint>().maxFragments = 3;

                Debug.Log("Entering 1");

                IsPlayerInside = true;
                // If Follow change to Flee
                FragmentController[] fragments = GameObject.FindObjectsOfType<FragmentController>();
                foreach (FragmentController fragment in fragments)
                {
                    if (fragment.currentState == FragmentController.states.FLEE)
                    {
                        fragment.currentState = FragmentController.states.FOLLOW;

                        Debug.Log("Rejoining with fragments");

                    }

                }
            }
            else if(gameObject.tag == "Realm2")
            {
                GameObject.Find("Player").GetComponent<Navpoint>().enteredFiberWorld = true;
                UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("FiberWorld", UnityEngine.SceneManagement.LoadSceneMode.Additive);
                gameObject.GetComponent<FiberWorld>().enabled = true;
                ScoreManager._instance.Crossfade();
                GameObject.Find("Player").GetComponent<Navpoint>().maxFragments = 3;
            }
            else if(gameObject.tag == "Realm3")
            {
                GameObject.Find("Player").GetComponent<Navpoint>().enteredLatteWorld = true;
                UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("LatteWorld", UnityEngine.SceneManagement.LoadSceneMode.Additive);
                gameObject.GetComponent<LatteWorld>().enabled = true;
                ScoreManager._instance.Crossfade();
                GameObject.Find("Player").GetComponent<Navpoint>().maxFragments = 3;
            }
            else if(gameObject.tag == "Realm4")
            {
                UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("ParasiteVoid", UnityEngine.SceneManagement.LoadSceneMode.Additive);
                Debug.Log("Entering 4");
            }
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        findDistance = true;
        collPosition = (collision.transform.position).magnitude;

        if (gameObject.tag == "Realm1")
        {
            gameObject.GetComponent<AmberWorld>().enabled = false;
            GameObject.Find("Player").GetComponent<Navpoint>().maxFragments = 3;

            Debug.Log("Exiting 1");
            IsPlayerInside = false;
           
            // If Follow change to Flee
            FragmentController[] fragments = GameObject.FindObjectsOfType<FragmentController>();
            foreach (FragmentController fragment in fragments)
            {
                if (fragment.currentState == FragmentController.states.FOLLOW)
                {
                    fragment.currentState = FragmentController.states.FLEE;
                    fragment.setDetatchingTransform(collision.gameObject.transform);
                    Debug.Log("Leavoing Fragments behind");

                }

            }

        }

        if (gameObject.tag == "Realm2")
        {
            gameObject.GetComponent<FiberWorld>().enabled = false;
            GameObject.Find("Player").GetComponent<Navpoint>().maxFragments = 3;
        }

        if (gameObject.tag == "Realm3")
        {
            gameObject.GetComponent<LatteWorld>().enabled = false;
            GameObject.Find("Player").GetComponent<Navpoint>().maxFragments = 3;
        }
    }
}
