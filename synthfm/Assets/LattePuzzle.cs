using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LattePuzzle : MonoBehaviour
{
    public int maxRemove = 25;
    public float radius = 50f;
    public LineRenderer lineRenderer;

    public Color offColor;
    public Color onColor;

    private GameObject player;

    private int maxFrames = 120;
    private int currentFrames = 0;

    void Start()
    {
        player = GameObject.Find("Player");
        //make sure line positions are at zero Z
        NormalizePositions();
    }

    void Update()
    {
        if(currentFrames >= maxFrames)
        {
            CheckNearbyPoints();
            currentFrames = 0;
        }
        else
        {
            currentFrames++;
        }
    }

    void NormalizePositions()
    {

        Vector3[] positions = new Vector3[lineRenderer.positionCount];

        lineRenderer.GetPositions(positions);

        for (int i = 0; i < lineRenderer.positionCount; i++)
        {
            Vector3 tmp = lineRenderer.GetPosition(i);
            tmp.z = 0;
            lineRenderer.SetPosition(i, tmp);
        }

        Debug.Log("Found " + positions.Length + " positions");
    }

    void CheckNearbyPoints()
    {
        //get the positions in a radius around the player
        Vector3[] fetchVectors = new Vector3[lineRenderer.positionCount];
        lineRenderer.GetPositions(fetchVectors);



        List<Vector3> positions = new List<Vector3>(fetchVectors);
        Debug.Log(positions.Count + " positions found");
        List<Vector3> inRangePositions = new List<Vector3>();


        for (int i = 0; i < lineRenderer.positionCount; i++)
        {
            Vector3 tmp = lineRenderer.GetPosition(i);

            float distance = Vector3.Distance(player.transform.position, tmp);

            if (distance <= radius)
            {
             //   Debug.Log("Found Point");
                inRangePositions.Add(tmp);
            }
            
        }

        if(inRangePositions.Count > 0 && inRangePositions.Count >= maxRemove)
        {
            for (int i = 0; i < maxRemove; i++)
            {
                positions.Remove(inRangePositions[i]);
            }
        }else if(inRangePositions.Count > 0 && inRangePositions.Count < maxRemove)
        {
            for (int i = 0; i < inRangePositions.Count; i++)
            {
                positions.Remove(inRangePositions[i]);
            }
        }
        

        Debug.Log(positions.Count + " positions left");

        lineRenderer.SetPositions(positions.ToArray());
    }
  
}
