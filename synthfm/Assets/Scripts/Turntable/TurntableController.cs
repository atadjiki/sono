namespace PlayerInput
{

    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using InControl;

    public class TurntableController : MonoBehaviour
    {

        [Header("Controls")]
        public static KeyCode left_1 = KeyCode.A;
        public static KeyCode left_2 = KeyCode.D;
        public static KeyCode right_1 = KeyCode.LeftArrow;
        public static KeyCode right_2 = KeyCode.RightArrow;
        public static KeyCode speed_up = KeyCode.W;
        public static KeyCode slow_down = KeyCode.S;
        public static KeyCode slow = KeyCode.Alpha1;
        public static KeyCode normal = KeyCode.Alpha2;
        public static KeyCode fast = KeyCode.Alpha3;

        public static string accelerationKey = "Acceleration";
        public static string torqueKey = "Torque";

        private float leftTurntable;
        private float rightTurntable;
        private float crossFade;
        private float rotation;

        private float previousLeft;
        private float previousRight;
        private float previousFade;
        private int currentFrames = 0;
        private int maxFrames = 60;
        private float torqueCount = 1;

        [Header("Player Physics")]
        public float acceleration = 5000;
        [SerializeField]
        private float accel_mod;
        private float accel_incr = 0.05f;
        public float accel_clamp = 1.5f;
        public float accel_floor = 0.25f;

        public float accel_slow = 0.25f;
        public float accel_normal = 0.75f;
        public float accel_fast = 1.5f;

        public float torque_slow = 25f;
        public float torque_normal = 25f;
        public float torque_fast = 25f;

        private bool fast_override = false;
        private bool slow_override = false;

        public float torqueIncrement = 1;
        private bool multiplyTorque = false;

        public enum Speed { Slow, Normal, Fast };
        public Speed currentSpeed;
        public enum ControlType { Keyboard, Joystick, Turntable };
        public ControlType controls = ControlType.Keyboard;

        private new Rigidbody2D rigidbody;


        [Header("References")]
        public TurntableManager turntableManager;
        public Transform[] fragmentSlots;
        public bool[] slotsFilled;


        // Use this for initialization
        void Start()
        {

            rotation = turntableManager.rotation;

            rigidbody = GetComponent<Rigidbody2D>();
            ChangeSpeed(Speed.Normal);

        }

        // Update is called once per frame
        void Update()
        {

            UpdateVariables();
            ApplyForce();

            if (controls == ControlType.Joystick)
                DoJoyStickInput();
            if (controls == ControlType.Keyboard)
                DoAltInput();
            if (controls == ControlType.Turntable)
                DoMIDIInput();

            DoSpeedInput();
            DoCheckForOverrides();

        }

        void UpdateVariables()
        {

            leftTurntable = turntableManager.getLeft();
            rightTurntable = turntableManager.getRight();
            crossFade = turntableManager.getFade();
            //Debug.Log("Update Variables: Left" + leftTurntable + ", Right: " + +rightTurntable + ", Fade: " + crossFade);
        }

        void ApplyForce()
        {
            accel_mod = getSpeed();
            rigidbody.AddForce(transform.up * accel_mod * acceleration * Time.deltaTime);
        }

        Speed ChangeSpeed(Speed speed)
        {
            if (speed == Speed.Slow)
            {
                currentSpeed = Speed.Slow;

            }
            else if (speed == Speed.Normal)
            {
                currentSpeed = Speed.Normal;
            }
            else if (speed == Speed.Fast)
            {
                currentSpeed = Speed.Fast;
            }
            Debug.Log("Speed changed to " + currentSpeed.ToString());
            return currentSpeed;
        }

        public float getSpeed()
        {

            if (fast_override)
            {
                return accel_fast;
            }
            if (slow_override)
            {
                return accel_slow;
            }

            if (currentSpeed == Speed.Slow)
            {
                return accel_slow;
            }
            else if (currentSpeed == Speed.Normal)
            {
                return accel_normal;
            }
            else
            {
                return accel_fast;
            }
        }

        void DoSpeedInput()
        {
            if (controls == ControlType.Keyboard)
            {
                if (Input.GetKeyDown(slow))
                {
                    ChangeSpeed(Speed.Slow);
                }
                else if (Input.GetKeyDown(normal))
                {
                    ChangeSpeed(Speed.Normal);
                }
                else if (Input.GetKeyDown(fast))
                {
                    ChangeSpeed(Speed.Fast);
                }
            }

        }

        void DoCheckForOverrides()
        {

            if (controls == ControlType.Keyboard)
            {
                if (Input.GetKey(speed_up))
                {
                    fast_override = true;
                }
                else if (Input.GetKeyUp(speed_up))
                {
                    Debug.Log("Speed back to " + currentSpeed.ToString());
                    ChangeSpeed(Speed.Normal);
                    fast_override = false;
                }

                if (Input.GetKey(slow_down))
                {
                    slow_override = true;
                }
                else if (Input.GetKeyUp(slow_down))
                {
                    Debug.Log("Speed back to " + currentSpeed.ToString());
                    ChangeSpeed(Speed.Normal);
                    slow_override = false;
                }
            }
            else if (controls == ControlType.Joystick)
            {
                if (Input.GetAxis("Speed_Up") > 0)
                {
                    fast_override = true;
                }
                else if (Input.GetAxis("Speed_Up") <= 0)
                {
                    Debug.Log("Speed back to " + currentSpeed.ToString());
                    ChangeSpeed(Speed.Normal);
                    fast_override = false;
                }

                if (Input.GetAxis("Slow_Down") > 0)
                {
                    slow_override = true;

                }
                else if (Input.GetAxis("Slow_Down") <= 0)
                {
                    Debug.Log("Speed back to " + currentSpeed.ToString());
                    ChangeSpeed(Speed.Normal);
                    slow_override = false;
                }
            }

        }

        void DoJoyStickInput()
        {
            try
            {
                // float leftStickX = XCI.GetAxis(XboxAxis.LeftStickX);
                //float rightStickX = XCI.GetAxis(XboxAxis.RightStickX);
                float leftStickX = 0;
                float rightStickX = 0;

                if (leftStickX != 0)
                {
                    if (leftStickX < 0)
                    {
                        leftTurntable = -1f;
                    }
                    else if (leftStickX > 0)
                    {
                        leftTurntable = 1f;
                    }
                    else
                    {
                        leftTurntable = 0;
                    }

                    accel_mod += leftTurntable;
                    previousLeft = leftTurntable;
                }

                if (rightStickX != 0)
                {

                    if (rightStickX < 0)
                    {
                        rightTurntable = 1f;
                    }
                    else if (rightStickX > 0)
                    {
                        rightTurntable = -1f;
                    }
                    else
                    {
                        rightTurntable = 0;
                    }
                    rigidbody.AddTorque(rightTurntable * getTorque());
                    previousRight = rightTurntable;
                }
            }
            catch (System.ArgumentException e)
            {
                Debug.LogError(e.Message);
                return;
            }

        }

        void DoAltInput()
        {
            if (Input.GetAxis("Acceleration") != 0)
            {
                accel_mod += Input.GetAxis(accelerationKey);
                previousLeft = leftTurntable;
            }

            if (Input.GetAxis("Torque") != 0)
            {
                rigidbody.AddTorque(Input.GetAxis(torqueKey) * getTorque());
                previousRight = rightTurntable;
            }
        }

        void DoMIDIInput()
        {

            if (previousLeft != leftTurntable)
            {
                if (leftTurntable > 0.5f)
                {
                    accel_mod -= accel_incr;
                }
                else if (leftTurntable < 0.5f)
                {
                    accel_mod += accel_incr;
                }
                previousLeft = leftTurntable;
            }

            if (previousRight != rightTurntable)
            {
                float torque = torqueCount + getTorque();

                if (rightTurntable < 0.5f)
                {
                    rigidbody.AddTorque(-torque);
                }
                else
                {
                    rigidbody.AddTorque(torque);
                }
                previousRight = rightTurntable;

                if (multiplyTorque)
                    torqueCount *= torqueIncrement;
                else
                {
                    torqueCount += torqueIncrement;
                }
            }

            currentFrames++;
            if (currentFrames > maxFrames)
            {
                currentFrames = 0;
                torqueCount = 1;
            }

            if (previousFade != crossFade)
            {
                if (crossFade > 0.5f)
                {

                }
                else if (crossFade < 0.5f)
                {

                }

                previousFade = crossFade;
            }
        }

        float getTorque()
        {
            if (currentSpeed == Speed.Slow)
            {
                return torque_slow;
            }
            else if (currentSpeed == Speed.Normal)
            {
                return torque_normal;
            }
            else if (currentSpeed == Speed.Fast)
            {
                return torque_fast;
            }
            else
            {
                return 0;
            }
        }
    }
}