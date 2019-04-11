using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// attached to every Crystall

public class Crystal: MonoBehaviour
{
    [Header(" The Sequence number for Seq mode")]
    public int sequenceNo;

    [Header("Colors for the Crystalls")]
    public Color baseColor;
    public Color errorColor;
    public Color activeColor;

    [Header("The State Of this Crystal ON = Active")]
    public ClusterManager.State _state = ClusterManager.State.OFF;

    [Header("Rotation Speed")]
    public float speed = 0f;

    [Header("Error State Timing in Seconds")]
    public float _errorTime = 1f;

    [Header("Direction")]
    public bool ForwardZ = false;
    public bool ReverseZ = false;

    // Shake Stuff
    private Transform transform;
    private float shakeDuration;
    public float shakeMagnitude = 0.2f;
    public float dampingSpeed = 5.0f;
    Vector3 initialPosition;

    private SpriteRenderer _renderer;
    private Color currentColor;

    // essentials
    private ClusterManager _cMamnager;

    private void Awake()
    {
        if (transform == null)
        {
            transform = GetComponent(typeof(Transform)) as Transform;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // get
        _cMamnager = transform.parent.gameObject.GetComponent<ClusterManager>();

        _renderer = this.GetComponent<SpriteRenderer>();
        _renderer.color = baseColor;
        currentColor = baseColor;

        transform = this.GetComponent<Transform>();
        initialPosition = transform.localPosition;
        shakeDuration = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if ((_state == ClusterManager.State.OFF)) // notify if this OFF
            {
                _cMamnager._Notify(this);
            }
        }
    }

    void Update()
    {
        // ROtation
            if (ForwardZ == true)
            {
                transform.Rotate(0, 0, Time.deltaTime * speed, Space.Self);
            }
            if (ReverseZ == true)
            {
                transform.Rotate(0, 0, -Time.deltaTime * speed, Space.Self);
            }

        // shaky shaky
        if (shakeDuration > 0)
        {
            transform.localPosition = initialPosition + Random.insideUnitSphere * shakeMagnitude;
            shakeDuration -= Time.deltaTime * dampingSpeed;
        }
        //else
        //{
        //    //shakeDuration = 0f;
        //    //transform.localPosition = initialPosition;
        //}

    }


    public void changeToActive()
    {
        // shaky shaky
        shakeDuration = 0.5f;

        _state = ClusterManager.State.ON;
        // currentColor = activeColor;
        _renderer.color = activeColor;

        int x = Random.Range(0, 3);
        if(x == 0)
        {
            ReverseZ = true;
        }
        else{
            ForwardZ = true;
        }
        speed = Random.Range(10,15);

      
    }

    public void changeToFail()
    {
        _state = ClusterManager.State.Error;
        _renderer.color = errorColor;

        StartCoroutine(setBaseColor());

        ForwardZ = false;
        ReverseZ = false;
        speed = 0;
   
    }
   
    IEnumerator setBaseColor()
    {
        yield return new WaitForSeconds(_errorTime);
        _renderer.color = baseColor;

        _state = ClusterManager.State.OFF;
    }

}
