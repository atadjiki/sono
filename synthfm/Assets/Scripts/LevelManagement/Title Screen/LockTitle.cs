using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockTitle : MonoBehaviour
{
    // Start is called before the first frame update

    public float lockTime = 10.0f;
    public TurntableController player;
    public Navpoint navPoint;

    void Awake()
    {
        StartCoroutine(Lock());

    }

    IEnumerator Lock()
    {
        Debug.Log(Time.time);
        player.enabled = false;
        navPoint.active = false;
        yield return new WaitForSecondsRealtime(lockTime);
        player.enabled = true;
        navPoint.active = true;
        Debug.Log(Time.time);
    }
}
