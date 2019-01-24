using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPiece : MonoBehaviour
{
    private Cinemachine.CinemachineVirtualCamera mainCamera;
    private TurntableController player;
    public Cinemachine.CinemachineVirtualCamera setPieceCamera;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GameObject.Find("CM_Main").GetComponent<Cinemachine.CinemachineVirtualCamera>();
        player = GameObject.Find("Player").GetComponent<TurntableController>();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject == player.gameObject)
        {
            Debug.Log("Switching camera to " + setPieceCamera.name);
            mainCamera.enabled = false;
            setPieceCamera.enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.gameObject == player.gameObject)
        {
            Debug.Log("Switching camera to " + mainCamera.Name);
            mainCamera.enabled = true;
            setPieceCamera.enabled = false;

        }
    }

}
