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

    public Transform[] Points;
    bool moveRight = true;
    bool ToMove = true;


    Vector2 A;
    Vector2 B;
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

        A = Points[0].position;
        B = Points[1].position;
        Debug.Log(A);
        //if (toStart)
        //{
        //    StartCoroutine(followCurve(currentRoute));
        //}

        //for (int i = 0; i < 4; i++)
        //{
        //    p[i] = tf[i].position;
        //}

        if (Input.GetKeyDown(KeyCode.L))
        {
            List<FragmentController> ToHandle = new List<FragmentController>();
            FragmentController[] fragments = GameObject.FindObjectsOfType<FragmentController>();
            foreach (FragmentController fragment in fragments)
            {
                if (fragment.currentState == FragmentController.states.FOLLOW)
                {
                    fragPos = fragment.gameObject.transform.position;
                    fragment.currentState = FragmentController.states.LEAD;
                  
                    //    Debug.Log("Leavoing Fragments behind");
                }
            }
        }
         
        //if (moveRight)
        //{ 
        //    moveright();
        //}
        //else
        //{
        //    moveleft();
        //}
    }

    void moveright()
    {
        
            Vector2 dir = Vector2.MoveTowards(fragPos, B, 0.5f * Time.deltaTime);
             Vector2 NewDir = new Vector2(dir.x, B.y );
            fragPos = NewDir;
            Debug.Log("Rightttt");

        if (fragPos.x >= B.x)
        {
            moveRight = false;
        }
    }

    void moveleft()
    {
        Vector2 dir = Vector2.MoveTowards(fragPos, A, 0.5f * Time.deltaTime);
        Vector2 NewDir = new Vector2(dir.x, B.y);
        fragPos = NewDir;
       
        Debug.Log("Lefttttt");

        if (fragPos.x <= A.x)
        {
            moveRight = true;
        }
    }

    IEnumerator followCurve(int iRoute)
    {

        toStart = false;

        while (t_Param < 1)
        {
            t_Param += Time.deltaTime * speed;

            fragPos = Mathf.Pow(1 - t_Param, 3) * tf[0].position +
                3 * Mathf.Pow(1 - t_Param, 2) * t_Param * tf[1].position +
                3 * (1 - t_Param) * Mathf.Pow(t_Param, 2) * tf[2].position +
                Mathf.Pow(t_Param, 3) * tf[3].position;

            //   transform.position = fragPos;
            yield return new WaitForEndOfFrame();
        }

        t_Param = 0f;
        toStart = true;
    }
}
