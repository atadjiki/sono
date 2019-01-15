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


    private Rigidbody2D rigidbody;

    public TurntableManager turntableManager;

	// Use this for initialization
	void Start () {

        rotation = turntableManager.rotation;
        rigidbody = GetComponent<Rigidbody2D>();
		
	}
	
	// Update is called once per frame
	void Update () {

        UpdateVariables();


       // float accel_modifier = Mathf.Clamp(leftTurntable, 0.0f, 1f);
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
        rigidbody.AddForce(transform.right * accel_mod * acceleration * Time.deltaTime);



        if (previousRight != rightTurntable)
        {
            if (rightTurntable < 0.5f)
            {
                //this.gameObject.transform.Rotate(Vector3.forward * -rotation);
                rigidbody.AddTorque(-torqueAmount);
            }
            else if (rightTurntable > 0.5f)
            {
                //this.gameObject.transform.Rotate(Vector3.forward * rotation);
                rigidbody.AddTorque(torqueAmount);
            }
            previousRight = rightTurntable;
        }
        

    }

    void UpdateVariables()
    {

        leftTurntable = turntableManager.getLeft();
        rightTurntable = turntableManager.getRight();
        crossFade = turntableManager.getFade();
    }    
}
