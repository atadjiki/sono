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
        private int maxFrames = 0;
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

        public float torque_slow = 45f;
        public float torque_normal = 35f;
        public float torque_fast = 25f;

        private bool fast_override = false;
        private bool slow_override = false;

        public float torqueIncrement = 1.5f;
        public bool multiplyTorque = true;

        public enum Speed { Slow, Normal, Fast, None };
        public Speed currentSpeed;

        private new Rigidbody2D rigidbody;


        [Header("References")]
        public TurntableManager turntableManager;
        public Transform[] fragmentSlots;
        public bool[] slotsFilled;
        public MeshRenderer playerBody;

        InputBindings inputBindings;
        string saveData;

        [Header("In Game Menu Items")]
        public InGameMenu i_Menu;
        private bool MenuMode = false;

        private float maxMidiMenuFrames = 12f;
        private float currentMidiMenuFrames = 0f;

        private float maxButtonFrames = 0f;
        private float currentButtonFrames = 0;

        //    private bool midiInput = false;

        private bool movingTowardsMouse = false;
        private Vector3 lastMousePosition;
        private float timeSinceLastClick = 0;

        private float angle_threshold = 5f;

        public float camera_lerp = 0.08f;
        public float speed_zoom_out = 20f;
        public float speed_zoom_in = 5f;
        public float zoom_default = -75;

        public Cinemachine.CinemachineVirtualCamera CM_Main;

        private List<List<Color>> sonoColors;
        private int currentSonoIndex;


        [Header("Animations")]
        public Animator animator;

        void OnEnable()
        {
            inputBindings = InputBindings.CreateWithDefaultBindings();
            LoadBindings();
        }

        void OnDisable()
        {
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

            currentSonoIndex = 0;

            turntableManager = GameObject.Find("TurntableInputManager").GetComponent<TurntableManager>(); ;
            rotation = turntableManager.rotation;

            ChangeColor cc = GameObject.Find("Main Camera").GetComponent<ChangeColor>();
            sonoColors = new List<List<Color>>();

            sonoColors.Add(cc.firstamberPuzzleColor);
            sonoColors.Add(cc.secondamberPuzzleColor);
            sonoColors.Add(cc.thirdamberPuzzleColor);
            sonoColors.Add(cc.firstFiberPuzzleColor);
            sonoColors.Add(cc.secondFiberPuzzleColor);
            sonoColors.Add(cc.thirdFiberPuzzleColor);
            sonoColors.Add(cc.firstlattePuzzleColor);
            sonoColors.Add(cc.secondlattePuzzleColor);
            sonoColors.Add(cc.thirdlattePuzzleColor);


            rigidbody = GetComponent<Rigidbody2D>();
            ChangeSpeed(Speed.Normal);

            CM_Main.GetCinemachineComponent<Cinemachine.CinemachineTransposer>().m_FollowOffset.z = zoom_default;

        }

        // Update is called once per frame
        void Update()
        {

            UpdateVariables();
            ApplyForce();

            if (!MenuMode)
            {
                DoAltInput();
                //DoMouseInput();
            }
            DoMIDIInput();

            DoSpeedInput();
            SetCameraZoom(DoCheckForOverrides(), currentSpeed);

            if (i_Menu != null)              // Only if menu is there
                DoMenuActions();


        }

        void UpdateVariables()
        {

            leftTurntable = turntableManager.getLeft();
            rightTurntable = turntableManager.getRight();
            crossFade = turntableManager.getFade();
            slider = turntableManager.getSlider();
            // Debug.Log("Update Variables: Left" + leftTurntable + ", Right: " + +rightTurntable + ", Fade: " + crossFade);
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
            UpdateAnimation(currentSpeed);

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

        bool DoSpeedInput()
        {
            if (inputBindings.SlowSpeed.WasPressed)
            {
                ChangeSpeed(Speed.Slow);
                return true;
            }
            else if (inputBindings.NormalSpeed.WasPressed)
            {
                ChangeSpeed(Speed.Normal);
                return true;
            }
            else if (inputBindings.FastSpeed.WasPressed)
            {
                ChangeSpeed(Speed.Fast);
                return true;
            }

            return false;

        }

        Speed DoCheckForOverrides()
        {

            if (inputBindings.SpeedUp.IsPressed)
            {
                fast_override = true;
                UpdateAnimation(Speed.Fast);
                return Speed.Fast;
            }
            else if (inputBindings.SpeedUp.WasReleased)
            {
                ChangeSpeed(Speed.Normal);
                fast_override = false;
            }

            if (inputBindings.SlowDown.IsPressed)
            {
                slow_override = true;
                UpdateAnimation(Speed.Slow);
                return Speed.Slow;
            }
            else if (inputBindings.SlowDown.WasReleased)
            {
                ChangeSpeed(Speed.Normal);
                slow_override = false;
            }

            return Speed.None;

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

        bool VectorRight(Vector3 a, Vector3 b)
        {
            if (a.x > b.x)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        bool VectorUp(Vector3 a, Vector3 b)
        {
            if (a.y > b.y)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        void TorqueTowardsMouse(Vector3 mousePosition, Vector3 playerPosition, float angleBetween)
        {


            //Mouse below and to the right of player
            if (VectorRight(mousePosition, playerPosition) && !VectorUp(mousePosition, playerPosition))
            {
                rigidbody.AddTorque(-1 * getTorque());
            }

            //Mouse below and to the left of player
            else if (!VectorRight(mousePosition, playerPosition) && !VectorUp(mousePosition, playerPosition))
            {
                rigidbody.AddTorque(1 * getTorque());
            }
            //Mouse above and to the right of player
            else if (VectorRight(mousePosition, playerPosition) && VectorUp(mousePosition, playerPosition))
            {
                rigidbody.AddTorque(-1 * getTorque());
            }
            //Mouse above and to the left of player
            else if (!VectorRight(mousePosition, playerPosition) && VectorUp(mousePosition, playerPosition))
            {
                rigidbody.AddTorque(1 * getTorque());
            }


            previousRight = rightTurntable;
        }

        //https://answers.unity.com/questions/855976/make-a-player-model-rotate-towards-mouse-location.html
        void DoMouseInput()
        {

            Vector3 mousePosition = Input.mousePosition;
            Vector3 playerPosition = Camera.main.WorldToScreenPoint(this.transform.position);
            Vector3 forward = this.transform.up;
            playerPosition.z = 0;

            Vector3 targetDir = mousePosition - playerPosition;
            float angleBetween = Vector3.Angle(targetDir, forward);

            if (inputBindings.Screen_Touch.IsPressed)
            {
                ChangeSpeed(Speed.Slow);
            }

            if (inputBindings.Screen_Touch.WasReleased)
            {
                lastMousePosition = mousePosition;
                movingTowardsMouse = true;
                Debug.Log("Moving towards mouse");

                //detect double click
                if ((Time.time - timeSinceLastClick) < 2f)
                {
                    ChangeSpeed(Speed.Fast);
                    timeSinceLastClick = Time.time;
                }
                else
                {
                    ChangeSpeed(Speed.Normal);
                    timeSinceLastClick = Time.time;
                }
            }
            else if (movingTowardsMouse && lastMousePosition != null)
            {

                if (angleBetween < angle_threshold)
                {
                    movingTowardsMouse = false;
                }
                else
                {
                    targetDir = lastMousePosition - playerPosition;
                    TorqueTowardsMouse(targetDir, playerPosition, angleBetween);
                }
            }

        }

        bool DoAltInput()
        {
            if (inputBindings.Left.IsPressed)
            {
                rigidbody.AddTorque(1 * getTorque());
                previousRight = rightTurntable;
                return true;
            }
            else if (inputBindings.Right.IsPressed)
            {
                rigidbody.AddTorque(-1 * getTorque());
                previousRight = rightTurntable;
                return true;
            }

            return false;
        }
        void DoMIDIInput()
        {

            turntableManager.UpdateLastInteracted();

            if (turntableManager.messageReceived && turntableManager.lastInteracted == TurntableManager.DJTechControl.Wheel)
            {
                if (MenuMode)
                {
                    if (currentMidiMenuFrames >= maxMidiMenuFrames)
                    {
                        if (rightTurntable < 0.5f)
                        {
                            i_Menu.scroll(true);
                        }
                        else if (leftTurntable > 0.5f)
                        {
                            i_Menu.scroll(false);
                        }
                        previousLeft = leftTurntable;
                        previousRight = rightTurntable;
                        currentMidiMenuFrames = 0f;
                    }
                    else
                    {
                        currentMidiMenuFrames++;
                    }


                }
                else
                {
                    float torque = torqueCount + getTorque();

                    if (rightTurntable < 0.5f)
                    {
                        rigidbody.AddTorque(torque);
                    }
                    else if (leftTurntable > 0.5f)
                    {
                        rigidbody.AddTorque(-torque);
                    }
                    previousLeft = leftTurntable;
                    previousRight = rightTurntable;
                }


            }

            else if (turntableManager.messageReceived && turntableManager.lastInteracted == TurntableManager.DJTechControl.Slider)
            {
                if (!MenuMode)
                {
                    if (slider == 0)
                        return;

                    if (slider > 0.6f)
                    {
                        ChangeSpeed(Speed.Fast);
                    }
                    else if (slider > 0.385f)
                    {
                        ChangeSpeed(Speed.Normal);
                    }
                    else
                    {
                        ChangeSpeed(Speed.Slow);
                    }

                    previousSlider = slider;
                }

            }
            else if (turntableManager.messageReceived && turntableManager.lastInteracted == TurntableManager.DJTechControl.Knob)
            {
                print(turntableManager.fetchSmallKnob());
                //LEFT
                if (turntableManager.fetchSmallKnob() < 0.5f && currentFrames >= maxFrames)
                {
                    if (currentSonoIndex > 1)
                    {
                        currentSonoIndex--;

                    }
                    else
                    {
                        currentSonoIndex = sonoColors.Count - 1;
                    }

                    Debug.Log("Knob turned left");
                    Color playercolorToChangeTo = sonoColors[currentSonoIndex][1];
                    Color playerCol = playerBody.material.GetColor("Color_D2FAE4B8");
                    playerCol = Color.Lerp(playerCol, playercolorToChangeTo, 0.5f);
                    playerBody.material.SetColor("Color_D2FAE4B8", playerCol);

                }
                //RIGHT
                else if(turntableManager.fetchSmallKnob() > 0.5f && currentFrames >= maxFrames)
                {
                    currentSonoIndex++;

                    if(currentSonoIndex >= sonoColors.Count)
                    {
                        currentSonoIndex = 0;
                    }
                    print(currentSonoIndex);

                    Debug.Log("Knob turned right");
                    Color playercolorToChangeTo = sonoColors[currentSonoIndex][1];
                    Color playerCol = playerBody.material.GetColor("Color_D2FAE4B8");
                    playerCol = Color.Lerp(playerCol, playercolorToChangeTo, 0.5f);
                    playerBody.material.SetColor("Color_D2FAE4B8", playerCol);
                }
                else
                {

                }
            }
            else if (turntableManager.messageReceived && turntableManager.lastInteracted == TurntableManager.DJTechControl.Cue)
            {
                Debug.Log("Cue Pressed");

            }
            else if (turntableManager.messageReceived && turntableManager.lastInteracted == TurntableManager.DJTechControl.KnobPress)
            {
                Debug.Log("Knob Pressed");
            }
            if (currentButtonFrames >= maxButtonFrames)
            {


                if (turntableManager.messageReceived && turntableManager.lastInteracted == TurntableManager.DJTechControl.Play)
                {

                    if (!MenuMode)
                    {
                        toggleMenu();
                    }
                    else if (MenuMode)
                    {
                        i_Menu.Do_Select(this);
                    }
                }

                currentButtonFrames = 0;
            }
            else
            {
                currentButtonFrames++;
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

        void SetCameraZoom(Speed overriden, Speed speed)
        {

            float current_z = CM_Main.GetCinemachineComponent<Cinemachine.CinemachineTransposer>().m_FollowOffset.z;
            float follow_z = zoom_default;


            if (speed == Speed.Fast || overriden == Speed.Fast)
            {
                follow_z = Mathf.Lerp(current_z, zoom_default - speed_zoom_out, camera_lerp);
            }
            else if (speed == Speed.Slow || overriden == Speed.Slow)
            {
                follow_z = Mathf.Lerp(current_z, zoom_default + speed_zoom_in, camera_lerp);
            }
            else
            {
                follow_z = Mathf.Lerp(current_z, zoom_default, camera_lerp);
            }

            CM_Main.GetCinemachineComponent<Cinemachine.CinemachineTransposer>().m_FollowOffset.z = follow_z;
        }

        void UpdateAnimation(Speed speed)
        {
            // Get the id of all state for this object
            int slowId = Animator.StringToHash("Slow");
            int normalId = Animator.StringToHash("Normal");
            int fastId = Animator.StringToHash("Fast");

            if (speed == Speed.Slow)
            {
                //   Debug.Log("Playing slow animation: " + slowId);
                animator.Play(slowId);

            }
            else if (speed == Speed.Normal)
            {
                //  Debug.Log("Playing normal animation: " + normalId);
                animator.Play(normalId);
            }
            else if (speed == Speed.Fast)
            {
                //  Debug.Log("Playing fast animation: " + fastId);
                animator.Play(fastId);
            }

            AnimatorStateInfo animStateInfo = animator.GetCurrentAnimatorStateInfo(0);

            if (slowId == animStateInfo.nameHash)
            {
                //  Debug.Log("Current state is Slow");
            }
            else if (normalId == animStateInfo.nameHash)
            {
                //  Debug.Log("Current state is Normal");
            }
            else if (fastId == animStateInfo.nameHash)
            {
                //  Debug.Log("Current state is Fast");

            }

        }

    }
}