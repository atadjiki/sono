using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this might be attached to Fragment
public class PatternGenerator : MonoBehaviour
{
    public float speed;

    public GameObject CurvePair;
   
    private Transform[] routes; // the route for the fragment
    private int currentRoute;
    private float t_Param;
    public Vector2 fragPos;

    [Header("Trail Time")]
    public float trailTime;

    private bool toStart; // whether to start a new curve
    public bool ToBegin = false;
    private Transform m_startingPoint;
    //Vector2[] p = new Vector2[4];
    //public Transform[] tf = new Transform[4];

    // Start is called before the first frame update
    void Start()
    {
        currentRoute = 0;
        t_Param = 0f;
       // speed = 0.6f;
        toStart = true;

        fragPos = transform.position;
        m_startingPoint = routes[0].GetChild(0).gameObject.transform;
       // changeTrailTimes(trailTime);
    }

    private void Awake()
    {
        // assign curve
        curveManager toAssign = GameObject.Find("Final Pattern Zone").GetComponent<curveManager>();
        FragmentController fc = gameObject.GetComponent<FragmentController>();
        switch(fc.currentWorld)
        {
            case FragmentController.world.AMBER:
                CurvePair = toAssign.Curves[fc.TrackIndex];
                break;
            case FragmentController.world.FIBER:
                CurvePair = toAssign.Curves[fc.TrackIndex + 3];
                break;
            case FragmentController.world.LATTE:
                CurvePair = toAssign.Curves[fc.TrackIndex + 6];
                break;
        }

        if(CurvePair == null)
        {
            Debug.Log("ERROR:: PAtterns not Assigned Successfully");
        }

        routes = new Transform[CurvePair.transform.childCount];
        for (int i = 0; i < routes.Length; i++)
        {
            routes[i] = CurvePair.transform.GetChild(i);
        }
    }

    void changeTrailTime(float i_time)
    {
        TrailRenderer tr = this.gameObject.transform.Find("Trail").GetComponent<TrailRenderer>();
        tr.time = i_time;
    }

    public Transform getStartingPoint()
    {
        return m_startingPoint;
    }

    // Update is called once per frame
    void Update()
    {
       
        //if (Input.GetKeyDown(KeyCode.M))
        //{
        //    ToBegin = true;
        //}

        if (ToBegin)
        {
            if (toStart)
            {
                StartCoroutine(followCurve(currentRoute));
            }
        }
        else
        {
            fragPos = transform.position;
        }
    }

    public void startCurve()
    {
        ToBegin = true;
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
