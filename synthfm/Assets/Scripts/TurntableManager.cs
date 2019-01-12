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
    public Text leftText;
    public Text rightText;
    public Text fadeText;
    public int rotation = 5;

    private float fadeAmount;
    private float currentLeft;
    private float currentRight;
    private int frames = 180;
    private int currentFrames = 0;

    public float getLeft()
    {
        return currentLeft;
    }

    public float getRight()
    {
        return currentRight;
    }

    public float getFade()
    {
        return fadeAmount;
    }


    // Start is called before the first frame update
    void Start()
    {
        csoundUnity = Camera.main.GetComponent<CsoundUnity>();
        crossFadeBar.value = 0.5f;
        currentLeft = fetchLeftTurntable();
        currentRight = fetchRightTurntable();

        fadeText.text = "";
        leftText.text = "";
        rightText.text = "";
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
        
        float crossFade = fetchCrossFade();
        if(crossFade != crossFadeBar.value)
        {
            fadeText.text = "Crossfade - 17 - ch1 - " + crossFade + "\n";
            Debug.Log("Crossfade - 17 - ch1 - " + crossFade);
            crossFadeBar.value = crossFade;
        } 

    }

    public void processLeftTurntable()
    {
        float leftTurntable = fetchLeftTurntable();
        if(leftTurntable != currentLeft)
        {
            leftText.text = "Left Turntable - 17 - ch2 - " + leftTurntable + "\n";
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
        float rightTurntable = fetchRightTurntable();
        if(rightTurntable != currentRight)
        {
            rightText.text = "Right Turntable - 17 - ch3 - " + rightTurntable + "\n";
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

    public float fetchLeftTurntable()
    {
        return MidiJack.MidiMaster.GetKnob(MidiJack.MidiChannel.Ch2, 17, 0f); //Left is channel two, knob 17
    }

    public float fetchRightTurntable()
    {
        return MidiJack.MidiMaster.GetKnob(MidiJack.MidiChannel.Ch3, 17, 0f); //Left is channel two, knob 17
    }

    public float fetchCrossFade()
    {
        return MidiJack.MidiMaster.GetKnob(MidiJack.MidiChannel.Ch1, 7, 0f); //Left is channel two, knob 17
    }

    public void playTest()
    {
        csoundUnity.sendScoreEvent("i1 0 1 1 200");


    }
}
