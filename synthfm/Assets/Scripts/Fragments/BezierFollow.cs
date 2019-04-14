using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierFollow : MonoBehaviour
{
    public float speed;

    [SerializeField]
    private Transform[] routes; // the route for the fragment
    private int currentRoute;
    private float t_Param;
    public Vector2 fragPos;

    private bool toStart; // whether to start a new curve

    Vector2[] p = new Vector2[4];
    public Transform[] tf = new Transform[4];
  
    // Start is called before the first frame update
    void Start()
    {
        currentRoute = 0;
        t_Param = 0f;
        speed = 0.6f;
        toStart = true;
       
        for (int i = 0; i < 4; i++)
        {
            tf[i] = routes[0].GetChild(i).transform;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (toStart)
        {
            StartCoroutine(followCurve(currentRoute));
        }
      
        for (int i = 0; i < 4; i++)
        {
            p[i] = tf[i].position;
        }
       
        if (Input.GetKeyDown(KeyCode.L))
        {
            List<FragmentController> ToHandle = new List<FragmentController>();
            FragmentController[] fragments = GameObject.FindObjectsOfType<FragmentController>();
            foreach (FragmentController fragment in fragments)
            {
                if (fragment.currentState == FragmentController.states.FOLLOW)
                {
                    fragment.currentState = FragmentController.states.LEAD;
                    //    Debug.Log("Leavoing Fragments behind");
                }
            }
        }
    }

    IEnumerator followCurve(int iRoute)
    {

        toStart = false;

        while (t_Param < 1)
        {
            t_Param += Time.deltaTime * speed;

            fragPos = Mathf.Pow(1 - t_Param, 3) * p[0] +
                3 * Mathf.Pow(1 - t_Param, 2) * t_Param * p[1] +
                3 * (1 - t_Param) * Mathf.Pow(t_Param, 2) * p[2] +
                Mathf.Pow(t_Param, 3) * p[3];

            //   transform.position = fragPos;
            yield return new WaitForEndOfFrame();
        }

        t_Param = 0f;
        toStart = true;
    }
}
