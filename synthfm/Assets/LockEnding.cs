using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LockEnding : MonoBehaviour
{
    // Start is called before the first frame update
    public bool checkForFragments = false;
    public bool active = true;
    public bool restartLevel = false;
    public float lockTime = 10.0f;
    public float controllerLockDelay = 1.0f;
    public PlayerInput.TurntableController player;
    public GameObject rbPlayer;
    private bool dirty = false;
    public Transform emissive;
    public float emissionRate = 0.05f;
    public Vector3 finalSize = new Vector3(500, 500, 100);

    void Awake()
    {
        if (player == null)
            player = GameObject.Find("Player").GetComponent<PlayerInput.TurntableController>();
        if (rbPlayer == null)
            rbPlayer = GameObject.Find("Player");
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (checkForFragments)
        {
            if (PuzzleProgressManager.instance.isCompletedWithGame())
            {
                return;
            }
        }

        if (!dirty && active)
        {
            if (collision != null && player != null)
            {
                if (collision.gameObject == player.gameObject)
                {
                    dirty = true;
                    StartCoroutine(Lock());

                }

            }


        }
    }

    IEnumerator Lock()
    {
        Debug.Log("Ending screen " + Time.time + " secs");

        this.gameObject.GetComponentInParent<SetPiece>().enabled = false;

        //lock controls
        StartCoroutine(LockControls());

        foreach (FragmentController fragment in GameObject.FindObjectsOfType<FragmentController>())
        {
            fragment.currentState = FragmentController.states.DEPOSIT;
        }

        yield return new WaitForSeconds(3.0f);

        StartCoroutine(LerpScale(emissive));

        yield return new WaitForSecondsRealtime(lockTime);

        StartCoroutine(RestartLevel());


    }

    IEnumerator LockControls()
    {
        yield return new WaitForSeconds(controllerLockDelay);
        rbPlayer.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        rbPlayer.GetComponent<Rigidbody2D>().simulated = false;
    }

    IEnumerator RestartLevel()
    {
        if (restartLevel)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        yield return new WaitForEndOfFrame();

    }

    IEnumerator LerpScale(Transform transform)
    {

        Vector3 startSize = transform.localScale;
        Vector3 targetSize = finalSize;

        // Track how many seconds we've been fading.
        float t = 0;

        while (transform.localScale != targetSize)
        { 

            // Blend to the corresponding opacity between start & target.
            transform.localScale = Vector3.Lerp(transform.localScale, targetSize,emissionRate);

            // Wait one frame, and repeat.
            yield return null;
        }
    }

}

