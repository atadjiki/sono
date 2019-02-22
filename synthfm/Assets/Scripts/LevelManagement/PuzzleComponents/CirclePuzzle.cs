using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * A puzzle in which the player must encircle certain targets with their 
 * particle trail in order to unlock a fragment
 */
public class CirclePuzzle : Puzzle
{

    public TrailRenderer playerTrail;
    public Transform playerTransform;

    private int currentFrames = 0;
    private int maxFrames = 60 / 3; //how frequently we want to check for an intersection 

    private bool playerEntered = false; //only check if the player is actually inside the puzzle!

    public GameObject[] targets; // game objects we want the player to encircle 

    float threshhold = 1f; //how close the player can be to a trail vertice count as an intersection 
    Vector3[] positions = new Vector3[1024];

    public new void DoSetup()
    {
        base.DoPuzzleSetup();
        playerTrail = GameObject.Find("MainTrail").GetComponent<TrailRenderer>();
        playerTransform = GameObject.Find("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void FixedUpdate() //FixedUpdate so we can still call update on Puzzle
    {
        if (!complete && playerEntered)
        {
            if (currentFrames == maxFrames)
            {
            //    Debug.Log("Checking for loop");
                bool DoesContain = false;
                if (CheckForClosedLoop(ref DoesContain)) //check if the player has drawn an enclosed shaped with their trail
                {
                    if (DoesContain) //check if that enclosed shape contains all of the targets 
                    {
                        complete = true; //notify the base class this puzzle is complete
                        ReleaseCage(); //release the forcefield 
                    }

                }
                currentFrames = 0;
            }
            else
            {
                currentFrames++;
            }
        }


    }

    bool CheckForClosedLoop(ref bool DoesContain)
    {

       // Debug.Log("Found " + playerTrail.positionCount + " vertices in trail");
        positions = new Vector3[playerTrail.positionCount];
        int result = playerTrail.GetPositions(positions); //find how many vertices are in the player's trail
        bool closed = false;
        Vector3 intersection = new Vector3(); //we want to know where the player collided if they do 


        foreach (Vector3 position in positions)
        {
            float xDist = Mathf.Abs(position.x - playerTransform.position.x);
            float yDist = Mathf.Abs(position.y - playerTransform.position.y);

            if (xDist < threshhold && yDist < threshhold) //if the player was close enough, claim this as a loop
            {
              //  Debug.Log("Loop closed!");
            //    Debug.Log(position.ToString() + ", collided with " + playerTransform.position.ToString());
                closed = true;
                intersection = position;
                break;
            }

        }

        if (closed)
        {
            DoesContain = CheckObjectsInside(intersection); //check if targets are within enclosed shape
            return closed;
        }
        else
        {
            return false;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Enter Circle Puzzle");
        base.TriggerEnter(collision);
        playerEntered = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("Exit Circle Puzzle");
        base.TriggerExit(collision);
        playerEntered = false;
    }

    new void ReleaseCage()
    {
        base.ReleaseCage();

        foreach (GameObject target in targets)
        {
            Destroy(target);
        }

        
    }

    bool CheckObjectsInside(Vector3 intersection)
    {

        //we only want a subsection of the player's trail because some of it might not be part of their shape
        List<Vector3> positionsToCheck = new List<Vector3>(positions);
        int startIndex = positionsToCheck.IndexOf(intersection);
        positionsToCheck = positionsToCheck.GetRange(startIndex, positionsToCheck.Count - startIndex);

        foreach (GameObject target in targets)
        {
            if (!ContainsPoint(positionsToCheck.ToArray(), target.transform.position))
            {
                return false; //make sure every target is inside the shape
            }
        }

        Debug.Log("Loop contains targets!");
       // GenerateMesh(positionsToCheck.ToArray(), intersection);
        return true;
    }

    //returns true if the provided point is 'inside' the bounds of a polygon 
    static bool ContainsPoint(Vector3[] polyPoints, Vector3 p)
    {
        int j = polyPoints.Length - 1;
        bool inside = false;
        for (int i = 0; i < polyPoints.Length; j = i++)
        {
            if (((polyPoints[i].y <= p.y && p.y < polyPoints[j].y) || (polyPoints[j].y <= p.y && p.y < polyPoints[i].y)) &&
               (p.x < (polyPoints[j].x - polyPoints[i].x) * (p.y - polyPoints[i].y) / (polyPoints[j].y - polyPoints[i].y) + polyPoints[i].x))
                inside = !inside;
        }
        return inside;
    }

}
