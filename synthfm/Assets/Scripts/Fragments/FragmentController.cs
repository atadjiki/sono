using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FragmentController : MonoBehaviour
{
    private Transform player;

    public enum states { IDLE, FLEE, FOLLOW, DEPOSIT };
    public states currentState;

    

    private Rigidbody2D rb;
    public float acceleration;
    public float maxSpeed;
    public float turnSpeed;

    public bool isAttached;

    public AudioSource audioSource;

    public Transform followTarget;
    public Transform newTarget;
    [HideInInspector]
    public GameObject fragmentCase;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        currentState = states.IDLE;
        rb = GetComponent<Rigidbody2D>();

        audioSource = GetComponent<AudioSource>();
        audioSource.volume = 0f;

    }

    private void Update()
    {
        RunState(currentState);
    }

    private void FixedUpdate()
    {
        if(currentState == states.DEPOSIT)
        {
            transform.RotateAround(GameObject.FindGameObjectWithTag("Hub").transform.position, new Vector3(0, 0, 1), 50 * Time.deltaTime);
            print("Rotating around Hub");
        }
    }

    public void RunState(states state)
    {
        switch(state)
        {
            case states.IDLE:
                Idle();
                break;
            case states.FOLLOW:
                Follow();
                break;
            case states.FLEE:
                break;
            case states.DEPOSIT:
                break;
            default:
                break;
        }
    }

    private void Idle()
    {
        rb.velocity = Vector2.Lerp(rb.velocity, Vector2.zero, 1f);
    }

    public void Deposit(Transform newTarget)
    {
        //transform.position = newTarget.transform.position;
        currentState = states.DEPOSIT;
        isAttached = false;

       // GameObject hub = GameObject.FindGameObjectWithTag("Hub");
       // transform.LookAt(newTarget.transform);
       // transform.Translate(transform.forward * Time.deltaTime * acceleration);
       // transform.position = newTarget.transform.position;


        print("Deposit fragment");
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

    public void Collect(Transform newFollowTarget)
    {
        followTarget = newFollowTarget;
        isAttached = true;
        currentState = states.FOLLOW;
        audioSource.time = LevelManager.instance.playerAudioSource.time;
        audioSource.Play();
        StartCoroutine(FadeInAudio());
        transform.SetParent(LevelManager.instance.transform);
        Destroy(fragmentCase);
    }

    public void SetClip(AudioClip newClip)
    {
        audioSource.clip = newClip;
    }

    private IEnumerator FadeInAudio()
    {
        float volume = 0f;

        while(volume < 0.5f)
        {
            volume += Time.deltaTime / 2f;
            audioSource.volume = volume;

            yield return null;
        }

        audioSource.volume = 0.5f;
    }

    public states GetState()
    {
        return currentState;
    }
}
