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
    
    private SpriteRenderer _renderer;
    private Color currentColor;

    [Header("Rotation Speed")]
    public float speed = 0f;

    [Header("Direction")]
    public bool ForwardZ = false;
    public bool ReverseZ = false;
  
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

    }
    

    // Start is called before the first frame update
    void Start()
    {
        _renderer = this.GetComponent<SpriteRenderer>();
        _renderer.color = baseColor;
        currentColor = baseColor;
    }
    

    public void changeToActive()
    {
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
        _renderer.color = errorColor;

        StartCoroutine(setBaseColor());

        ForwardZ = false;
        ReverseZ = false;
        speed = 0;
    }
   
    IEnumerator setBaseColor()
    {
        yield return new WaitForSeconds(1);
        _renderer.color = baseColor;
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
