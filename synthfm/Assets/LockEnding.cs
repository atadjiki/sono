using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LockEnding : MonoBehaviour
{
    // Start is called before the first frame update
    public bool active = true;
    public bool restartLevel = false;
    public float lockTime = 10.0f;
    public float controllerLockDelay = 1.0f;
    public PlayerInput.TurntableController player;
    public GameObject rbPlayer;
    private bool dirty = false;

    public Cinemachine.CinemachineVirtualCamera mainCamera;
    public Cinemachine.CinemachineVirtualCamera setPieceCamera;

    void Awake()
    {
        if (player == null)
            player = GameObject.Find("Player").GetComponent<PlayerInput.TurntableController>();
        if (rbPlayer == null)
            rbPlayer = GameObject.Find("Player");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!dirty && active)
        {
            if (collision != null && player != null)
            {
                if (collision.gameObject == player.gameObject)
                {
                    if (mainCamera != null && setPieceCamera != null)
                    {
                        Debug.Log("Switching camera to " + setPieceCamera.name);
                        mainCamera.enabled = false;
                        setPieceCamera.enabled = true;
                        setPieceCamera.Priority = 20;
                    }

                }

            }

            dirty = true;
            StartCoroutine(Lock());
        }
    }

    IEnumerator Lock()
    {
        Debug.Log("Ending screen " + Time.time + " secs");

        StartCoroutine(LockControls());
        
        foreach (FragmentController fragment in GameObject.FindObjectsOfType<FragmentController>())
        {
            fragment.currentState = FragmentController.states.DEPOSIT;
        }

        yield return new WaitForSecondsRealtime(lockTime);

        rbPlayer.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        rbPlayer.GetComponent<Rigidbody2D>().simulated = true;
        Debug.Log("Ending screen finished " + Time.time + " secs");

        if (restartLevel)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
       
    }

    IEnumerator LockControls()
    {
        yield return new WaitForSeconds(controllerLockDelay);
        rbPlayer.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        rbPlayer.GetComponent<Rigidbody2D>().simulated = false;
        FXToggle.instance.AllFXOff();
    }

}

