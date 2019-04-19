using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PatternTrigger : MonoBehaviour
{
    public float cmrFieldView;
    public float timeOut;
    

    GameObject playerRef;
    float PrevAcceleration;
    PlayerInput.TurntableController tController;
    Cinemachine.CinemachineVirtualCamera mainCamera;
    float prevFieldofView;

    // Start is called before the first frame update
    void Start()
    {
        playerRef = GameObject.Find("Player");
        tController = playerRef.GetComponent<PlayerInput.TurntableController>();
        mainCamera = GameObject.Find("CM_Main").GetComponent<Cinemachine.CinemachineVirtualCamera>();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject == GameObject.Find("Player"))
        {
            // change speed
            Debug.Log("Starting sequence");

            PrevAcceleration = tController.acceleration;
            tController.acceleration = 2;

            // change camera
            prevFieldofView = mainCamera.m_Lens.FieldOfView;
           // mainCamera.m_Lens.FieldOfView = cmrFieldView;
           // var transposer =  mainCamera.GetCinemachineComponent<CinemachineTransposer>();
           // transposer.m_FollowOffset.Set(0,-35,-75);

            // change fragment state and start pattern
            FragmentController[] fragments = GameObject.FindObjectsOfType<FragmentController>();
            foreach (FragmentController fragment in fragments)
            {
                if (fragment.currentState == FragmentController.states.FLEE)
                {
                    if (fragment.TrackIndex != 1) // temporary
                    {
                        fragment.currentState = FragmentController.states.FINAL_PATERN;
                    }
                    Debug.Log("generating patterns");

                }

            }


            // start coroutine and set everything back to prev
            StartCoroutine(restoreState());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator restoreState()
    {
        yield return new WaitForSeconds(timeOut);
        Debug.Log("Restoring");
        tController.acceleration = PrevAcceleration;
        StartCoroutine(resetCamera());

        FragmentController[] fragments = GameObject.FindObjectsOfType<FragmentController>();
        foreach (FragmentController fragment in fragments)
        {
            if (fragment.currentState == FragmentController.states.FINAL_PATERN)
            {
                if (fragment.TrackIndex != 1) // temporary
                {
                    fragment.currentState = FragmentController.states.FOLLOW;
                    TrailRenderer tr = fragment.gameObject.transform.Find("Trail").GetComponent<TrailRenderer>();
                    tr.time = 8;
                }
                Debug.Log("Done generating patterns");

            }

        }
    }

    IEnumerator resetCamera()
    {
        yield return new WaitForSeconds(5);
       // mainCamera.m_Lens.FieldOfView = prevFieldofView;
    }
}
