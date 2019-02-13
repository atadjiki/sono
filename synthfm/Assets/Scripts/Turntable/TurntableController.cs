using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;

public class TurntableController : MonoBehaviour {

    private float leftTurntable;
    private float rightTurntable;
    private float crossFade;
    private float rotation;

    private float previousLeft;
    private float previousRight;
    private float previousFade;
    private int currentFrames = 0;
    public int maxFrames = 60;
    private float torqueCount = 1;

    public float acceleration = 5000;
    private float accel_mod;
    public float accel_incr = 0.05f;
    public float accel_clamp = 1f;
    public float accel_floor = 0.1f;

    public float accel_slow = 0.25f;
    public float accel_normal = 0.75f;
    public float accel_fast = 1.25f;

    public float torqueAmount = 10;
    public float torqueIncrement = 1;
    public bool multiplyTorque = false;

    public KeyCode alt_left_1 = KeyCode.A;
    public KeyCode alt_left_2 = KeyCode.D;

    public KeyCode alt_right_1 = KeyCode.LeftArrow;
    public KeyCode alt_right_2 = KeyCode.RightArrow;

    public bool KeyboardControls = true;
    public bool TurntableControls = true;
    public bool JoystickControls = true;

    private Rigidbody2D rigidbody;

    public TurntableManager turntableManager;

    public GameObject head;
    private float angle = 0f;
    public float maxTiltAngle;
    public float tiltSpeed = 30f;

    public Transform[] fragmentSlots;
    public bool[] slotsFilled;

	// Use this for initialization
	void Start () {

       rotation = turntableManager.rotation;

        rigidbody = GetComponent<Rigidbody2D>();
		
	}

    // Update is called once per frame
    void Update()
    {
        if(JoystickControls)
            DoJoyStickInput();
        if(KeyboardControls)
            DoAltInput();
        if(TurntableControls)
            DoMIDIInput();
    }

    void UpdateVariables()
    {

        leftTurntable = turntableManager.getLeft();
        rightTurntable = turntableManager.getRight();
        crossFade = turntableManager.getFade();
        //Debug.Log("Update Variables: Left" + leftTurntable + ", Right: " + +rightTurntable + ", Fade: " + crossFade);
    }   

    float normalizeAcceleration(float accel)
    {

        float slowDif = Mathf.Abs(accel - accel_slow);
        float normalDif = Mathf.Abs(accel - accel_normal);
        float fastDif = Mathf.Abs(accel - accel_fast);

        if(slowDif < normalDif && slowDif < fastDif)
        {
            return accel_slow;
        }else if(normalDif < slowDif && normalDif < fastDif)
        {
            return accel_normal;
        }
        else
        {
            return accel_fast;
        }

    }

    void DoJoyStickInput()
    {

        float leftStickX = XCI.GetAxis(XboxAxis.LeftStickX);
        float rightStickX = XCI.GetAxis(XboxAxis.RightStickX);

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
        accel_mod = Mathf.Clamp(accel_mod, accel_floor, accel_clamp);
        rigidbody.AddForce(transform.up * normalizeAcceleration(accel_mod) * acceleration * Time.deltaTime);

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
            rigidbody.AddTorque(rightTurntable * torqueAmount);
            previousRight = rightTurntable;
        }
    }

    void DoAltInput()
    {
        if (Input.GetAxis("Acceleration") != 0)
        {
            accel_mod += Input.GetAxis("Acceleration");
            previousLeft = leftTurntable;
        }
        accel_mod = Mathf.Clamp(accel_mod, accel_floor, accel_clamp);
        rigidbody.AddForce(transform.up * normalizeAcceleration(accel_mod) * acceleration * Time.deltaTime);

        if (Input.GetAxis("Torque") != 0)
        {
            rigidbody.AddTorque(Input.GetAxis("Torque") * torqueAmount);
            previousRight = rightTurntable;
        }
    }

    void DoMIDIInput()
    {
        UpdateVariables();

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
        accel_mod = Mathf.Clamp(accel_mod, accel_floor, accel_clamp);
        rigidbody.AddForce(transform.up * normalizeAcceleration(accel_mod) * acceleration * Time.deltaTime);
        
        if (previousRight != rightTurntable)
        {
            float torque = torqueCount + torqueAmount;

            if (rightTurntable < 0.5f)
            {
                rigidbody.AddTorque(-torque);
            }
            else
            {
                rigidbody.AddTorque(torque);
            }
            previousRight = rightTurntable;

            if(multiplyTorque)
                torqueCount*=torqueIncrement;
            else
            {
                torqueCount += torqueIncrement;
            }
        }

        currentFrames++;
        if(currentFrames > maxFrames)
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
} 
