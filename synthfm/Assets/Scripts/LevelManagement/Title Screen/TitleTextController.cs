using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleTextController : MonoBehaviour
{
    public Animator anim;
    public float idleTime;

    private void Start()
    {
        StartCoroutine(Idle());
    }

    private IEnumerator Idle()
    {
        Debug.Log("Coroutine Started");
        
        yield return new WaitForSeconds(idleTime);
        Debug.Log("Coroutine Ended");
        anim.SetTrigger("Out");
    }
}
