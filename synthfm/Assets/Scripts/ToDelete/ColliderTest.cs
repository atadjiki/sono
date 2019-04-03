using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderTest : MonoBehaviour
{
    // Start is called before the first frame update
    public Action<string,string,string,string> onObjectCollidedEvent;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(onObjectCollidedEvent != null)
        {
            onObjectCollidedEvent(gameObject.name, gameObject.tag,collision.gameObject.name,collision.gameObject.tag);
        }
    }

    private void Update()
    {
        //Debug.Log(gameObject.transform.position);
    }
}
