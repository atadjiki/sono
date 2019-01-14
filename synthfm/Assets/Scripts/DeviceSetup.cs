using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DeviceSetup : MonoBehaviour
{

    public MidiJack.MidiChannel leftTurnChnl;
    public MidiJack.MidiChannel rightTurnChnl;
    public MidiJack.MidiChannel crossFadeChnl;

    public int leftTurnKnob;
    public int rightTurnKnob;
    public int crossFadeKnob;

    public Text instructionText;
    public Text statusText;
    public Button submitButton;

    public Text leftTurnText;
    public Text rightTurnText;
    public Text crossFadeText;

    public Dropdown itemDropdown;
    public Dropdown knobDropdown;

    private enum SelectedKnob { leftTurntable, rightTurntable, crossFade };
    private SelectedKnob selectedKnob;

    private int[] foundKnobs;
    private int currentKnob;


    // Start is called before the first frame update
    void Start()
    {
        selectedKnob = SelectedKnob.leftTurntable;
        instructionText.text = "Turn knobs you would like to calibrate";
        statusText.text = "Waiting for input...";
        leftTurnText.text = "Left Turntable Knob: " + leftTurnKnob;
        rightTurnText.text = "Right Turntable Knob: " + rightTurnKnob;
        crossFadeText.text = "Crossfade: " + crossFadeKnob;

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void populateKnobList()
    {
        foundKnobs = MidiJack.MidiMaster.GetKnobNumbers();
        Debug.Log("Detecting " + foundKnobs.Length + " knobs");


        knobDropdown.options.Clear();
        List<int> list = new List<int>(foundKnobs);
        foreach (int i in list)
        {
            knobDropdown.options.Add(new Dropdown.OptionData(i.ToString()));
        }

        knobDropdown.RefreshShownValue();
    }

    public void selectKnob()
    {
        currentKnob = knobDropdown.value;
        knobDropdown.RefreshShownValue();
    }

    public void SetCalibrationItem()
    {
        int index = itemDropdown.value;

        if (index == 0)
        {
            selectedKnob = SelectedKnob.leftTurntable;
            instructionText.text = "Left Knob Setup:\n Turn a knob for left input";
            statusText.text = "Waiting for input...";
        }
        else if (index == 1)
        {
            selectedKnob = SelectedKnob.rightTurntable;
            instructionText.text = "Right Knob Setup:\n Turn a knob for right input";
            statusText.text = "Waiting for input...";
        }
        else if (index == 2)
        {
            selectedKnob = SelectedKnob.crossFade;
            instructionText.text = "Fade Setup:\n Turn a knob for fade input";
            statusText.text = "Waiting for input...";
        }
    }

    public void SetKnobToControl()
    {

        populateKnobList();

        if (foundKnobs.Length == 0)
        {
            Debug.Log("No Knobs Detected");
            return;
        }
        else
        {

            int knob = foundKnobs[knobDropdown.value];

            if (selectedKnob == SelectedKnob.leftTurntable)
            {
                leftTurnKnob = knob;
                statusText.text = "Received input:\n Left Turn set to Knob # " + leftTurnKnob;
                leftTurnText.text = "Left Turntable Knob: " + leftTurnKnob;
            }
            else if (selectedKnob == SelectedKnob.rightTurntable)
            {
                rightTurnKnob = knob;
                statusText.text = "Received input:\n Right Turn set to Knob # " + rightTurnKnob;
                rightTurnText.text = "Right Turntable Knob: " + rightTurnKnob;


            }
            else if (selectedKnob == SelectedKnob.crossFade)
            {
                crossFadeKnob = knob;
                statusText.text = "Received input:\n Crossfade set to Knob # " + crossFadeKnob;
                crossFadeText.text = "Crossfade: " + crossFadeKnob;
            }


        }

    }

    public void finalizeSettings()
    {

        //PlayerPrefs.SetInt(Resources.leftTurnKnob, leftTurnKnob);
        //PlayerPrefs.SetInt(Resources.rightTurnKnob, rightTurnKnob);
        //PlayerPrefs.SetInt(Resources.crossFadeKnob, crossFadeKnob);
        //PlayerPrefs.SetString(Resources.leftTurnChnl, leftTurnChnl.ToString());
        //PlayerPrefs.SetString(Resources.rightTurnChnl, rightTurnChnl.ToString());
        //PlayerPrefs.SetString(Resources.crossFadeChnl, crossFadeChnl.ToString());
        //PlayerPrefs.Save();

        SceneManager.LoadScene(Resources.gameScene);
    }

}
