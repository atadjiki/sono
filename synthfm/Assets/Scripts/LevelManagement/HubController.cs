using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HubController : MonoBehaviour
{

    private float z_rot = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DoRotate();
    }
    
    public void DoRotate()
    {

        this.transform.Rotate(0, 0, z_rot);

    }

}
