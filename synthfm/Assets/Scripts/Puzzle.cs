using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class Puzzle : MonoBehaviour
{

    private Cinemachine.CinemachineVirtualCamera mainCamera;
    private TurntableController player;

    public bool complete = false;
    public Cinemachine.CinemachineVirtualCamera puzzleCamera;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GameObject.Find("CM_Main").GetComponent<Cinemachine.CinemachineVirtualCamera>();
        player = GameObject.Find("Player").GetComponent<TurntableController>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    { 

        if (collision.gameObject == player.gameObject)
        {
            Debug.Log("On Trigger Enter");
            mainCamera.enabled = false;
            puzzleCamera.enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
       
        if (collision.gameObject == player.gameObject)
        {
            Debug.Log("On Trigger Close");
            mainCamera.enabled = true;
            puzzleCamera.enabled = false;

        }
    }

    public void SetStatus(bool status)
    {
        complete = status;
    }

    public bool GetStatus()
    {
        return complete;
    }

}
