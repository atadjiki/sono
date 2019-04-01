using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorIt : MonoBehaviour
{
    [Header("Colors for the Crystalls")]
    public Color baseColor;
    public Color activeColor;

    [Header("Is the Crystal interactable ?")]
    public bool ToColor;

    [Header("To Apply shake effect while activating")]
    public bool toShake;

    [Header("Shake crtiteria if Shakable")]
    public float shakeDuration;
    public float shakeMagnitude = 0.2f;
    public float dampingSpeed = 5.0f;
    public float speed = 0f;

    private SpriteRenderer _renderer;
    private Color currentColor;

    // ROtation stuff
    private bool ForwardZ = false;
    private bool ReverseZ = false;
    private Vector3 initialPosition;
    private float shakeDur;
    private Transform transform;

    // Start is called before the first frame update
    void Start()
    {
        _renderer = this.GetComponent<SpriteRenderer>();
        _renderer.color = baseColor;

        transform = this.GetComponent<Transform>();
        initialPosition = transform.localPosition;
    }

    private void Update()
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

        if (toShake)
        {
            // shaky shaky
            if (shakeDur > 0)
            {
                transform.localPosition = initialPosition + Random.insideUnitSphere * shakeMagnitude;
                shakeDur -= Time.deltaTime * dampingSpeed;

                transform = this.GetComponent<Transform>();
                initialPosition = transform.localPosition;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (ToColor)
        {
            _renderer.color = activeColor;
        }

        shakeDur = shakeDuration;

        int x = Random.Range(0, 3);
        if (x == 0)
        {
            ReverseZ = true;
        }
        else
        {
            ForwardZ = true;
        }
        speed = Random.Range(10, 15);

        StartCoroutine(stopShake());
    }

    IEnumerator stopShake()
    {
        yield return new WaitForSeconds(shakeDuration);
       
        speed = 0;
    }
}
