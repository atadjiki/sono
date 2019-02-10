using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Boilerplate for a new puzzle 
 */
public class CirclePuzzle : Puzzle
{

    public TrailRenderer playerTrail;
    public Transform playerTransform;

    private int currentFrames = 0;
    private int maxFrames = 60;

    private bool playerEntered = false;

    public GameObject forceField;

    public GameObject[] targets;

    float threshhold = 1f;
    Vector3[] positions = new Vector3[1024];

    // Start is called before the first frame update
    void Awake()
    {
        base.Initialize(); //Make sure you include this call!
    }

    // Update is called once per frame
    void Update()
    {
        if (!complete && playerEntered)
        {
            if (currentFrames == maxFrames)
            {
                Debug.Log("Checking for loop");
                bool DoesContain = false;
                if (CheckForClosedLoop(ref DoesContain))
                {
                    if (DoesContain)
                    {
                        complete = true;
                        ReleaseCage();
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

        Debug.Log("Found " + playerTrail.positionCount + " vertices in trail");
        positions = new Vector3[playerTrail.positionCount];
        int result = playerTrail.GetPositions(positions);
        bool closed = false;
        Vector3 intersection = new Vector3();


        foreach (Vector3 position in positions)
        {
            float xDist = Mathf.Abs(position.x - playerTransform.position.x);
            float yDist = Mathf.Abs(position.y - playerTransform.position.y);

            if (xDist < threshhold && yDist < threshhold)
            {
                Debug.Log("Loop closed!");
                Debug.Log(position.ToString() + ", collided with " + playerTransform.position.ToString());
                closed = true;
                intersection = position;
                break;
            }

        }

        if (closed)
        {
            DoesContain = CheckObjectsInside(intersection);
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

    void ReleaseCage()
    {

        foreach(GameObject target in targets)
        {
            Destroy(target);
        }

        //lower the force field and turn off its noise
        forceField.GetComponent<PointEffector2D>().enabled = false;
        forceField.GetComponent<AudioSource>().enabled = false;
        ParticleSystem[] particles = forceField.GetComponentsInChildren<ParticleSystem>();

        foreach (ParticleSystem particle in particles)
        {
            particle.Stop(); //Stop the animations instead of destroying them for the dissipation effect 
        }
    }

    bool CheckObjectsInside(Vector3 intersection)
    {

        List<Vector3> positionsToCheck = new List<Vector3>(positions);
        int startIndex = positionsToCheck.IndexOf(intersection);
        positionsToCheck = positionsToCheck.GetRange(startIndex, positionsToCheck.Count - startIndex);

        foreach(GameObject target in targets)
        {
            if(!ContainsPoint(positionsToCheck.ToArray(), target.transform.position))
            {
                return false;
            }
        }

        Debug.Log("Loop contains targets!");
        return true;
    }

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
