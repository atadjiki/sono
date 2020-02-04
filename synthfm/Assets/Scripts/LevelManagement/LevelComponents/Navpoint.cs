using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Navpoint : MonoBehaviour
{

    public GameObject target;
    public GameObject eyeball;
    public GameObject centerOfEye;
    public Transform hub;
    public SphereCollider sphereCollider;
    public bool locked = true;

    public float maxFrames = 120f;
    public float currentFrames = 0;
    public int maxFragments = 3;

    private Puzzle[] puzzles;
    private FragmentController[] fragments;

    public bool canTargetAmberWorld = true;
    public bool enteredAmberWorld = false;

    public bool canTargetFiberWorld = true;
    public bool enteredFiberWorld = false;

    public bool canTargetLatteWorld = true;
    public bool enteredLatteWorld = false;

    public GameObject amberWorld;
    public GameObject fiberWorld;
    public GameObject latteWorld;
    public GameObject hubWorld;

    public GameObject amberWorldTrigger;
    public GameObject fiberWorldTrigger;
    public GameObject latteWorldTrigger;
    public GameObject hubWorldTrigger;

    private int amberFragments;
    private int latteFragments;
    private int fiberFragments;

    public GameObject finalZone;

    public bool pointToFinalZone = false;

    private void Start()
    {
        //Lock();
        locked = false;
        fiberWorldTrigger = GameObject.FindGameObjectWithTag("Realm2");
        latteWorldTrigger = GameObject.FindGameObjectWithTag("Realm3");

        amberFragments = 0;
        latteFragments = 0;
        fiberFragments = 0;

    }

    // Update is called once per frame
    void Update()
    {
        //print(FragmentManager.instance.CountAttachedFragments());
        if (!locked)
        {

            
            if (currentFrames >= maxFrames)
            {
                if (pointToFinalZone)
                {
                    SetTarget(finalZone);
                }
                else
                {
                    SortFragments();

                    if ((amberFragments >= maxFragments) && (fiberFragments >= maxFragments) && (latteFragments >= maxFragments))
                    {
                        CheckForHub();
                    }

                    CheckForNewTarget();
                    currentFrames = 0;
                }
            }
            else
            {
                currentFrames++;
            }


            MoveEyeball();
        }
       
    }

    public void SetTarget(GameObject newTarget)
    {
        target = newTarget;
        currentFrames = 0;
    }

    private void SortFragments()
    {
        List<FragmentController> temp = FragmentManager.instance.fragments;

        amberFragments = 0;
        latteFragments = 0;
        fiberFragments = 0;

        foreach (FragmentController fragment in temp)
        {
            if(fragment.currentWorld == FragmentController.world.AMBER && fragment.currentState == FragmentController.states.FOLLOW)
            {
                amberFragments += 1;
            }
            else if(fragment.currentWorld == FragmentController.world.FIBER && fragment.currentState == FragmentController.states.FOLLOW)
            {
                fiberFragments += 1;
            }
            else if (fragment.currentWorld == FragmentController.world.LATTE && fragment.currentState == FragmentController.states.FOLLOW)
            {
                latteFragments += 1;
            }

        }
    }

    public void CheckForNewTarget()
    {

        if (amberFragments >= maxFragments || fiberFragments >=maxFragments || latteFragments>=maxFragments)
        {
            if(!CheckForNewWorld())
            {
                CheckForNewFragment();
            }
        }

        else
        {
            CheckForNewFragment();
          
        }
    }

    public void CheckForHub()
    {
        target = hub.gameObject;
    }

    public bool CheckForNewWorld()
    {
        Vector3 playerPos = GameObject.Find("Player").transform.position;

        if ((GameObject.Find("LatteWorld")) == null && (GameObject.Find("FiberWorld")) == null)
        {
            float FDistance = Vector3.Distance(transform.position, fiberWorldTrigger.transform.position);
            float LDistance = Vector3.Distance(transform.position, latteWorldTrigger.transform.position);

          //  print("FDistance: " +FDistance);
           // print("LDistance: " + LDistance);


            if ((FDistance < LDistance) && (FDistance > 0f && LDistance > 0f))
            {
              //  print("Target is fiber world ");
                target = fiberWorldTrigger.gameObject;
            }
            else
            {
             //   print("Target is latte world ");

                target = latteWorldTrigger.gameObject;
            }

            return true;
        }
        if ((GameObject.Find("LatteWorld")) == null && fiberFragments >= maxFragments)
        {
            target = latteWorldTrigger.gameObject;
            return true;
        }
        else if ((GameObject.Find("FiberWorld")) == null && latteFragments >=maxFragments)
        {
            target = fiberWorldTrigger.gameObject;
            return true;
        }
        else
        {
            return false;
        }
    }


    void CheckForNewFragment()
    {
        fragments = FindObjectsOfType<FragmentController>();
       
        if (fragments.Length <= 0) { return; }

        float minimumDistance = 0;
        FragmentController closestFragment = null;

        foreach (FragmentController fragment in fragments)
        {
            if (fragment.currentState == FragmentController.states.IDLE)
            {
                float distance = Vector3.Distance(transform.position, fragment.transform.position);
                if (distance <= minimumDistance || minimumDistance <= 0)
                {
                    minimumDistance = distance;
                    closestFragment = fragment;
                }
            }

        }
        if (closestFragment == null)
        {

            if (fiberWorld != null && canTargetFiberWorld)
            {
                Debug.Log("Switching target to fiber world");
                target = fiberWorld;
            }
            else
            {
                target = centerOfEye;
            }
        }
        else
        {
            target = closestFragment.gameObject;
        }
    }

    void CheckForNewPuzzle()
    {
        puzzles = FindObjectsOfType<Puzzle>();

        if (puzzles.Length <= 0) { return; }

        float minimumDistance = 0;
        Puzzle closestPuzzle = null;

        foreach (Puzzle puzzle in puzzles)
        {
            if (!puzzle.Complete)
            {
                if(enteredFiberWorld && canTargetFiberWorld)
                {
                    canTargetFiberWorld = false;
                }


                float distance = Vector3.Distance(transform.position, puzzle.transform.position);
                if (distance <= minimumDistance || minimumDistance <= 0)
                {
                    minimumDistance = distance;
                    closestPuzzle = puzzle;
                }
            }

        }
        if(closestPuzzle == null)
        { 

            if(fiberWorld != null && canTargetFiberWorld)
            {
                Debug.Log("Switching target to fiber world");
                target = fiberWorld;
            }
            else
            {
                target = centerOfEye;
            }
        }
        else
        {
            target = closestPuzzle.gameObject;
        }

    }

    void MoveEyeball()
    {
        if (target == null) { return; }

        //find vector on sphere to draw the pointer
        Vector3 position = sphereCollider.ClosestPoint(target.transform.position);
        // Debug.Log("Position " + position);
        position.z = -1;

        float angle = Vector3.Angle(sphereCollider.transform.position, target.transform.position);


        eyeball.transform.position = position;
        //    Debug.DrawRay(transform.position, target.transform.position);
    }

    public void Unlock()
    {
        locked = false;
        CheckForNewTarget();
    }

    public void Lock()
    {
        locked = true;
        target = centerOfEye;
    }
}
