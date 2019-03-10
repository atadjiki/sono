using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockTitle : MonoBehaviour
{
    // Start is called before the first frame update

    public float lockTime = 10.0f;
    public PlayerInput.TurntableController player;
    //public Navpoint navPoint;

    [SerializeField] private GameObject rbPlayer;

    void Awake()
    {
        rbPlayer.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        rbPlayer.GetComponent<Rigidbody2D>().simulated = false;
       StartCoroutine(Lock());

    }

    IEnumerator Lock()
    {
        Debug.Log("Title screen " + Time.time + " secs");
        //navPoint.active = false;
        yield return new WaitForSecondsRealtime(lockTime);
        rbPlayer.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        rbPlayer.GetComponent<Rigidbody2D>().simulated = true;
       // navPoint.active = true;
        Debug.Log("Title screen finished " + Time.time + " secs");
    }
}
