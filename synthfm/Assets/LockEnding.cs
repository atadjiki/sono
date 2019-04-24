using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LockEnding : MonoBehaviour
{
    // Start is called before the first frame update
    public bool active = false;
    public float lockTime = 10.0f;
    public PlayerInput.TurntableController player;
    public Navpoint navPoint;
    public GameObject bubbles;
    public GameObject playerTrail;
    public Animator animator;


    public GameObject rbPlayer;

    void Awake()
    {
        if (player == null)
            player = GameObject.Find("Player").GetComponent<PlayerInput.TurntableController>();
        if (navPoint == null)
            navPoint = GameObject.Find("Player").GetComponent<Navpoint>();
        if (rbPlayer == null)
            rbPlayer = GameObject.Find("Player");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(active && collision.gameObject == GameObject.Find("Player"))
        {
            StartCoroutine(Lock());
        }
    }

    IEnumerator Lock()
    {
        Debug.Log("Ending screen " + Time.time + " secs");
        rbPlayer.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        rbPlayer.GetComponent<Rigidbody2D>().simulated = false;
        FXToggle.instance.AllFXOff();


        foreach(FragmentController fragment in GameObject.FindObjectsOfType<FragmentController>())
        {
            fragment.currentState = FragmentController.states.DEPOSIT;
        }

        yield return new WaitForSecondsRealtime(lockTime);

        rbPlayer.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        rbPlayer.GetComponent<Rigidbody2D>().simulated = true;
        // navPoint.Unlock();
        Debug.Log("Title screen finished " + Time.time + " secs");

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
