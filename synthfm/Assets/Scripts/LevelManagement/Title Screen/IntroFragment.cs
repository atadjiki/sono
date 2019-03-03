using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroFragment : MonoBehaviour
{
    private enum states { FLEE, CIRCLE };
    private states currentState;
    public float timeUntilKill = 10f;
    private Rigidbody2D rb;
    public float acceleration = 500;
    public float maxSpeed = 50;
    public float turnSpeed = 25;

    private AudioSource audioSource;

    public Transform followTarget;
    private Vector3 previousTargetPosition;

    public TrailRenderer trail;

    private void Start()
    {
        if(followTarget == null)
        {
            followTarget = GameObject.Find("Player").transform;
        }
        if(trail == null)
        {
            trail = GetComponentInChildren<TrailRenderer>();
        }
        
        previousTargetPosition = followTarget.position;

        currentState = states.CIRCLE;
        rb = GetComponent<Rigidbody2D>();

        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        RunState(currentState);
    }

    private void RunState(states state)
    {
        switch (state)
        {
            case states.CIRCLE:
                Follow();
                break;
            case states.FLEE:
                Flee();
                break;
            default:
                break;
        }
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
            rb.AddForce(transform.up * acceleration * Time.deltaTime);
        }

        if(followTarget.position != previousTargetPosition)
        {
            Kill();
        }
    }

    private void Flee()
    {
        //accelerate
        rb.AddForce(transform.up * acceleration * 2f * Time.deltaTime);
        trail.time = Mathf.Lerp(trail.time, 0f, 0.025f);
    }

    private IEnumerator KillTimer()
    {
        yield return new WaitForSeconds(timeUntilKill);

        Destroy(gameObject);
    }

    public void Kill()
    {
        currentState = states.FLEE;
        StartCoroutine(KillTimer());
    }
}
