using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this might be attached to Fragment
public class PatternGenerator : MonoBehaviour
{
    public float speed;

    [SerializeField]
    private Transform[] routes; // the route for the fragment
    private int currentRoute;
    private float t_Param;
    public Vector2 fragPos;

    private bool toStart; // whether to start a new curve
    private bool ToBegin = false;
    //Vector2[] p = new Vector2[4];
    //public Transform[] tf = new Transform[4];

    // Start is called before the first frame update
    void Start()
    {
        currentRoute = 0;
        t_Param = 0f;
       // speed = 0.6f;
        toStart = true;
        fragPos = this.gameObject.transform.position;
        //for (int i = 0; i < 4; i++)
        //{
        //    tf[i] = routes[0].GetChild(i).transform;
        //}

    }

    // Update is called once per frame
    void Update()
    {
       
        if (Input.GetKeyDown(KeyCode.M))
        {
            ToBegin = true;
        }

        //if(Input.GetKeyDown(KeyCode.W))
        //{
        //    List<FragmentController> ToHandle = new List<FragmentController>();
        //    FragmentController[] fragments = GameObject.FindObjectsOfType<FragmentController>();
        //    foreach (FragmentController fragment in fragments)
        //    {
        //        if (fragment.currentState == FragmentController.states.FINAL_PATERN)
        //        {
        //          //  fragPos = fragment.gameObject.transform.position;
        //            fragment.Collect(LevelManager.instance.getPlayer().transform);
        //            fragment.currentState = FragmentController.states.FOLLOW;
                 
        //            //    Debug.Log("Leavoing Fragments behind");
        //        }
        //    }
        //}
    

        if (ToBegin)
        {
            if (toStart)
            {
                StartCoroutine(followCurve(currentRoute));
            }
        }
    }

    IEnumerator followCurve(int iRoute)
    {
        List<Vector3> Points = new List<Vector3>();
        for (int i = 0; i < routes[iRoute].childCount; i++)
        {
            Points.Add(routes[iRoute].GetChild(i).position);
        }
        toStart = false;

        while (t_Param < 1)
        {
            t_Param += Time.deltaTime * speed;

            fragPos = Mathf.Pow(1 - t_Param, 3) * Points[0] +
                3 * Mathf.Pow(1 - t_Param, 2) * t_Param * Points[1] +
                3 * (1 - t_Param) * Mathf.Pow(t_Param, 2) * Points[2] +
                Mathf.Pow(t_Param, 3) * Points[3];

            //   transform.position = fragPos;
            yield return new WaitForEndOfFrame();

        }

        t_Param = 0f;
        toStart = true;

        currentRoute++;
        if(currentRoute >= routes.Length)
        {
            currentRoute = 0;
        }

    }
}
