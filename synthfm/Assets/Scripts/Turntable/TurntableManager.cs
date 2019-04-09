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

        private float messageCount = 0;
        public bool messageReceived = false;

        public enum Turntable { Numark, DJTech };
        public Turntable model = Turntable.DJTech;
        private TurntableProfile profile;

        public enum DJTechControl { Wheel, Cue, Play, Slider, Knob, KnobPress, None };
        public DJTechControl lastInteracted;

        private int ignoreNextNMessages = 4;
        private int currentIgnored = 0;

        private string lastMessage = "";

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

            if(model == Turntable.DJTech)
            {
                profile = TurntableProfile.DJTech();
            }
            else if(model == Turntable.Numark)
            {
                profile = TurntableProfile.NumarkTurntable();
            }

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

            if (MidiJack.MidiDriver.Instance.TotalMessageCount > messageCount)
            { 
                messageReceived = true;
            }
            else
            {
                messageReceived = false;
            }

            processLeftTurntable();
            processRightTurntable();
            processFade();
            processSlider();

            messageCount = MidiJack.MidiDriver.Instance.TotalMessageCount;

        }

        public void UpdateLastInteracted()
        {
            if (MidiJack.MidiDriver.Instance.TotalMessageCount <= 0)
            {
                return;
            }

            string tmp = MidiJack.MidiDriver.Instance.History.Peek().ToString();

            if (tmp == lastMessage)
            {
             //   Debug.Log("Disregarding message");
                return;
            }
            

            lastMessage = tmp;


            Debug.Log(lastMessage);
            if (lastMessage.Contains("d(" + profile.leftTurnKnob.ToString("X")) && lastMessage.Contains("s(B0)"))
            {
                lastInteracted = DJTechControl.Wheel;
            } else if (lastMessage.Contains("d(" + profile.rightTurnKnob.ToString("X")) && lastMessage.Contains("s(B0)"))
            {
                lastInteracted = DJTechControl.Wheel;
            }
            else if (lastMessage.Contains("s(E0)"))
            {
                lastInteracted = DJTechControl.Slider;
            }
            else if (lastMessage.Contains("s(B0) d(38"))
            {
                lastInteracted = DJTechControl.Knob;
            }
            else if (lastMessage.Contains("s(90) d(2A,7F)"))
            {
                if(currentIgnored >= ignoreNextNMessages)
                {
                    lastInteracted = DJTechControl.Play;
                    currentIgnored = 0;
                }
                else
                {
                    currentIgnored++;
                }
                
            }
            else if (lastMessage.Contains("s(90) d(2B,7F)"))
            {
                if (currentIgnored >= ignoreNextNMessages)
                {
                    lastInteracted = DJTechControl.Cue;
                    currentIgnored = 0;
                }
                else
                {
                    currentIgnored++;
                }
            }
            else if(lastMessage.Contains("s(90) d(1F,7F)"))
            {
                
                if (currentIgnored >= ignoreNextNMessages)
                {
                    lastInteracted = DJTechControl.KnobPress;
                    currentIgnored = 0;
                }
                else
                {
                    currentIgnored++;
                }
            }
            else
            {
                lastInteracted = DJTechControl.None;
            }
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

            return MidiJack.MidiMaster.GetKnob(profile.leftTurnChannel, profile.leftTurnKnob, 0f); //Left is channel two, knob 17
        }

        public float fetchRightTurntable()
        {
            return MidiJack.MidiMaster.GetKnob(profile.rightTurnChannel, profile.rightTurnKnob, 0f); //Left is channel two, knob 17
        }

        public float fetchCrossFade()
        {
            return MidiJack.MidiMaster.GetKnob(profile.crossFadeChannel, profile.crossFadeKnob, 0f); 
        }

        public float fetchSlider()
        {
            // return MidiJack.MidiMaster.GetKnob(profile.sliderChannel, profile.sliderKnob, 0f); //Slider is channel one, knob 24?

            float result = 0;

            if (MidiJack.MidiDriver.Instance.TotalMessageCount <= 0)
            {
                return -1;
            }

            string lastMessage = MidiJack.MidiDriver.Instance.History.Peek().ToString();

            if (lastMessage.Contains("s(E0)"))
            {
                int index = lastMessage.IndexOf(',');
                index++;
               
                lastMessage = lastMessage.Substring(index, 2);
                
                if(float.TryParse(lastMessage, out result))
                {
                    result = (result - 0) / (77 - 0);
                }

            }
           
            return result; 
        }
    }
}