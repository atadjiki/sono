using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LattePuzzle : MonoBehaviour
{

    public float radius = 50f;
    public LineRenderer lineRenderer;

    public Color offColor;
    public Color onColor;

    private GameObject player;

    void Start()
    {
        player = GameObject.Find("Player");
        //make sure line positions are at zero Z
        NormalizePositions();

    }

    void Update()
    {
        
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
    }

    private void OnTriggerStay2D(Collider2D collision)
    {

        //get the positions in a radius around the player
        Vector3[] positions = new Vector3[lineRenderer.positionCount];
        List<Vector3> inRangePositions = new List<Vector3>();

        lineRenderer.GetPositions(positions);

        for (int i = 0; i < lineRenderer.positionCount; i++)
        {
            Vector3 tmp = lineRenderer.GetPosition(i);

            float distance = Vector3.Distance(player.transform.position, tmp);

            if(distance > radius)
            {
                inRangePositions.Add(tmp);
            }
        }

        lineRenderer.SetPositions(inRangePositions.ToArray());

    }
}
