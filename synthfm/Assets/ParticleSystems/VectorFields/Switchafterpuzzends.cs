using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.VFX;
public class Switchafterpuzzends : MonoBehaviour
{
    public VisualEffect VE;
    public Texture3D tex;
    public float Complete;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {

            print("Nice");
            VE.SetFloat("SwitchInt", Complete);
        }
    }
}