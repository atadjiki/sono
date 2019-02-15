using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * Represents an important location on the map that the player can encounter
 * Set pieces must have a Cinemachine virtual camera to switch to upon the player encountering them, 
 * as well as a 2D Collider to mark their area. 
 */ 
public class SetPiece : MonoBehaviour
{
    private Cinemachine.CinemachineVirtualCamera mainCamera;
    private TurntableController player;
    public Cinemachine.CinemachineVirtualCamera setPieceCamera;

    // Start is called before the first frame update
    void Start()
    { 
        //get player and main camera
        mainCamera = GameObject.Find("CM_Main").GetComponent<Cinemachine.CinemachineVirtualCamera>();
       
        player = GameObject.Find("Player").GetComponent<TurntableController>();
       

        if (setPieceCamera == null)
        {
            setPieceCamera = mainCamera;
        }
        else
        {
            setPieceCamera.enabled = false;
        }

        

    }


    public void Initialize()
    {
        Start();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (!(collision is null) && !(player is null))
        {
            if (collision.gameObject == player.gameObject)
            {
                Debug.Log("Switching camera to " + setPieceCamera.name);
                mainCamera.enabled = false;
                setPieceCamera.enabled = true;
            }
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!(collision is null) && !(player is null))
        {
            if (collision.gameObject == player.gameObject)
            {
                Debug.Log("Switching camera to " + mainCamera.Name);
                mainCamera.enabled = true;
                setPieceCamera.enabled = false;

            }
        }
        
    }

    public void TriggerEnter(Collider2D collision)
    {
        OnTriggerEnter2D(collision);
    }

    public void TriggerExit(Collider2D collision)
    {
        OnTriggerExit2D(collision);
    }

    public Cinemachine.CinemachineVirtualCamera getMainCamera()
    {
        return mainCamera;
    }

}
