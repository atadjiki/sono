using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This will be attached to Rock

public class RockIt : MonoBehaviour
{
    [Header("The bright rock sprite")]
    public Sprite Sprite_Bright;

    public bool ToDeactivate = false;
    
    // essentials
    private ClusterManager _cMamnager;

    [Header("Speed of Disappearing Rock")]
    public float speed = 0.2f;

    private SpriteRenderer _thisRender;
    private Color _color;
    private float _opacity;

    //shake stuff
    private Transform transform;
    private float shakeDuration;
    public float shakeMagnitude = 0.8f;
    public float dampingSpeed = 1.0f;
    Vector3 initialPosition;

    bool FadeOut = false;
    bool FadeIn = false;

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


        // minor
        _thisRender = this.GetComponent<SpriteRenderer>();
      //  _color = _thisRender.color;
        _opacity = _thisRender.color.a;
        _color = _thisRender.color;
        transform = this.GetComponent<Transform>();
        initialPosition = transform.localPosition;
        shakeDuration = 0;
    }

    // do nothing
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (!ToDeactivate) // notify if not deactivated
            {
                // Rocks do not harm crystalls anymore
               // _cMamnager._NotifyFromROck(this);
            }
        }
    }

    public void ActivateIt()
    {
        /* New Stuff
         * 1- Fade out
         * 2- Change sprite
         * 3- Fade In
        */
        FadeOut = true;
        
    }

    // Update is called once per frame
    void Update()
    {
        HandleActivation();

        // OLD
        if (ToDeactivate)
        {
            
               shakeDuration = 2;
                _opacity -= (speed * Time.deltaTime);
                _thisRender.color = new Color(_color.r, _color.g, _color.b, _opacity);

            //shaky shaky and destroy
            transform.localPosition = initialPosition + Random.insideUnitSphere * shakeMagnitude;
            shakeDuration -= Time.deltaTime * dampingSpeed;

            if (_opacity < 0)
            {
                ToDeactivate = false;
                Destroy(this.gameObject);  // destroy this object ??? May be yes
            }

        }

    }

    private void HandleActivation()
    {
        if (FadeOut)
        {
            _opacity -= (speed * Time.deltaTime);
            _thisRender.color = new Color(_color.r, _color.g, _color.b, _opacity);
            if (_opacity <= 0.1)
            {
                _thisRender.sprite = Sprite_Bright;
                FadeOut = false;
                FadeIn = true;
            }
        }
        if (FadeIn)
        {
            _opacity += (speed * Time.deltaTime);
            _thisRender.color = new Color(_color.r, _color.g, _color.b, _opacity);
            if (_opacity == 1)
            {
                FadeIn = false;
            }
        }
    }
}
