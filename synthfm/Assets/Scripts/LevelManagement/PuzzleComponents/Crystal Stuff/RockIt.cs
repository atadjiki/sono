using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This will be attached to Rock

public class RockIt : MonoBehaviour
{
    [Header("Speed of Disappearing Rock")]
    public float speed = 0.2f;
    public PuzzleManager _Manager;

    private SpriteRenderer _thisRender;
    private Color _color;
    public bool ToDeactivate = false;
    private float _opacity;

    //shake stuff
    private Transform transform;
    private float shakeDuration;
    public float shakeMagnitude = 0.8f;
    public float dampingSpeed = 1.0f;
    Vector3 initialPosition;

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
        _thisRender = this.GetComponent<SpriteRenderer>();
        _color = _thisRender.color;
        _opacity = _color.a;

        transform = this.GetComponent<Transform>();
        initialPosition = transform.localPosition;
        shakeDuration = 0;
    }


    public void DestroyIt()
    {
        // lower down opacity and disable/ destroy eventually

        ToDeactivate = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (ToDeactivate)
        {
            Debug.Log("Here");
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
}
