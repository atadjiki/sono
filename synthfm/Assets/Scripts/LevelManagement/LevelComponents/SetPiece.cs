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
    private Cinemachine.CinemachineVirtualCamera mainCamera;
    private TurntableController player;
    public Cinemachine.CinemachineVirtualCamera setPieceCamera;
    private static bool initialized = false;
    private GameObject center;
    private GameObject VCam;

    // Start is called before the first frame update

    private void OnEnable()
    {
        if (!Application.isEditor || Application.isPlaying || initialized) { Debug.Log("Initialized " + initialized); return; }

        if (GameObject.Find("Center"))

            Debug.Log("Adding collider");
        this.gameObject.AddComponent<CircleCollider2D>();
        this.gameObject.GetComponent<CircleCollider2D>().radius = 200;
        this.gameObject.GetComponent<CircleCollider2D>().isTrigger = true;


        //get player and main camera
        mainCamera = GameObject.Find("CM_Main").GetComponent<Cinemachine.CinemachineVirtualCamera>();
        player = GameObject.Find("Player").GetComponent<TurntableController>();

        VCam = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/Cameras/CM_Puzzle"));
        VCam.name = "CM_" + this.name;
        VCam.transform.parent = GameObject.Find("Camera Rig").transform;
        setPieceCamera = VCam.GetComponent<Cinemachine.CinemachineVirtualCamera>();
        setPieceCamera.GetCinemachineComponent<Cinemachine.CinemachineTransposer>().m_FollowOffset = new Vector3(0, 0, -200);
        setPieceCamera.AddCinemachineComponent<Cinemachine.CinemachineBasicMultiChannelPerlin>();
        setPieceCamera.GetCinemachineComponent<Cinemachine.CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0.2f;
        setPieceCamera.GetCinemachineComponent<Cinemachine.CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = 0.2f;


        setPieceCamera.GetCinemachineComponent<Cinemachine.CinemachineBasicMultiChannelPerlin>().m_NoiseProfile =
        AssetDatabase.LoadAssetAtPath<Cinemachine.NoiseSettings>("Packages/com.Unity.Cinemachine/Presets/Noise/Handheld_tele_mild.asset");


        GameObject center = new GameObject();
        center.name = "Center";
        center.transform.parent = this.transform;

        setPieceCamera.Follow = center.transform;
        setPieceCamera.LookAt = center.transform;

        initialized = true;
    }

    void Start()
    {

       

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
