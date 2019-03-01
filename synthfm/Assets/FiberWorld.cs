using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiberWorld : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("FiberSpawner").SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
