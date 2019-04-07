using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HubController : MonoBehaviour
{
    public GameObject EntryPoint;
    public float z_rot = 0.5f;
    private GameObject player;
    public bool inEntryPoint = false;
    public bool insideBounds = false;
    public CircleCollider2D wallcollider;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
       // DoRotate();
        CheckBounds();
    }

    public void CheckBounds()
    {

        Debug.Log(insideBounds);
        if (!inEntryPoint && insideBounds)
        {
            Debug.Log("Not in entry point and inside bounds");

        }
        else if (!inEntryPoint && insideBounds == false)
        {
            Debug.Log("Not in entry point and outside bounds");
            transform.Rotate(0, 0, z_rot);
            Debug.Log("Rotating");
        }


    }
}
