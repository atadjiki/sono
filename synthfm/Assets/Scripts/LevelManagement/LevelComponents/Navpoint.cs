using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Navpoint : MonoBehaviour
{

    public GameObject sphere;
    public GameObject target;
    public GameObject pointer;
    public float minDistance = 200;
    public float radius = 50;
    public bool active = true;


    // Start is called before the first frame update
    void Start()
    {

        sphere.GetComponent<SphereCollider>().radius = radius;
        
    }

    // Update is called once per frame
    void Update()
    {

        if(active && Vector3.Distance(sphere.transform.position, target.transform.position) > minDistance)
        {
            pointer.gameObject.SetActive(true);

            //find vector on sphere to draw the pointer
            Vector3 position = sphere.GetComponent<SphereCollider>().ClosestPointOnBounds(target.transform.position);

            float angle = Vector3.Angle(sphere.transform.position, target.transform.position);
          //  Debug.Log("Angle between " + angle);
            Vector3 rotation = new Vector3(0, 0, angle);

            pointer.transform.position = position;

            pointer.transform.eulerAngles = rotation;
        }
        else
        {
            pointer.gameObject.SetActive(false);
        }
    }
}
