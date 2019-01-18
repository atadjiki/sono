using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class SetPiece : MonoBehaviour
{

    public Cinemachine.CinemachineVirtualCamera mainCamera;
    public Cinemachine.CinemachineVirtualCamera setCamera;

    public TurntableController player;

    // Start is called before the first frame update
    void Start()
    {
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
            setCamera.enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
       
        if (collision.gameObject == player.gameObject)
        {
            Debug.Log("On Trigger Close");
            mainCamera.enabled = true;
            setCamera.enabled = false;

        }
    }

}
