using System.Collections;
using System.Collections.Generic;
using MidiJack;

public struct TurntableProfile 
{
    public int crossFadeKnob;
    public MidiChannel crossFadeChannel;

    public int leftTurnKnob;
    public MidiChannel leftTurnChannel;

    public int rightTurnKnob;
    public MidiChannel rightTurnChannel;

    public int sliderKnob;
    public MidiChannel sliderChannel;

    public static TurntableProfile NumarkTurntable()
    {
        TurntableProfile result = new TurntableProfile();

        result.crossFadeKnob = 7;
        result.crossFadeChannel = MidiChannel.Ch1;
        result.leftTurnKnob = 17;
        result.leftTurnChannel = MidiChannel.Ch2;
        result.rightTurnKnob = 17;
        result.rightTurnChannel = MidiChannel.Ch3;

        result.sliderKnob = 24;
        result.sliderChannel = MidiChannel.Ch2;
        
        return result;
    }

    public static  TurntableProfile DJTech()
    {
        TurntableProfile result = new TurntableProfile();

        result.crossFadeKnob = 7;
        result.crossFadeChannel = MidiChannel.Ch11;
        result.leftTurnKnob = 53;
        result.leftTurnChannel = MidiChannel.Ch1;
        result.rightTurnKnob = 53;
        result.rightTurnChannel = MidiChannel.Ch1;
        result.sliderKnob = 0;
        result.sliderChannel = MidiChannel.Ch4;

        return result;
    }
}


