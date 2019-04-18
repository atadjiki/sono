using UnityEngine;

[ExecuteInEditMode]
//[RequireComponent(typeof(Collider2D))]
public class LightUpTrail : MonoBehaviour
{
    Grid grid;
    Collider2D pathCollider;
    curveDrawer CurveDrawer;
    Vector2[] points;
    new LineRenderer renderer; //hides base member. We don't care.
    public float Smoothing;

    public float segmentSize;


    public enum PathMode
    {
        Open, Closed, Bezier
    };
    public PathMode pathMode;

    public void Setup()
    {
        Validate();

        Vector3[] linePoints = new Vector3[points.Length];
        for (int i = 0; i < points.Length; i++)
        {
            linePoints[i].x = points[i].x;
            linePoints[i].y = points[i].y;
            linePoints[i].z = transform.position.z;
        }
        renderer.positionCount = linePoints.Length;
        renderer.SetPositions(linePoints);
        renderer.enabled = true;
    }

    void Validate()
    {
        grid = GetComponent<Grid>();
        if (grid == null)
            grid = gameObject.AddComponent<Grid>();

        pathCollider = GetComponent<Collider2D>();
        CurveDrawer = GetComponent<curveDrawer>();

        renderer = GetComponent<LineRenderer>();
        if (renderer == null)
            renderer = gameObject.AddComponent<LineRenderer>();

        if (pathMode == PathMode.Open)
        {
            Debug.Assert(pathCollider is EdgeCollider2D);
            points = ((EdgeCollider2D)pathCollider).points;
            renderer.loop = false;
        }
        else if (pathMode == PathMode.Closed)
        {
            Debug.Assert(pathCollider is PolygonCollider2D);
            points = ((PolygonCollider2D)pathCollider).points;
            renderer.loop = true;
        }
        else if(pathMode == PathMode.Bezier)
        {
            Debug.Assert(CurveDrawer != null);
            renderer.loop = false;
            CurveDrawer.SetupPositions(Smoothing);
            points = CurveDrawer.positions.ToArray();

            SetEdgeColliderToMatchPath();
        }
        
    }

    void SetEdgeColliderToMatchPath()
    {
        if ((pathCollider == null) || (pathCollider is PolygonCollider2D))
            pathCollider = gameObject.AddComponent<EdgeCollider2D>();
        Vector2[] colliderPoints = new Vector2[points.Length];
        for(int i = 0; i < points.Length; i++)
        {
            colliderPoints[i].x = points[i].x - transform.position.x;
            colliderPoints[i].y = points[i].y - transform.position.y;
        }
        ((EdgeCollider2D)pathCollider).points = colliderPoints;
    }

    void Initialize()
    {
        Validate();
    }

    private void Awake()
    {
        Initialize();
        renderer.enabled = false;
    }

    private void Reset()
    {
        GetComponent<Collider2D>().isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        renderer.enabled = true;
    }

    
}
