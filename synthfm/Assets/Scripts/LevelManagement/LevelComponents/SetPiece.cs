using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/*
 * Represents an important location on the map that the player can encounter
 * Set pieces must have a Cinemachine virtual camera to switch to upon the player encountering them, 
 * as well as a 2D Collider to mark their area. 
 */
[ExecuteInEditMode]
public class SetPiece : MonoBehaviour
{
    public Cinemachine.CinemachineVirtualCamera mainCamera;
    public PlayerInput.TurntableController player;
    public Cinemachine.CinemachineVirtualCamera setPieceCamera;
    public GameObject center;

    private bool centerExists = false;
    // Start is called before the first frame update

    private void OnEnable()
    {
       // if (!Application.isEditor || Application.isPlaying || transform.childCount != 0) { Debug.Log(" Set Piece Initialized "); return; }
      //  DoSetup();
    }

    public void DoSetup()
    {

        //this.transform.position = Vector3.zero;
        if(this.gameObject.GetComponent<BoxCollider2D>() == null && this.gameObject.GetComponent<CircleCollider2D>() == null)
        {
            this.gameObject.AddComponent<CircleCollider2D>();
            this.gameObject.GetComponent<CircleCollider2D>().radius = 75;
            this.gameObject.GetComponent<CircleCollider2D>().isTrigger = true;
            Debug.Log("Already has collider");
        }
        //get player and main camera
        mainCamera = GameObject.Find("CM_Main").GetComponent<Cinemachine.CinemachineVirtualCamera>();
        player = GameObject.Find("Player").GetComponent<PlayerInput.TurntableController>();

        GameObject VCam = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/Cameras/CM_Puzzle"));
        VCam.name = "CM_" + this.name;
        VCam.transform.parent = GameObject.Find("Camera Rig").transform;
        setPieceCamera = VCam.GetComponent<Cinemachine.CinemachineVirtualCamera>();
        setPieceCamera.GetCinemachineComponent<Cinemachine.CinemachineTransposer>().m_FollowOffset = new Vector3(0, 0, -200);
        setPieceCamera.AddCinemachineComponent<Cinemachine.CinemachineBasicMultiChannelPerlin>();
        setPieceCamera.GetCinemachineComponent<Cinemachine.CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0.2f;
        setPieceCamera.GetCinemachineComponent<Cinemachine.CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = 0.2f;

        #if UNITY_EDITOR
            setPieceCamera.GetCinemachineComponent<Cinemachine.CinemachineBasicMultiChannelPerlin>().m_NoiseProfile =
            AssetDatabase.LoadAssetAtPath<Cinemachine.NoiseSettings>("Packages/com.Unity.Cinemachine/Presets/Noise/Handheld_tele_mild.asset");
        #endif

        foreach(Transform child in transform)
        {
            if(child.gameObject.name == "Center")
            {
                centerExists = true;
            }
        }
        if(centerExists == false)
        {
            center = new GameObject();
            center.name = "Center";
            center.transform.parent = this.transform;
            center.transform.position = Vector3.zero;
            setPieceCamera.Follow = center.transform;
            setPieceCamera.LookAt = center.transform;
        }
    }


    public void Initialize()
    {
        OnEnable();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.name);
        Debug.Log(player.gameObject.name);
        if (collision != null && player != null)
        {
            if (collision.gameObject == player.gameObject)
            {
                Debug.Log("Switching camera to " + setPieceCamera.name);
                mainCamera.enabled = false;
                setPieceCamera.enabled = true;
                setPieceCamera.Priority = 20;
            }
           
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision != null && player != null)
        {
            if (collision.gameObject == player.gameObject)
            {
                Debug.Log("Switching camera to " + mainCamera.Name);
                mainCamera.enabled = true;
                setPieceCamera.enabled = false;
                setPieceCamera.Priority = 10;

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
