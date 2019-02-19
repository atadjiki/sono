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

    // Start is called before the first frame update
    void Start()
    {
        _thisRender = this.GetComponent<SpriteRenderer>();
        _color = _thisRender.color;
        _opacity = _color.a;
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
         
                _opacity -= (speed * Time.deltaTime);
                _thisRender.color = new Color(_color.r, _color.g, _color.b, _opacity);


            if(_opacity<0)
                ToDeactivate = false;
            // destroy this object ??? May be yes
        }
    }
}
