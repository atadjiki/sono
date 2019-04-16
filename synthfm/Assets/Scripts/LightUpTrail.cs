using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Collider2D))]
public class LightUpTrail : MonoBehaviour
{
    Grid grid;
    Collider2D pathCollider;


    public enum PathMode
    {
        Open, Closed
    };
    public PathMode pathMode;

    public void Setup()
    {
        
    }

    void Validate()
    {
        grid = GetComponent<Grid>();
        if (grid == null)
            gameObject.AddComponent<Grid>();

        pathCollider = GetComponent<Collider2D>();

        if (pathMode == PathMode.Open)
            Debug.Assert(pathCollider is EdgeCollider2D);
        if (pathMode == PathMode.Closed)
            Debug.Assert(pathCollider is PolygonCollider2D);
    }

    void Initialize()
    {
        
    }

    private void Awake()
    {
        Initialize();
    }

    private void Reset()
    {
        GetComponent<Collider2D>().isTrigger = true;
    }
}
