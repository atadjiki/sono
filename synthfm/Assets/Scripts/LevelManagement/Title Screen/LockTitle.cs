using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockTitle : MonoBehaviour
{
    // Start is called before the first frame update

    public float lockTime = 10.0f;
    public PlayerInput.TurntableController player;
    public Navpoint navPoint;
    public GameObject bubbles;
    public GameObject playerTrail;


    public GameObject rbPlayer;

    void Awake()
    {
        if (player == null)
            player = GameObject.Find("Player").GetComponent<PlayerInput.TurntableController>();
        if (navPoint == null)
            navPoint = GameObject.Find("Player").GetComponent<Navpoint>();
        if (rbPlayer == null)
            rbPlayer = GameObject.Find("Player");

        rbPlayer.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        rbPlayer.GetComponent<Rigidbody2D>().simulated = false;
        FXToggle.instance.AllFXOff();
       StartCoroutine(Lock());

        
    }

    IEnumerator Lock()
    {
        Debug.Log("Title screen " + Time.time + " secs");
        //navPoint.active = false;
        yield return new WaitForSecondsRealtime(lockTime);

        rbPlayer.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        rbPlayer.GetComponent<Rigidbody2D>().simulated = true;
       // navPoint.Unlock();
        Debug.Log("Title screen finished " + Time.time + " secs");
        bubbles.SetActive(true);
        playerTrail.SetActive(true);
        FXToggle.instance.ToggleFX(FragmentController.world.AMBER);

       // yield return new WaitForSecondsRealtime(2f);

      //  GameObject.Find("TitleSequence").GetComponent<SetPiece>().enabled = false;
       // Destroy(GameObject.Find("CM_TitleSequence"));
    }
}
