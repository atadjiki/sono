using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParasiteController : MonoBehaviour
{

    private Transform player;

    public enum states { IDLE, FLEE, FOLLOW };
    public states currentState;

    private Rigidbody2D rb;
    public float acceleration;
    public float maxSpeed;
    public float turnSpeed;

    public bool isAttached;

    public Transform followTarget;

    //public AudioSource audioSource;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        rb = GetComponent<Rigidbody2D>();

        if(followTarget == null)
        {
            followTarget = player;
        }

    }

    private void Update()
    {
        RunState(currentState);
    }

    public void RunState(states state)
    {
        switch (state)
        {
            case states.IDLE:
                Idle();
                break;
            case states.FOLLOW:
                Follow();
                break;
            case states.FLEE:
                break;
            default:
                break;
        }
    }

    private void Idle()
    {
        rb.velocity = Vector2.Lerp(rb.velocity, Vector2.zero, 1f);
    }

    private void Follow()
    {
        if (followTarget != null)
        {
            //get direction to add torque
            Vector3 evadeDirection = (followTarget.position - transform.position).normalized;
            float angle = Mathf.Atan2(evadeDirection.y, evadeDirection.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle - 90f, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, turnSpeed * Time.deltaTime);

            //accelerate
            float restrictedAccel = Vector2.Distance(transform.position, player.position) / 25f * acceleration;

            rb.AddForce(transform.up * restrictedAccel * Time.deltaTime);
        }
    }

    public states GetState()
    {
        return currentState;
    }
}
