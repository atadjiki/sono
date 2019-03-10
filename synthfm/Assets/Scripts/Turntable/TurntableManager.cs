using System;
using System.Collections;
using System.Collections.Generic;
using MidiJack;
using UnityEngine;
namespace PlayerInput
{
    public class TurntableManager : MonoBehaviour
    {

        public int minOrthSize = 5;
        public int maxOrthSize = 50;
        public int rotation = 5;

        private float fadeAmount;
        private float currentLeft;
        private float currentRight;
        private float currentSlider;

        public MidiJack.MidiChannel sliderChannel;
        private MidiJack.MidiChannel leftTurnChannel;
        private MidiJack.MidiChannel rightTurnChannel;
        private MidiJack.MidiChannel crossFadeChannel;
        private int leftTurnKnob;
        private int rightTurnKnob;
        private int crossFadeKnob;
        public int sliderKnob;

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

        public float getSlider()
        {
            return currentSlider;
        }


        // Start is called before the first frame update
        void Start()
        {

            // LoadPlayerPrefs();
            crossFadeKnob = 7;
            crossFadeChannel = MidiChannel.Ch1;
            leftTurnKnob = 17;
            leftTurnChannel = MidiChannel.Ch2;
            rightTurnKnob = 17;
            rightTurnChannel = MidiChannel.Ch3;

            sliderChannel = MidiChannel.Ch2;
            sliderKnob = 24;

            currentLeft = fetchLeftTurntable();
            currentRight = fetchRightTurntable();
            currentSlider = fetchSlider();
        }

        private MidiChannel GetChannelByString(string v)
        {
            return (MidiChannel)Enum.Parse(typeof(MidiChannel), v);
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

            processSlider();

        }

        public void processFade()
        {

            float crossFade = fetchCrossFade();

        }

        public void processLeftTurntable()
        {
            float leftTurntable = fetchLeftTurntable();
            if (leftTurntable != currentLeft)
            {
                currentLeft = leftTurntable;
            }

        }

        public void processRightTurntable()
        {
            float rightTurntable = fetchRightTurntable();
            if (rightTurntable != currentRight)
            {
                currentRight = rightTurntable;
            }

        }

        public void processSlider()
        {
            float slider = fetchSlider();
            if(slider != currentSlider)
            {
                currentSlider = slider;
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
            return MidiJack.MidiMaster.GetKnob(crossFadeChannel, crossFadeKnob, 0f); 
        }

        public float fetchSlider()
        {
            return MidiJack.MidiMaster.GetKnob(sliderChannel, sliderKnob, 0f); //Slider is channel one, knob 24?
        }

        void LoadPlayerPrefs()
        {
            if (PlayerPrefs.HasKey(GameResources.crossFadeKnob))
            {
                crossFadeKnob = PlayerPrefs.GetInt(GameResources.crossFadeKnob);
            }
            else
            {
                crossFadeKnob = GameResources.defaultCrossFade;
            }
            if (PlayerPrefs.HasKey(GameResources.leftTurnKnob))
            {
                leftTurnKnob = PlayerPrefs.GetInt(GameResources.leftTurnKnob);
            }
            else
            {
                leftTurnKnob = GameResources.defaultLeftTurn;
            }
            if (PlayerPrefs.HasKey(GameResources.rightTurnKnob))
            {
                rightTurnKnob = PlayerPrefs.GetInt(GameResources.rightTurnKnob);
            }
            else
            {
                rightTurnKnob = GameResources.defaultRightTurn;
            }
            if (PlayerPrefs.HasKey(GameResources.crossFadeChnl))
            {
                //crossFadeChannel = GetChannelByString(PlayerPrefs.GetString(GameResources.crossFadeChnl));
                crossFadeChannel = GameResources.defaultFadeChnl;
            }
            else
            {
                crossFadeChannel = GameResources.defaultFadeChnl;
            }
            if (PlayerPrefs.HasKey(GameResources.leftTurnChnl))
            {
                //leftTurnChannel = GetChannelByString(PlayerPrefs.GetString(GameResources.leftTurnChnl));
                leftTurnChannel = GameResources.defaultLeftChnl;
            }
            else
            {
                leftTurnChannel = GameResources.defaultLeftChnl;
            }
            if (PlayerPrefs.HasKey(GameResources.rightTurnChnl))
            {
                //rightTurnChannel = GetChannelByString(PlayerPrefs.GetString(GameResources.rightTurnChnl));
                rightTurnChannel = GameResources.defaultRightChnl;
            }
            else
            {
                rightTurnChannel = GameResources.defaultRightChnl;
            }

        }
    }
}