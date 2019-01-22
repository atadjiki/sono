using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Navpoint : MonoBehaviour
{

    public GameObject sphere;
    public GameObject target;
    public GameObject pointer;
    public float minDistance = 200;
    private Vector3 center;
    public float radius = 50;



    // Start is called before the first frame update
    void Start()
    {

        sphere.GetComponent<SphereCollider>().radius = radius;
        
    }

    // Update is called once per frame
    void Update()
    {

        if(Vector3.Distance(sphere.transform.position, target.transform.position) > minDistance)
        {
            pointer.gameObject.SetActive(true);
            pointer.transform.position = sphere.GetComponent<SphereCollider>().ClosestPointOnBounds(target.transform.position);
        }
        else
        {
            pointer.gameObject.SetActive(false);
        }

 

    }
}
