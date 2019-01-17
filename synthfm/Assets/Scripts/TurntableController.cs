using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public float torqueAmount = 10;
    public float torqueIncrement = 1;
    public bool multiplyTorque = false;

    public KeyCode alt_left_1 = KeyCode.A;
    public KeyCode alt_left_2 = KeyCode.D;

    public KeyCode alt_right_1 = KeyCode.LeftArrow;
    public KeyCode alt_right_2 = KeyCode.RightArrow;

    public bool ForceAlternativeControls = false;

    public AnimationCurve curve;

    public Cinemachine.CinemachineVirtualCamera camera;
    public int minOrthSize = 5;
    public int maxOrthSize = 50;
    public int orthSize = 50;


    private Rigidbody2D rigidbody;

    public TurntableManager turntableManager;

	// Use this for initialization
	void Start () {

       rotation = turntableManager.rotation;

        rigidbody = GetComponent<Rigidbody2D>();
		
	}
	
	// Update is called once per frame
	void Update () {

        if (ForceAlternativeControls)
        {
            DoAltInput();

        }
        else
        {
            DoAltInput();
            DoMIDIInput();
        }
    }

    void UpdateVariables()
    {

        leftTurntable = turntableManager.getLeft();
        rightTurntable = turntableManager.getRight();
        crossFade = turntableManager.getFade();
        Debug.Log("Update Variables: Left" + leftTurntable + ", Right: " + +rightTurntable + ", Fade: " + crossFade);
    }    

    void DoAltInput()
    {
        if (Input.GetAxis("Acceleration") != 0)
        {
            accel_mod += Input.GetAxis("Acceleration");
            previousLeft = leftTurntable;
        }
        accel_mod = Mathf.Clamp(accel_mod, -accel_clamp, accel_clamp);
        rigidbody.AddForce(transform.up * accel_mod * acceleration * Time.deltaTime);

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
        accel_mod = Mathf.Clamp(accel_mod, -accel_clamp, accel_clamp);
        rigidbody.AddForce(transform.up * accel_mod * acceleration * Time.deltaTime);

        if (previousRight != rightTurntable)
        {
          //  float input = curve.Evaluate(1-rightTurntable);
            float torque = torqueCount + torqueAmount;
          //  torque *= input;

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
                orthSize++;
            }
            else if (crossFade < 0.5f)
            {
                orthSize--;
            }
            orthSize = Mathf.Clamp(orthSize, minOrthSize, maxOrthSize);
            camera.m_Lens.OrthographicSize = Mathf.Clamp(orthSize * crossFade , minOrthSize, maxOrthSize);
            previousFade = crossFade;
        }


    }
} 
