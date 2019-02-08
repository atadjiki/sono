using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.VFX;

public class Test : MonoBehaviour
{
    public VisualEffect VE;
    public Texture3D tex;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            VE.SetTexture("Vectorfields", tex);
        }
    }
}
 