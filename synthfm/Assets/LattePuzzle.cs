using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LattePuzzle : MonoBehaviour
{
    private int maxRemove = 25;
    private float radius = 50f;
    private LineRenderer lineRenderer;

    private Color offColor;
    private Color onColor;

    private GameObject player;

    private int maxFrames = 120;
    private int currentFrames = 0;

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        FXToggle.instance.TogglePlayerFog(this.gameObject, true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        FXToggle.instance.TogglePlayerFog(this.gameObject, false);
    }

}
