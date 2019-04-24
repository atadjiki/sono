using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enableGatePuzzle : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<GatePuzzle>().enabled = true;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
    }
}
