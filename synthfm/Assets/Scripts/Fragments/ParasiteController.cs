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
    private float timeUntilKill = 5;

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
                Flee();
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
            Vector3 followRandomPosition = followTarget.position;
            followRandomPosition.x += Random.Range(-15, 15);
            followRandomPosition.y += Random.Range(-30, 30);

            Vector3 evadeDirection = (followRandomPosition - transform.position).normalized;
            float angle = Mathf.Atan2(evadeDirection.y, evadeDirection.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle - 90f, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, turnSpeed * Time.deltaTime);

            //accelerate
            float restrictedAccel = Vector2.Distance(transform.position, player.position) / Random.Range(5, 25) * acceleration;

            rb.AddForce(transform.up * restrictedAccel * Time.deltaTime);
        }
    }

    private void Flee()
    {
        //accelerate
        rb.AddForce(transform.up * acceleration * 2f * Time.deltaTime);
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

    public states GetState()
    {
        return currentState;
    }
}
