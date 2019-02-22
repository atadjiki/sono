using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorIt: MonoBehaviour
{
    [Header("Colors for the brick")]
    public Color baseColor;
    public Color errorColor;
    public Color activeColor;

    public int sequenceNo;

    [Header("The State Of this Crystal ON = Active")]
    public PuzzleManager.State _state = PuzzleManager.State.OFF;

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
        _renderer = this.GetComponent<SpriteRenderer>();
        _renderer.color = baseColor;
        currentColor = baseColor;

        transform = this.GetComponent<Transform>();
        initialPosition = transform.localPosition;
        shakeDuration = 0;
    }


    void Update()
    {
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

        _state = PuzzleManager.State.ON;
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
        _state = PuzzleManager.State.Error;
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

        _state = PuzzleManager.State.OFF;
    }

    // Set
    public void setCurrentColor()
    {

    }

    // Set
    public void setBaseColor(Color i_color)
    {
        baseColor = i_color;
    }
}
