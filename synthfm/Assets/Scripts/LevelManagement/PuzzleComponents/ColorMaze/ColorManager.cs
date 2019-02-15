using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorManager : MonoBehaviour
{
  
    [Header("Colors for the brick")]
    public Color baseColor;
    public Color errorColor;
    public Color activeColor;

    public int sequenceNo;

    private SpriteRenderer _renderer;
    private Color currentColor;
    // add a couroutine to change from error to default


    // Start is called before the first frame update
    void Start()
    {
        _renderer = this.GetComponent<SpriteRenderer>();
        _renderer.color = baseColor;
        
    }

    public void changeToActive()
    {
        _renderer.color = activeColor;
    }

    public void changeToFail()
    {
        _renderer.color = errorColor;

        StartCoroutine(setBaseColor());
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
