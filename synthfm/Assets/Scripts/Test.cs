using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{

    private CsoundUnity csoundUnity;

    // Start is called before the first frame update
    void Start()
    {
        csoundUnity = Camera.main.GetComponent<CsoundUnity>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playTest()
    {
        csoundUnity.sendScoreEvent("i1 0 1 1 200");
    }
}
