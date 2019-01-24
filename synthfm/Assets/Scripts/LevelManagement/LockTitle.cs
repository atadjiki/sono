using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockTitle : MonoBehaviour
{
    // Start is called before the first frame update

    public float lockTime = 10.0f;
    public TurntableController player;

    void Start()
    {
        StartCoroutine(Lock());
    }

    IEnumerator Lock()
    {
        Debug.Log(Time.time);
        player.enabled = false;
        yield return new WaitForSecondsRealtime(lockTime);
        player.enabled = true;
        Debug.Log(Time.time);
    }
}
