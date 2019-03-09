using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Navpoint : MonoBehaviour
{

    private GameObject target;
    public GameObject eyeball;
    public SphereCollider sphereCollider;

    private float maxFrames = 120f;
    private float currentFrames = 0;

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
        }
        else
        {
            currentFrames++;
        }


        MoveEyeball();
    }

    void SetTarget(GameObject newTarget)
    {
        target = newTarget;
    }

    void CheckForNewTarget()
    {
        Puzzle[] puzzles = FindObjectsOfType<Puzzle>();

        if (puzzles.Length <= 0) { return; }


        Debug.Log("Found " + puzzles.Length + " incomplete puzzles");

        float minimumDistance = 0;
        Puzzle closestPuzzle = puzzles[0];

        foreach (Puzzle puzzle in puzzles)
        {
            if (puzzle.complete)
            {
                float distance = Vector3.Distance(transform.position, puzzle.transform.position);
                if (distance <= minimumDistance || minimumDistance <= 0)
                {
                    minimumDistance = distance;
                    closestPuzzle = puzzle;
                }
            }

        }

        target = closestPuzzle.gameObject;
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
        Debug.DrawRay(transform.position, target.transform.position);
    }
}
