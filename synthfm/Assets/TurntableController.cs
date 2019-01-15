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

    public float acceleration;
    public float accel_mod;
    public float accel_incr;
    public float accel_clamp;

    public float torqueAmount;

    public KeyCode alt_left_1;
    public KeyCode alt_left_2;

    public KeyCode alt_right_1;
    public KeyCode alt_right_2;

    public bool UseAlternativeControls = false;


    private Rigidbody2D rigidbody;

    public TurntableManager turntableManager;

	// Use this for initialization
	void Start () {

       rotation = turntableManager.rotation;

        rigidbody = GetComponent<Rigidbody2D>();
		
	}
	
	// Update is called once per frame
	void Update () {

        if (UseAlternativeControls)
        {
            if (Input.GetKey(alt_left_1) || Input.GetKey(alt_left_2))
            {
                if (Input.GetKey(alt_left_1))
                {
                    accel_mod -= accel_incr;
                }
               if (Input.GetKey(alt_left_2))
                {
                    accel_mod += accel_incr;
                }
                previousLeft = leftTurntable;
            }
            accel_mod = Mathf.Clamp(accel_mod, -accel_clamp, accel_clamp);
            rigidbody.AddForce(transform.up * accel_mod * acceleration * Time.deltaTime);

            if (Input.GetKey(alt_right_1) || Input.GetKey(alt_right_2))
            {
                if (Input.GetKey(alt_right_2))
                {
                    rigidbody.AddTorque(-torqueAmount);
                }
                if (Input.GetKey(alt_right_1))
                {
                    rigidbody.AddTorque(torqueAmount);
                }
                previousRight = rightTurntable;
            }

        }
        else
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
                if (rightTurntable < 0.5f)
                {
                    rigidbody.AddTorque(-torqueAmount);
                }
                else if (rightTurntable > 0.5f)
                {
                    rigidbody.AddTorque(torqueAmount);
                }
                previousRight = rightTurntable;
            }
        }
    }

    void UpdateVariables()
    {

        leftTurntable = turntableManager.getLeft();
        rightTurntable = turntableManager.getRight();
        crossFade = turntableManager.getFade();
        Debug.Log("Update Variables: " + leftTurntable + "," + +rightTurntable + "," + crossFade);
    }    
}
