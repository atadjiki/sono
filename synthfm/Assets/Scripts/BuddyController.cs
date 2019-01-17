using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuddyController : MonoBehaviour
{
    private Transform player;

    private enum states { IDLE, FLEE, FOLLOW };
    private states currentState;

    private Rigidbody2D rb;
    public float acceleration;
    public float maxSpeed;
    public float turnSpeed;

    public Transform followTarget;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        currentState = states.FOLLOW;
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if(currentState == states.FOLLOW)
        {
            Follow();
        }
    }

    private void Follow()
    {
        //get direction to add torque
        Vector3 evadeDirection = (followTarget.position - transform.position).normalized;
        float angle = Mathf.Atan2(evadeDirection.y, evadeDirection.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle - 90f, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, turnSpeed * Time.deltaTime);

        //accelerate
        rb.AddForce(transform.up * acceleration * Time.deltaTime);
    }
}
