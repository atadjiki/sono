using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HubController : MonoBehaviour
{
    public GameObject EntryPoint;
    private float z_rot = 0.01f;
    private GameObject player;
    public float entryDistance = 100f;
    private bool stopRotating = false;
    public PolygonCollider2D wallcollider;
    public Rigidbody2D wallRigidBody;
    public float angle_offset = 90;

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
        if (stopRotating) return;

        Vector3 v_diff = (player.transform.position - EntryPoint.transform.position);
        float atan2 = Mathf.Atan2(v_diff.y, v_diff.x);
        transform.rotation = Quaternion.Euler(0f, 0f, (atan2 * Mathf.Rad2Deg)+angle_offset);

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
