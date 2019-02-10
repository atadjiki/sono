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
                if (CheckForClosedLoop())
                {
                    complete = true;
                    ReleaseCage();
                }
                currentFrames = 0;
            }
            else
            {
                currentFrames++;
            }
        }


    }

    bool CheckForClosedLoop()
    {
        int lookBehind = 1024 * 1024;
        Vector3[] positions = new Vector3[lookBehind];
        int result = playerTrail.GetPositions(positions);

        foreach(Vector3 position in positions)
        {
            float xDist = Mathf.Abs(position.x - playerTransform.position.x);
            float yDist = Mathf.Abs(position.y - playerTransform.position.y);

            if(xDist > 0 && xDist < 1 && yDist > 0 && yDist < 1)
            {
                Debug.Log("Loop closed!");
                return true;
            }

        }

        return false;
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
        //lower the force field and turn off its noise
        forceField.GetComponent<PointEffector2D>().enabled = false;
        forceField.GetComponent<AudioSource>().enabled = false;
        ParticleSystem[] particles = forceField.GetComponentsInChildren<ParticleSystem>();

        foreach (ParticleSystem particle in particles)
        {
            particle.Stop(); //Stop the animations instead of destroying them for the dissipation effect 
        }
    }

}
