using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Collider2D))]
public class LightUpTrail : MonoBehaviour
{
    Grid grid;
    Collider2D pathCollider;
    Vector2[] points;
    new LineRenderer renderer; //hides base member. We don't care.

    public float segmentSize;


    public enum PathMode
    {
        Open, Closed
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

        renderer = GetComponent<LineRenderer>();
        if (renderer == null)
            renderer = gameObject.AddComponent<LineRenderer>();

        if (pathMode == PathMode.Open)
        {
            Debug.Assert(pathCollider is EdgeCollider2D);
            points = ((EdgeCollider2D)pathCollider).points;
            renderer.loop = false;
        }
        if (pathMode == PathMode.Closed)
        {
            Debug.Assert(pathCollider is PolygonCollider2D);
            points = ((PolygonCollider2D)pathCollider).points;
            renderer.loop = true;
        }
        
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
