using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameResources : MonoBehaviour
{
    public static string leftTurnKnob = "LeftTurnKnob";
    public static string rightTurnKnob = "RightTurnKnob";
    public static string crossFadeKnob = "CrossfadeKnob";

    public static string leftTurnChnl = "LeftTurnChnl";
    public static string rightTurnChnl = "RightTurnChnl";
    public static string crossFadeChnl = "CrossFadeChnl";

    public static int defaultLeftTurn = 17;
    public static int defaultRightTurn = 17;
    public static int defaultCrossFade = 7;

    public static MidiJack.MidiChannel defaultFadeChnl = MidiJack.MidiChannel.Ch1;
    public static MidiJack.MidiChannel defaultLeftChnl = MidiJack.MidiChannel.Ch2;
    public static MidiJack.MidiChannel defaultRightChnl = MidiJack.MidiChannel.Ch3;

    public static string deviceSetupScene = "DeviceSetup";
    public static string gameScene = "Game";



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
