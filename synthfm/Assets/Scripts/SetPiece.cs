using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPiece : MonoBehaviour
{
    private Cinemachine.CinemachineVirtualCamera mainCamera;
    private TurntableController player;
    public Cinemachine.CinemachineVirtualCamera camera;

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
            Debug.Log("On Trigger Enter");
            mainCamera.enabled = false;
            camera.enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.gameObject == player.gameObject)
        {
            Debug.Log("On Trigger Close");
            mainCamera.enabled = true;
            camera.enabled = false;

        }
    }

}
