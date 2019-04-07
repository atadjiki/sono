using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HubEntryPoint : MonoBehaviour
{
    private HubController hub;

    // Start is called before the first frame update
    void Start()
    {
        hub = GetComponentInParent<HubController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Player entered entry point");
        hub.inEntryPoint = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        hub.inEntryPoint = false;
    }

}
