using UnityEngine;

public class SphereController : MonoBehaviour {

    //CsoundUnity component
    private CsoundUnity csoundUnity;
    float newScale;
    float ratio;

    void Awake()
    {
        //assign member variable
        csoundUnity = GetComponent<CsoundUnity>();
    }

    // Use this for initialization
    void Start ()
    {
        //set a unique random colour and frequency for each sonic sphere
        ratio = Time.deltaTime;
        InvokeRepeating("ChangeScale", 1f, 1f);
        GetComponent<Renderer>().material.color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        csoundUnity.setChannel("amp", .2f);
        csoundUnity.setChannel("freq", Random.Range(100, 1000));
    }


    void Update()
    {
        //randomly resize sphere to give some motion
        float newVal = Mathf.Lerp(transform.localScale.y, newScale, ratio);
        transform.localScale = new Vector3(newVal, newVal, newVal);
    }

    void ChangeScale()
    {
        newScale = Random.Range(1f, 1.5f);
    }
}
