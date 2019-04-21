using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransferRealms : MonoBehaviour
{
    [Header("The maximum amount of fragments to exit this world")]
    public int maxToExit;

    //  [SerializeField] private bool IsPlayerInside;

    [SerializeField] private GameObject player;
    [SerializeField] private GameObject memoryManager;


    [Tooltip("Boolean that is true when we exit a realm and want to know how far away we are from it.")]
    private bool findDistance;

    private SceneMemoryManagement smm;
    private float BRsceneDistance;
    private float collPosition;

    public string CurrentWorld;

    private bool isAmberLoaded;
    private bool isFiberLoaded;
    private bool isLatteLoaded;
    private bool isVoidLoaded;


    void Start()
    {
        isAmberLoaded = false;
        isFiberLoaded = false;
        isLatteLoaded = false;
        isVoidLoaded = false;

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

    private void LoadScene(string sceneName)
    {
        if(sceneName == "AmberWorld")
        {
            isAmberLoaded = true;
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("AmberWorld", UnityEngine.SceneManagement.LoadSceneMode.Additive);

        }
        else if(sceneName == "LatteWorld")
        {
            isLatteLoaded = true;
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("LatteWorld", UnityEngine.SceneManagement.LoadSceneMode.Additive);

        }
        else if(sceneName == "FiberWorld")
        {
            isFiberLoaded = true;
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("FiberWorld", UnityEngine.SceneManagement.LoadSceneMode.Additive);

        }
        else if(sceneName == "ParasiteVoid")
        {
            isVoidLoaded = true;
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("ParasiteVoid", UnityEngine.SceneManagement.LoadSceneMode.Additive);

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //gameObject.GetComponent<CapsuleCollider2D>().enabled = false;

            if (gameObject.tag == "Realm1")
            {
                if (isAmberLoaded == false)
                {
                    LoadScene("AmberWorld");

                }

                GameObject.Find("Player").GetComponent<Navpoint>().enteredAmberWorld = true;
                gameObject.GetComponent<AmberWorld>().enabled = true;
                changeAppearance("Amber");
                ScoreManager._instance.Crossfade();
                GameObject.Find("Player").GetComponent<Navpoint>().maxFragments = 3;


                // If Flee -> change to FOLLOW
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
            else if (gameObject.tag == "Realm2")
            {
                if (isFiberLoaded == false)
                {
                    LoadScene("FiberWorld");
                    isFiberLoaded = true;

                }
                GameObject.Find("Player").GetComponent<Navpoint>().enteredFiberWorld = true;
                changeAppearance("Fiber");
                gameObject.GetComponent<FiberWorld>().enabled = true;
                ScoreManager._instance.Crossfade();
                GameObject.Find("Player").GetComponent<Navpoint>().maxFragments = 3;

                handleFragment_Tarnsport(gameObject.tag.ToString());
            }
            else if (gameObject.tag == "Realm3")
            {
                if (isLatteLoaded == false)
                {
                    LoadScene("LatteWorld");
                    isLatteLoaded = true;
                }
                GameObject.Find("Player").GetComponent<Navpoint>().enteredLatteWorld = true;
                gameObject.GetComponent<LatteWorld>().enabled = true;
                ScoreManager._instance.Crossfade();
                GameObject.Find("Player").GetComponent<Navpoint>().maxFragments = 3;
            }
            else if (gameObject.tag == "Realm4")
            {
                if (!GameObject.Find("Parasite"))
                {
                    if (isVoidLoaded == false)
                    {
                        LoadScene("ParasiteVoid");
                        isVoidLoaded = true;
                    }
                    changeAppearance("Void");
                    Debug.Log("Entering 4");
                }
            }
        }

    }

    private void changeAppearance(string destination)
    {
        ChangeColor cc = GameObject.Find("Main Camera").GetComponent<ChangeColor>();
        if (destination == "Void")
        {
            StartCoroutine(cc.changeColor(cc.voidbgColor, cc.voidColor[2], cc.voidColor[3]));
        }
        else if (destination == "Amber")
        {
            StartCoroutine(cc.changeColor(cc.dark, cc.firstamberPuzzleColor[2], cc.firstamberPuzzleColor[3]));
        }
        else if (destination == "Fiber")
        {
            StartCoroutine(cc.changeColor(cc.dark, cc.currentPlayercolor, cc.currentTrailColor));
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        findDistance = true;
        collPosition = (collision.transform.position).magnitude;

        if (gameObject.tag == "Realm1")
        {
            gameObject.GetComponent<AmberWorld>().enabled = false;
            // GameObject.Find("Player").GetComponent<Navpoint>().maxFragments = 3;
          
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

    private void handleFragment_Tarnsport(string i_realm)
    {
        // If Follow -> change to Flee (is less than three)
        int n = 0;
        List<FragmentController> ToHandle = new List<FragmentController>();
        FragmentController[] fragments = GameObject.FindObjectsOfType<FragmentController>();
        foreach (FragmentController fragment in fragments)
        {
            switch (i_realm)
            {
                case "Realm2":
                    if (fragment.TrackIndex >= 1 || fragment.TrackIndex <= 3)
                    {
                        if (fragment.currentState == FragmentController.states.FOLLOW)
                        {
                            fragment.followTarget = fragment.gameObject.GetComponent<PatternGenerator>().getStartingPoint();
                            fragment.changeTrailTime(4);
                        }
                    }
                    break;

                case "Realm3":
                    if (fragment.TrackIndex >= 4 || fragment.TrackIndex <= 6)
                    {
                        if (fragment.currentState == FragmentController.states.FOLLOW)
                        {
                            fragment.followTarget = fragment.gameObject.GetComponent<PatternGenerator>().getStartingPoint();
                            fragment.changeTrailTime(4);
                        }
                    }
                    break;
                case "Realm4":
                    // spawn the final pattern in from of player
                    break;

                if (fragment.currentState == FragmentController.states.FOLLOW)
                {
                    n++;
                    ToHandle.Add(fragment);
                    //    Debug.Log("Leavoing Fragments behind");
                }
            }
        }

        if (n < maxToExit)
        {
            foreach (FragmentController fc in ToHandle)
            {
                fc.currentState = FragmentController.states.FLEE;
            }
        }
        else
        {
            Debug.Log("Successfully exiting the world !");
        }
    }
}
