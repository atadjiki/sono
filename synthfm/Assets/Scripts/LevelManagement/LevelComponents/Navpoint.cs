using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Navpoint : MonoBehaviour
{

    public GameObject target;
    public GameObject eyeball;
    public SphereCollider sphereCollider;

    // Update is called once per frame
    void Update()
    {
        //find vector on sphere to draw the pointer
        Vector3 position = sphereCollider.ClosestPoint(target.transform.position);
        Debug.Log("Position " + position);
        position.z = -1;

        float angle = Vector3.Angle(sphereCollider.transform.position, target.transform.position);

        
        eyeball.transform.position = position;
        Debug.DrawRay(transform.position, target.transform.position);
    }
}
