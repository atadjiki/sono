using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HubBounds : MonoBehaviour
{

    private HubController hub;

    // Start is called before the first frame update
    void Start()
    {
        hub = GetComponentInParent<HubController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        hub.insideBounds = true;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        hub.insideBounds = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        hub.insideBounds = false;
    }
}
