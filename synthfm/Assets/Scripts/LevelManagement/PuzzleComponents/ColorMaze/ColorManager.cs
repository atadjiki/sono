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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (LevelManager.instance.getPlayer() == collision.gameObject)
        {
           
            // check puzzle state and update the color if true sequence
            // if(this.sequenceNo == puzle.getCurrentSequenceNo(); )
                _renderer.color = activeColor;
            // else error color

                // Start coroutine to change back to normal color

        }
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
