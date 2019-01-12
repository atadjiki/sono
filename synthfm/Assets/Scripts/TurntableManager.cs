using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurntableManager : MonoBehaviour
{

    private CsoundUnity csoundUnity;
    public GameObject leftImage;
    public GameObject rightImage;
    public Slider crossFadeBar;
    public int rotation = 5;

    private float currentLeft;
    private float currentRight;

    // Start is called before the first frame update
    void Start()
    {
        csoundUnity = Camera.main.GetComponent<CsoundUnity>();
        crossFadeBar.value = 0.5f;
        currentLeft = getLeftTurntable();
        currentRight = getRightTurntable();
    }

    // Update is called once per frame
    void Update()
    {
        //query the left turntable
        processLeftTurntable();

        //right turntable
        processRightTurntable();

        //crossfade 
        processFade();

    }

    public void processFade()
    {
        
        float crossFade = getCrossFade();
        if(crossFade != crossFadeBar.value)
        {
            Debug.Log("Crossfade - 17 - ch1 - " + crossFade);
            crossFadeBar.value = crossFade;
        }

    }

    public void processLeftTurntable()
    {
        float leftTurntable = getLeftTurntable();
        if(leftTurntable != currentLeft)
        {
            Debug.Log("Left Turntable - 17 - ch2 - " + leftTurntable);
            if (leftTurntable < 0.5f)
            {
                leftImage.transform.Rotate(new Vector3(0, 0, -rotation));
            }
            else if (leftTurntable > 0.5f)
            {
                leftImage.transform.Rotate(new Vector3(0, 0, rotation));
            }
            currentLeft = leftTurntable;
        }

    }

    public void processRightTurntable()
    {
        float rightTurntable = getRightTurntable();
        if(rightTurntable != currentRight)
        {
            Debug.Log("Right Turntable - 17 - ch3 - " + rightTurntable);
            if (rightTurntable < 0.5f)
            {
                rightImage.transform.Rotate(new Vector3(0, 0, -rotation));
            }
            else if (rightTurntable > 0.5f)
            {
                rightImage.transform.Rotate(new Vector3(0, 0, rotation));
            }
            currentRight = rightTurntable;
        }
       
    }

    public float getLeftTurntable()
    {
        return MidiJack.MidiMaster.GetKnob(MidiJack.MidiChannel.Ch2, 17, 0f); //Left is channel two, knob 17
    }

    public float getRightTurntable()
    {
        return MidiJack.MidiMaster.GetKnob(MidiJack.MidiChannel.Ch3, 17, 0f); //Left is channel two, knob 17
    }

    public float getCrossFade()
    {
        return MidiJack.MidiMaster.GetKnob(MidiJack.MidiChannel.Ch1, 7, 0f); //Left is channel two, knob 17
    }

    public void playTest()
    {
        csoundUnity.sendScoreEvent("i1 0 1 1 200");


    }
}
