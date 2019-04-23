using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransferRealms : MonoBehaviour
{
    // [Header("The maximum amount of fragments to exit this world")]
    private int maxToExit = 3;

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

            FragmentManager.instance.currentFrames = FragmentManager.instance.maxFrames;

        }
        else if(sceneName == "LatteWorld")
        {
            isLatteLoaded = true;
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("LatteWorld", UnityEngine.SceneManagement.LoadSceneMode.Additive);

            FragmentManager.instance.currentFrames = FragmentManager.instance.maxFrames;


        }
        else if(sceneName == "FiberWorld")
        {
            isFiberLoaded = true;
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("FiberWorld", UnityEngine.SceneManagement.LoadSceneMode.Additive);

            FragmentManager.instance.currentFrames = FragmentManager.instance.maxFrames;


        }
        else if(sceneName == "ParasiteVoid")
        {
            isVoidLoaded = true;
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("ParasiteVoid", UnityEngine.SceneManagement.LoadSceneMode.Additive);

            FragmentManager.instance.currentFrames = FragmentManager.instance.maxFrames;


        }
    }

    
    private void OnTriggerEnter2D(Collider2D collision)  // ** ENTER **
    {
        if (collision.gameObject.tag == "Player")
        {
            //gameObject.GetComponent<CapsuleCollider2D>().enabled = false;

            if (gameObject.tag == "Realm1") // AMBER
            {
                if (isAmberLoaded == false)
                {
                    LoadScene("AmberWorld");

                }

                GameObject.Find("Player").GetComponent<Navpoint>().enteredAmberWorld = true;
                gameObject.GetComponent<AmberWorld>().enabled = true;
                changeAppearance("Amber");
                ScoreManager._instance.LoadPattern(3);
                ScoreManager._instance.Crossfade();
                print("Entering Amber");
                GameObject.Find("Player").GetComponent<Navpoint>().maxFragments = 3;

                FXToggle.instance.ToggleFX(FragmentController.world.AMBER);


                HandleEnterActions(FragmentController.world.AMBER); // Fragment enter actions
                
            }
            else if (gameObject.tag == "Realm2") // FIBER
            {
                if (isFiberLoaded == false)
                {
                    LoadScene("FiberWorld");
                    isFiberLoaded = true;

                }

                GameObject.Find("Player").GetComponent<Navpoint>().enteredFiberWorld = true;
                changeAppearance("Fiber");
                gameObject.GetComponent<FiberWorld>().enabled = true;
                ScoreManager._instance.LoadPattern(1);
                ScoreManager._instance.Crossfade();
                GameObject.Find("Player").GetComponent<Navpoint>().maxFragments = 3;

                HandleEnterActions(FragmentController.world.FIBER); // Fragment enter actions
                changeStateToVoid(FragmentController.world.AMBER); // make fragment stop following you in FIber world

                FXToggle.instance.ToggleFX(FragmentController.world.FIBER);
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
                ScoreManager._instance.LoadPattern(2);
                ScoreManager._instance.Crossfade();
                GameObject.Find("Player").GetComponent<Navpoint>().maxFragments = 3;
                FXToggle.instance.ToggleFX(FragmentController.world.LATTE);

                HandleEnterActions(FragmentController.world.LATTE); // Fragment enter actions
               // changeStateToVoid(FragmentController.world.LATTE); // make fragment stop following you in FIber world

                prepareForCurve();
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
                    int voidlevel = FragmentManager.instance.fragments.Count / 3;
                    ScoreManager._instance.LoadVoidAtLevel(voidlevel);
                    ScoreManager._instance.Crossfade();
                    changeAppearance("Void");

                    ParasiteSpawner.instance.RunSpawn();

                    FXToggle.instance.AllFXOff();

                    Debug.Log("Entering 4");
                }
            }
        }

    }

   
    private void OnTriggerExit2D(Collider2D collision)  // ** EXIT **
    {
        findDistance = true;
        collPosition = (collision.transform.position).magnitude;

        if (gameObject.tag == "Realm1")
        {
            gameObject.GetComponent<AmberWorld>().enabled = false;

            HandleExitActions(FragmentController.world.AMBER);
        }

        if (gameObject.tag == "Realm2")
        {
            gameObject.GetComponent<FiberWorld>().enabled = false;
            GameObject.Find("Player").GetComponent<Navpoint>().maxFragments = 3;

            if (collision.gameObject.tag == "Player")
            {
                HandleExitActions(FragmentController.world.FIBER);
                // join with the Amber Fragments
                JoinInVoid();
            }
        }

        if (gameObject.tag == "Realm3")
        {
            gameObject.GetComponent<LatteWorld>().enabled = false;
            GameObject.Find("Player").GetComponent<Navpoint>().maxFragments = 3;

            if (collision.gameObject.tag == "Player")
            {
                HandleExitActions(FragmentController.world.LATTE);

                SpawnFinalPatternZone();
            }
        }

        if(gameObject.tag != "Realm4")
        {

            ParasiteSpawner.instance.KillParasites();
            ParasiteSpawner.instance.StopSpawn();
        }
    }

    private void SpawnFinalPatternZone()
    {
        // spawn the finalPattern Zone
        GameObject finalPattern = GameObject.Find("Final Pattern Zone");
        Vector3 playerPos = GameObject.Find("Player").gameObject.transform.position;
        finalPattern.transform.position = playerPos - new Vector3(100, 0, 0);

        // make the Latte frags follow starting point
        FragmentController[] fragments = GameObject.FindObjectsOfType<FragmentController>();
        foreach (FragmentController fragment in fragments)
        {
            if(fragment.currentWorld == FragmentController.world.LATTE && fragment.currentState == FragmentController.states.FOLLOW)
            {
                fragment.makeFollowCurveStartingPoint();
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

    // move fragments to the curve pos
    private void prepareForCurve()
    {
        FragmentController[] fragments = GameObject.FindObjectsOfType<FragmentController>();
        foreach (FragmentController fragment in fragments)
        {
            if (fragment.currentState == FragmentController.states.FOLLOW)
            {
                fragment.changeTrailTime(0);
                fragment.getReadyForCurve();
            }
        } // all 6 frags have PRE_FINAL state after this
    }

    // Change state to void
    private void changeStateToVoid(FragmentController.world i_World)
    {
        FragmentController[] fragments = GameObject.FindObjectsOfType<FragmentController>();
        foreach (FragmentController fragment in fragments)
        {
            if(fragment.currentState == FragmentController.states.FOLLOW && fragment.currentWorld == i_World)
            {
                fragment.currentState = FragmentController.states.VOID;
            }
        }
    }

    // to move fragment to void around player
    private void JoinInVoid()   // Amber Frags joins you in void between Fiber and Latte
    {
        Vector3 playerPos = GameObject.Find("Player").gameObject.transform.position;
        // change trail to zero
        // move around player
        // change trail time to 10
        FragmentController[] fragments = GameObject.FindObjectsOfType<FragmentController>();
        foreach (FragmentController fragment in fragments)
        {
            if(fragment.currentState == FragmentController.states.VOID && fragment.currentWorld == FragmentController.world.AMBER)
            {
                fragment.changeTrailTime(0);
                fragment.transform.position = playerPos + new Vector3(150,0,0);
                fragment.changeTrailTime(5);
                fragment.currentState = FragmentController.states.FOLLOW; 
            }
        } // so all 6 fragment have follow state now afer this
    }


    // Fragments Behaviour when player ENTER any world
    private void HandleEnterActions(FragmentController.world iWorld)
   {
        // If Flee -> change to FOLLOW
        FragmentController[] fragments = GameObject.FindObjectsOfType<FragmentController>();
        foreach (FragmentController fragment in fragments)
        {
            if (fragment.currentState == FragmentController.states.FLEE && fragment.currentWorld == iWorld)
            {
                fragment.currentState = FragmentController.states.FOLLOW;
                Debug.Log("Rejoining with fragments for : " + iWorld);
            }
        }
   }


    // Fragments behaviour when player EXIT any world
    private void HandleExitActions(FragmentController.world iWorld)
    {
        int n = 0; // count the fragments
        List<FragmentController> ToHandle = new List<FragmentController>();
       
        FragmentController[] fragments = GameObject.FindObjectsOfType<FragmentController>();
        foreach (FragmentController fragment in fragments)
        {
            if (fragment.currentState == FragmentController.states.FOLLOW && fragment.currentWorld == iWorld)
            {
                n++;
                ToHandle.Add(fragment);
            }
        }


        // If FOLLOW -> change to FLEE
        foreach (FragmentController fc in ToHandle)
        {
            if (n < maxToExit) // 
            {
                fc.currentState = FragmentController.states.FLEE;
            }
            else // move them to the void after few seconds
            {
                Debug.Log("Successfully exiting the world !");
            }
        }
    }


}
