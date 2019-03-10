using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Navpoint : MonoBehaviour
{

    private GameObject target;
    public GameObject eyeball;
    public GameObject centerOfEye;
    public SphereCollider sphereCollider;

    private float maxFrames = 120f;
    private float currentFrames = 0;
    private const int maxFragments = 3;

    private DepositFragments[] depositZones;
    private Puzzle[] puzzles;

    private void Start()
    {
        CheckForNewTarget();
    }

    // Update is called once per frame
    void Update()
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

    public void SetTarget(GameObject newTarget)
    {
        target = newTarget;
        currentFrames = 0;
    }

    public void CheckForNewTarget()
    {
        if (FragmentManager.instance.CountAttachedFragments() >= maxFragments)
        {
            CheckForNewDepositZone();
        }
        else
        {
            CheckForNewPuzzle();
        }
    }

    void CheckForNewDepositZone()
    {
        depositZones = FindObjectsOfType<DepositFragments>();

        if (depositZones.Length <= 0) { CheckForNewPuzzle(); }

        float minimumDistance = 0;
        DepositFragments closestDepositZone = null;

        foreach (DepositFragments depositZone in depositZones)
        {
            float distance = Vector3.Distance(transform.position, depositZone.transform.position);
            if (distance <= minimumDistance || minimumDistance <= 0)
            {
                minimumDistance = distance;
                closestDepositZone = depositZone;
            }
        }

        if(closestDepositZone == null)
        {
            target = centerOfEye;
            Debug.Log("No target for navpoint at the moment!");
        }
        else
        {
            target = closestDepositZone.gameObject;
        }

        Debug.Log("Found deposit zone at " + target.transform.position);

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
            target = centerOfEye;
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
}
