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

    public SpriteRenderer top;
    public SpriteRenderer bottom;

    public GameObject restartPanel;

    private bool waitingForRestart = false;

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
            if (!PuzzleProgressManager.instance.isCompletedWithGame())
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

        yield return new WaitForSeconds(4.0f);

        StartCoroutine(LerpScale(emissive));
        StartCoroutine(FadeTo(top));
        StartCoroutine(FadeTo(bottom));

        yield return new WaitForSeconds(4.0f);

        ShowCreditsPanel();

        yield return new WaitForSecondsRealtime(lockTime);
        
    }

    void ShowCreditsPanel()
    {
        waitingForRestart = true;
        restartPanel.SetActive(true);
    }

    IEnumerator LockControls()
    {
        yield return new WaitForSeconds(controllerLockDelay);
        rbPlayer.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        rbPlayer.GetComponent<Rigidbody2D>().simulated = false;
    }

    IEnumerator FadeTo(SpriteRenderer renderer)
    {

        // Cache the current color of the material, and its initiql opacity.
        Color color = renderer.color;
        float startOpacity = color.a;
        float targetOpacity = 0.0f;
        float duration =5.0f;

        // Track how many seconds we've been fading.
        float t = 0;

        while (t < duration)
        {
            // Step the fade forward one frame.
            t += Time.deltaTime;
            // Turn the time into an interpolation factor between 0 and 1.
            float blend = Mathf.Clamp01(t / duration);

            // Blend to the corresponding opacity between start & target.
            color.a = Mathf.Lerp(startOpacity, targetOpacity, blend);

            // Apply the resulting color to the material.
            renderer.color = color;

            // Wait one frame, and repeat.
            yield return null;
        }
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

    private void Update()
    {
        if (waitingForRestart)
        {
            if(GameObject.Find("Player").GetComponent<PlayerInput.TurntableController>().turntableManager.lastInteracted == PlayerInput.TurntableManager.DJTechControl.Play)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }else if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }

}

