using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.VFX;
public class FragmentController : MonoBehaviour
{
    private Transform player;

    public enum states { IDLE, FLEE, FOLLOW, DEPOSIT, VOID, FINAL_PATERN, PRE_FINAL, LEAD };

    public enum world { AMBER, LATTE, FIBER,HUB};

    public states currentState;

    public world currentWorld;

    public GameObject portal;

    public Navpoint playerEyeball;

    private Rigidbody2D rb;
    public float acceleration;
    public float maxSpeed;
    public float turnSpeed;

    public bool isAttached;

    public AudioSource audioSource;

    [Header("Score Manager")]
    ScoreManager scoreManager;
    public int TrackIndex;

    CircleCollider2D tempCollider;

    public Transform followTarget;
    public Transform newTarget;
    [HideInInspector]
    public GameObject fragmentCase;

    float angle, radius = 10;
    float angleSpeed = 2;
    float radialSpeed = 0.5f;

    private Transform DetatchedPosition; /* the transform info where the fragment is detatched from the player
                                         * While leaving the Zone without all three fragments
                                           */
    private BezierFollow bzFollow;
    private PatternGenerator ptGenerator;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();

        audioSource = GetComponent<AudioSource>();
        
        audioSource.volume = 0f;

        bzFollow = this.GetComponent<BezierFollow>();
        ptGenerator = this.GetComponent<PatternGenerator>();
    }

    private void Start()
    {
        scoreManager = ScoreManager.GetInstance();
        playerEyeball = GameObject.Find("Player").GetComponent<Navpoint>();

    }

    private void Update()
    {
        RunState(currentState);
    }

    private void FixedUpdate()
    {
        if (currentState == states.DEPOSIT)
        {
            // TO DO: DO A MOTHERFUCKING SPIRAL
            //transform.LookAt(portal.transform);
            transform.position = Vector3.MoveTowards(transform.position, GameObject.Find("MoveTowards").transform.position, 20 * Time.deltaTime);
          

        }
    }

    public IEnumerator RotateFragments()
    {
        yield return null;
    }

    public void getColliderDeposit(CircleCollider2D coll)
    {
        tempCollider = coll ;
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
                Flee();
                break;
            case states.DEPOSIT:
                break;
            case states.VOID:
                Void();
                break;
            case states.FINAL_PATERN:
                CreatePatterns();
                break;
            case states.PRE_FINAL:
                Void();
                break;
            case states.LEAD:
                Lead();
                break;
            default:
                break;
        }
    }

    private void Idle()
    {
        rb.velocity = Vector2.Lerp(rb.velocity, Vector2.zero, 1f);
    }

    public void Lead()
    {
        transform.position =  bzFollow.fragPos;
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

        scoreManager.FadeOutMixerGroup(TrackIndex);
        print("Deposit fragment");
    }

    public void makeFollowCurveStartingPoint()
    {
        followTarget =  gameObject.GetComponent<PatternGenerator>().getStartingPoint();
    }

    private void Follow()
    {
        if (followTarget != null)
        {
            if(followTarget.gameObject == player.gameObject)
            {
                isAttached = true;
            }
            
            //get direction to add torque
            Vector3 followRandomPosition = followTarget.position;
            followRandomPosition.x += Random.Range(-5, 5);
            followRandomPosition.y += Random.Range(-5, 5);

            Vector3 evadeDirection = (followRandomPosition - transform.position).normalized;
            float angle = Mathf.Atan2(evadeDirection.y, evadeDirection.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle - 90f, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, turnSpeed * Time.deltaTime);

            //accelerate
            float restrictedAccel = Vector2.Distance(transform.position, player.position) / 25f * acceleration;

            rb.AddForce(transform.up * restrictedAccel * Time.deltaTime);

            playerEyeball.currentFrames = playerEyeball.maxFrames;
        }
    }

    public void Flee()
    {
        rb.velocity = Vector2.Lerp(rb.velocity, Vector2.zero, 1f);

        // this.gameObject.transform.position = ptGenerator.getStartingPoint().position;
       
    }

    public void getReadyForCurve()
    {
         // move to starting points of curve
         StartCoroutine(MoveToCurve());
        currentState = states.PRE_FINAL;
    }

    // change the position to curve starting point
    IEnumerator MoveToCurve()
    {
        yield return new WaitForSeconds(1);
        this.gameObject.transform.position = ptGenerator.getStartingPoint().position;
        changeTrailTime(5);
    }

    public void CreatePatterns()
    {
        if (!ptGenerator.ToBegin)
        {
            ptGenerator.ToBegin = true;
        }
        this.transform.position = ptGenerator.fragPos;
    }

    public void changeTrailTime(float i_time)
    {
        TrailRenderer tr = this.gameObject.transform.Find("Trail").GetComponent<TrailRenderer>();
        tr.time = i_time;
    }

    public void Void()
    {
        rb.velocity = Vector2.Lerp(rb.velocity, Vector2.zero, 1f);
    }

    public void setDetatchingTransform(Transform i_tf)
    {
        DetatchedPosition = i_tf;
    }

    public void Collect(Transform newFollowTarget)
    {
        followTarget = newFollowTarget;
        isAttached = true;
        currentState = states.FOLLOW;

        //audioSource.time = LevelManager.instance.playerAudioSource.time;
        //audioSource.Play();
        //StartCoroutine(FadeInAudio());
        if(scoreManager != null) scoreManager.FadeInMixerGroup(TrackIndex);
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
