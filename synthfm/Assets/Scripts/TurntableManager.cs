using System;
using System.Collections;
using System.Collections.Generic;
using MidiJack;
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

    private MidiJack.MidiChannel leftTurnChannel;
    private MidiJack.MidiChannel rightTurnChannel;
    private MidiJack.MidiChannel crossFadeChannel;
    private int leftTurnKnob;
    private int rightTurnKnob;
    private int crossFadeKnob;
 
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

        LoadPlayerPrefs();

        csoundUnity = Camera.main.GetComponent<CsoundUnity>();
        crossFadeBar.value = 0.5f;
        currentLeft = fetchLeftTurntable();
        currentRight = fetchRightTurntable();

        fadeText.text = "";
        leftText.text = "";
        rightText.text = "";
    }

    private MidiChannel GetChannelByString(string v)
    {
        return (MidiChannel) Enum.Parse(typeof(MidiChannel), v);
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
            fadeText.text = "Crossfade - " + crossFadeKnob + " - " + crossFadeChannel.ToString() + " - " + crossFade + "\n";
            Debug.Log("Crossfade - " + crossFadeKnob + " - " + crossFadeChannel.ToString() + " - " + crossFade);
            crossFadeBar.value = crossFade;
        } 

    }

    public void processLeftTurntable()
    {
        float leftTurntable = fetchLeftTurntable();
        if(leftTurntable != currentLeft)
        {
            leftText.text = "Left Turntable - " + leftTurnKnob + " - " + leftTurnChannel.ToString() + " - " + leftTurntable + "\n";
            Debug.Log("Left Turntable - " + leftTurnKnob + " - " + leftTurnChannel.ToString() + " - " + leftTurntable);
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
            rightText.text = "Right Turntable - " + rightTurnKnob + " - " +rightTurnChannel.ToString() + " - " + rightTurntable + "\n";
            Debug.Log("Right Turntable - " + rightTurnKnob + " - " + rightTurnChannel.ToString() + " - " + rightTurntable);
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
        return MidiJack.MidiMaster.GetKnob(leftTurnChannel, leftTurnKnob, 0f); //Left is channel two, knob 17
    }

    public float fetchRightTurntable()
    {
        return MidiJack.MidiMaster.GetKnob(rightTurnChannel, rightTurnKnob, 0f); //Left is channel two, knob 17
    }

    public float fetchCrossFade()
    {
        return MidiJack.MidiMaster.GetKnob(crossFadeChannel, crossFadeKnob, 0f); //Left is channel two, knob 17
    }

    public void playTest()
    {
        csoundUnity.sendScoreEvent("i1 0 1 1 200");


    }

    void LoadPlayerPrefs()
    {
        if (PlayerPrefs.HasKey(Resources.crossFadeKnob))
        {
            crossFadeKnob = PlayerPrefs.GetInt(Resources.crossFadeKnob);
        }
        else
        {
            crossFadeKnob = Resources.defaultCrossFade;
        }
        if (PlayerPrefs.HasKey(Resources.leftTurnKnob))
        {
            leftTurnKnob = PlayerPrefs.GetInt(Resources.leftTurnKnob);
        }
        else
        {
            leftTurnKnob = Resources.defaultLeftTurn;
        }
        if (PlayerPrefs.HasKey(Resources.rightTurnKnob))
        {
            rightTurnKnob = PlayerPrefs.GetInt(Resources.rightTurnKnob);
        }
        else
        {
            rightTurnKnob = Resources.defaultRightTurn;
        }
        if (PlayerPrefs.HasKey(Resources.crossFadeChnl))
        {
            //crossFadeChannel = GetChannelByString(PlayerPrefs.GetString(Resources.crossFadeChnl));
            crossFadeChannel = Resources.defaultFadeChnl;
        }
        else
        {
            crossFadeChannel = Resources.defaultFadeChnl;
        }
        if (PlayerPrefs.HasKey(Resources.leftTurnChnl))
        {
            //leftTurnChannel = GetChannelByString(PlayerPrefs.GetString(Resources.leftTurnChnl));
            leftTurnChannel = Resources.defaultLeftChnl;
        }
        else
        {
            leftTurnChannel = Resources.defaultLeftChnl;
        }
        if (PlayerPrefs.HasKey(Resources.rightTurnChnl))
        {
            //rightTurnChannel = GetChannelByString(PlayerPrefs.GetString(Resources.rightTurnChnl));
            rightTurnChannel = Resources.defaultRightChnl;
        }
        else
        {
            rightTurnChannel = Resources.defaultRightChnl;
        }

    }
}
