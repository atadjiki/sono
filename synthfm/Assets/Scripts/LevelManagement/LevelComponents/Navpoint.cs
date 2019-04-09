using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Navpoint : MonoBehaviour
{

    private GameObject target;
    public GameObject eyeball;
    public GameObject centerOfEye;
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

    private void Start()
    {
        //Lock();
        locked = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!locked)
        {
            if (currentFrames >= maxFrames)
            {
                CheckForNewTarget();
                currentFrames = 0;
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

    public void CheckForNewTarget()
    {
        //if (FragmentManager.instance.CountAttachedFragments() >= maxFragments)
        //{
            //TO DO: Navpoint pointing to hub
        //}
        //else
        //{
            //`CheckForNewPuzzle();
            CheckForNewFragment();
        //}
    }

    void CheckForNewFragment()
    {
        fragments = FindObjectsOfType<FragmentController>();
       
        if (fragments.Length <= 0) { return; }

        float minimumDistance = 0;
        FragmentController closestFragment = null;

        foreach (FragmentController fragment in fragments)
        {
            if (fragment.currentState != FragmentController.states.FOLLOW)
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
            if (!puzzle.complete)
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
