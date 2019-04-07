using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HubController : MonoBehaviour
{
    public GameObject EntryPoint;
    public float z_rot = 0.5f;
    private GameObject player;
    public float entryDistance = 100f;
    private bool stopRotating = false;
    public PolygonCollider2D wallcollider;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        DoRotate();
        CheckEntryPoint();
    }
    
    public void DoRotate()
    {
        if (stopRotating) transform.Rotate(0, 0, z_rot/4); ;

        transform.Rotate(0, 0, z_rot);

    }

    public void CheckEntryPoint()
    {

        if (wallcollider.bounds.Contains(player.transform.position))
        {
            Debug.Log("Player inside bounds");
        }
        else
        {

            if (Vector3.Distance(EntryPoint.transform.position, player.transform.position) <= entryDistance)
            {
            
                stopRotating = true;
                Debug.Log("Player near entry point");
            }
            else
            {
                Debug.Log("Player outside and away from entry point " + Vector3.Distance(player.transform.position, EntryPoint.transform.position).ToString());
            }

        }

        
        
    }

}
