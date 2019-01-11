using UnityEngine;
using System.Collections;

public class ColumnController : MonoBehaviour {

    private CsoundUnity csoundUnity;
    public int frequency = 0;
    int columnNumber;

    void Awake()
    {
        csoundUnity = GetComponent<CsoundUnity>();
    }

	// Use this for initialization
	void Start ()
    {
        //programmatically assign a frequncy to each colum gameObject based on name
        columnNumber = int.Parse(gameObject.name.Substring(gameObject.name.IndexOf("(") + 1, gameObject.name.IndexOf(")") - 1 - gameObject.name.IndexOf("(")));
        csoundUnity.setChannel("freq", 100*columnNumber);
        //now programmatically set the colour of each column
        GetComponent<Renderer>().material.color = new Color(0f, 1f / columnNumber, 0f);
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void setAmplitude(float amp)
    {
        //scale amplitude of successively higher partials
        csoundUnity.setChannel("amp", amp*(1f/(columnNumber*.33f)));
    }
}
