using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// attached to every Crystall

public class Crystal: MonoBehaviour
{
    [Header("Colors for the Crystalls")]
    public Sprite Sprite_Bright;
    public Sprite Sprite_Dull;
    private Sprite Sprite_InHand;
    
    [Header("The State Of this Crystal ON = Active")]
    public ClusterManager.State _state = ClusterManager.State.OFF;

    [Header("shake and Rotate")]
    public bool ToShakeRotate;

    [Header("Fading speed")]
    public float fadeSpeed;

    [Header("Rotation Speed")]
    public float RotationSpeed = 0f;

    private bool ForwardZ = false;
    private bool ReverseZ = false;

    // Shake Stuff
    private Transform _transform;
    private float shakeDuration;
    public float shakeMagnitude = 0.2f;
    public float dampingSpeed = 5.0f;
    Vector3 initialPosition;

    private SpriteRenderer _thisRender;
    private Color _color;
    private float _opacity;
    private bool ToFadeOut;
    private bool ToFadeIn;

    // essentials
    private ClusterManager _cMamnager;
    public bool IsPuzzleComplete = false;

    // OLD Stuff below from here
    [Header("Colors for the Crystalls")]
    private Color baseColor;
    private Color errorColor;
    private Color activeColor;

    [Header(" The Sequence number for Seq mode")]
    public int sequenceNo;

    [Header("Error State Timing in Seconds")]
    public float _errorTime = 1f;

    private void Awake()
    {
        if (_transform == null)
        {
            _transform = GetComponent(typeof(Transform)) as Transform;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        InitStuff();
    }

    void InitStuff()
    {
        // get
        _cMamnager = _transform.parent.gameObject.GetComponent<ClusterManager>();

        _thisRender = this.GetComponent<SpriteRenderer>();
        Sprite_Dull = _thisRender.sprite;
        Sprite_InHand = Sprite_Bright;
        _opacity = _thisRender.color.a;
        _color = _thisRender.color;

        _transform = this.GetComponent<Transform>();
        initialPosition = _transform.localPosition;
        shakeDuration = 0;
        fadeSpeed = 0.5f;
    }

    void Update()
    {
        // ROtation
            if (ForwardZ == true)
            {
                _transform.Rotate(0, 0, Time.deltaTime * RotationSpeed, Space.Self);
            }
            if (ReverseZ == true)
            {
                _transform.Rotate(0, 0, -Time.deltaTime * RotationSpeed, Space.Self);
            }

        // shaky shaky
        if (shakeDuration > 0)
        {
            _transform.localPosition = initialPosition + Random.insideUnitSphere * shakeMagnitude;
            shakeDuration -= Time.deltaTime * dampingSpeed;
        }

        if (ToFadeOut)
        {
            _opacity -= (fadeSpeed * Time.deltaTime);
            _thisRender.color = new Color(_color.r, _color.g, _color.b, _opacity);
            if (_opacity <= 0.2)
            {
                _thisRender.sprite = Sprite_InHand;
                ToFadeOut = false;
                ToFadeIn = true;
            }
        }

        if (ToFadeIn)
        {
            _opacity += (fadeSpeed * Time.deltaTime);
            _thisRender.color = new Color(_color.r, _color.g, _color.b, _opacity);
            if (_opacity == 1)
            {
                ToFadeIn = false;
                
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if ((_state == ClusterManager.State.OFF)) // notify if this OFF
            {
                _state = ClusterManager.State.ON;
                ActivateIt();
                if (!IsPuzzleComplete)
                {
                    _cMamnager._Notify(true);
                }
            }
            else if (_state == ClusterManager.State.ON)
            {
                _state = ClusterManager.State.OFF;
                DeactivateIt();
                
                if (!IsPuzzleComplete)
                {
                    _cMamnager._Notify(false);
                }
            }

            // shake and Rotation
            if(ToShakeRotate)
            {
                ShakeandRotate();
            }
        }
    }


    public void ShakeandRotate()
    {
        // shaky shaky
        shakeDuration = 0.5f;

        int x = Random.Range(0, 3);
        if(x == 0)
        {
            ReverseZ = true;
        }
        else{
            ForwardZ = true;
        }
        RotationSpeed = Random.Range(10,15);
    }

    private void ActivateIt()
    {
        // fade out, change sprite, fade in
        Sprite_InHand = Sprite_Bright;
        ToFadeOut = true;
    }

    private void DeactivateIt()
    {
        // similar as activate but diff sprite
        Sprite_InHand = Sprite_Dull;
        ToFadeOut = true;
    }


// Error stuff not using anymore
    public void changeToFail()
    {
        _state = ClusterManager.State.Error;
        _thisRender.color = errorColor;

        StartCoroutine(setBaseColor());

        ForwardZ = false;
        ReverseZ = false;
        RotationSpeed = 0;
   
    }
   
    IEnumerator setBaseColor()
    {
        yield return new WaitForSeconds(_errorTime);
        _thisRender.color = baseColor;

        _state = ClusterManager.State.OFF;
    }

}
