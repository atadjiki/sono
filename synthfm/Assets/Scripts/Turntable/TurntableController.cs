namespace PlayerInput
{

    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using InControl;

    public class TurntableController : MonoBehaviour
    {

        [Header("Controls")]

        public static string accelerationKey = "Acceleration";
        public static string torqueKey = "Torque";

        private float leftTurntable;
        private float rightTurntable;
        private float crossFade;
        private float slider;
        private float rotation;

        private float previousLeft;
        private float previousRight;
        private float previousFade;
        private float previousSlider;
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

        private new Rigidbody2D rigidbody;


        [Header("References")]
        public TurntableManager turntableManager;
        public Transform[] fragmentSlots;
        public bool[] slotsFilled;

        InputBindings inputBindings;
        string saveData;

        [Header("In Game Menu Items")]
        public InGameMenu i_Menu;
        private bool MenuMode = false;

        private bool midiInput = false;

        void OnEnable()
        {
            // See PlayerActions.cs for this setup.

            //playerActions.Move.OnLastInputTypeChanged += ( lastInputType ) => Debug.Log( lastInputType );

            inputBindings = InputBindings.CreateWithDefaultBindings();
            LoadBindings();
        }

        void OnDisable()
        {
            // This properly disposes of the action set and unsubscribes it from
            // update events so that it doesn't do additional processing unnecessarily.
            inputBindings.Destroy();
        }

        void SaveBindings()
        {
            saveData = inputBindings.Save();
            PlayerPrefs.SetString("Bindings", saveData);
        }


        void LoadBindings()
        {
            if (PlayerPrefs.HasKey("Bindings"))
            {
                saveData = PlayerPrefs.GetString("Bindings");
                inputBindings.Load(saveData);
            }
        }


        void OnApplicationQuit()
        {
            PlayerPrefs.Save();
        }

        // Use this for initialization
        void Start()
        {

            turntableManager = GameObject.Find("TurntableInputManager").GetComponent<TurntableManager>(); ;
            rotation = turntableManager.rotation;

            rigidbody = GetComponent<Rigidbody2D>();
            ChangeSpeed(Speed.Normal);

        }

        // Update is called once per frame
        void Update()
        {

            UpdateVariables();
            ApplyForce();

            if (!MenuMode)
            {
                DoAltInput();
            }
            DoMIDIInput();

            DoSpeedInput();
            DoCheckForOverrides();

            if (i_Menu != null)              // Only if menu is there
                DoMenuActions();
        }

        void UpdateVariables()
        {

            leftTurntable = turntableManager.getLeft();
            rightTurntable = turntableManager.getRight();
            crossFade = turntableManager.getFade();
            slider = turntableManager.getSlider();
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
            //    Debug.Log("Speed changed to " + currentSpeed.ToString());
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
            if (inputBindings.SlowSpeed.WasPressed)
            {
                ChangeSpeed(Speed.Slow);
            }
            else if (inputBindings.NormalSpeed.WasPressed)
            {
                ChangeSpeed(Speed.Normal);
            }
            else if (inputBindings.FastSpeed.WasPressed)
            {
                ChangeSpeed(Speed.Fast);
            }

        }

        void DoCheckForOverrides()
        {

            if (inputBindings.SpeedUp.WasPressed)
            {
                fast_override = true;
                Debug.Log("Speed overriden to Fast");
            }
            else if (inputBindings.SpeedUp.WasReleased)
            {
                ChangeSpeed(Speed.Normal);
                fast_override = false;
            }

            if (inputBindings.SlowDown.WasPressed)
            {
                slow_override = true;
                Debug.Log("Speed overriden to Slow");
            }
            else if (inputBindings.SlowDown.WasReleased)
            {
                ChangeSpeed(Speed.Normal);
                slow_override = false;
            }

        }

        void DoMenuActions()            // In Game Menu Actions
        {
            if (inputBindings.Pause.WasPressed)
            {
                toggleMenu();
            }
            if (MenuMode)
            {
                if (inputBindings.Menu_Down.WasPressed)
                {
                    i_Menu.scroll(false);
                }
                else if (inputBindings.Menu_Up.WasPressed)
                {
                    i_Menu.scroll(true);
                }
                else if (inputBindings.Menu_Select.IsPressed)
                {
                    i_Menu.Do_Select(this);
                }
            }
        }

        public void toggleMenu()
        {
            // pull up the menu
            MenuMode = !MenuMode;
            i_Menu.ActivatePannel(MenuMode); // Actiavate / Deactivate Menu Panel
            TogglePause(MenuMode);
        }

        void DoAltInput()
        {
            if (inputBindings.Left.IsPressed)
            {
                rigidbody.AddTorque(1 * getTorque());
                previousRight = rightTurntable;
            }
            else if (inputBindings.Right.IsPressed)
            {
                rigidbody.AddTorque(-1 * getTorque());
                previousRight = rightTurntable;
                //}else if (inputBindings.Pause.WasPressed)
                //{
                //    TogglePause();
                //}
            }
        }
        void DoMIDIInput()
        {

            midiInput = false;

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
                midiInput = true;
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

                midiInput = true;
            }

            currentFrames++;
            if (currentFrames > maxFrames)
            {
                currentFrames = 0;
                torqueCount = 1;
            }

            if (previousSlider != slider)
            {
                midiInput = true;
                if (slider > 0.9f)
                {
                    ChangeSpeed(Speed.Slow);
                }
                else if (slider > 0.35f)
                {
                    ChangeSpeed(Speed.Normal);
                }
                else
                {
                    ChangeSpeed(Speed.Fast);
                }

                previousSlider = slider;
            }
        }

        public bool IsMidiInput()
        {
            return midiInput;
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

        void TogglePause(bool i_state)
        {
            if (i_state)
            {
                Time.timeScale = 0;
            }
            else
            {
                Time.timeScale = 1;
            }
        }

    }
}