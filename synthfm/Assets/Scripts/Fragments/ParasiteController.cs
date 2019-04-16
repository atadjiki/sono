using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParasiteController : MonoBehaviour
{

    private Transform player;

    public enum states { IDLE, FLEE, FOLLOW };
    public states currentState = states.FOLLOW;

    private Rigidbody2D rb;
    public float acceleration;
    public float maxSpeed;
    public float turnSpeed;
    private float timeUntilKill = 5;
    public int radius_factor = 0;

    public bool isAttached;

    public Transform followTarget;
    private Vector3 followRandomPosition;

    private float maxFrames = 15f;
    private float currentFrames = 0f;

    private bool followRight = false;
    private bool followUp = false;

    public static float x_range = 70;
    public static float y_range = 70;

    public float currentRange;

    //public AudioSource audioSource;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        rb = GetComponent<Rigidbody2D>();

        if(followTarget == null)
        {
            followTarget = player;
        }

        if(Random.Range(0.0f, 1.0f) >= 0.5f)
        {
            followRight = true;
        }
        if (Random.Range(0.0f, 1.0f) >= 0.5f)
        {
            followUp = true;
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
            followRandomPosition = followTarget.position + calculateRange();


            if (Vector3.Distance(this.transform.position, followTarget.transform.position) >= 500)
            {
                ParasiteSpawner.instance.KillParasite(this, true);
            }


            Vector3 evadeDirection = (followRandomPosition - transform.position).normalized;
            float angle = Mathf.Atan2(evadeDirection.y, evadeDirection.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle - 90f, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, turnSpeed * Time.deltaTime);

            //accelerate
            float restrictedAccel = Vector2.Distance(transform.position, player.position) / Random.Range(5, 25) * acceleration;

            rb.AddForce(transform.up * restrictedAccel * Time.deltaTime);
        }
    }

    private Vector3 calculateRange()
    {

        Vector3 range = new Vector3();

        FragmentManager.instance.RefreshFragmentList();
        radius_factor = FragmentManager.instance.AttachedFragments().Count;


        if(radius_factor > 0)
        {

            float factor = Mathf.Pow(radius_factor, 2);
            range += new Vector3(x_range + factor, y_range + factor, 0f);

            if (!followUp)
            {
                range.y *= -1;
            }
            if (!followRight)
            {
                range.x *= -1;
            }

        }
        else
        {
            range.x = Random.Range(-x_range, x_range);
            range.y = Random.Range(-y_range, y_range);
            
        }

        range.z = 0;

        currentRange = range.magnitude;
        return range;
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
