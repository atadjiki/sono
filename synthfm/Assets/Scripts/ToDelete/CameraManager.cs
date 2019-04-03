using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public GameObject m_Main;

    // Start is called before the first frame update
    void Start()
    {
        m_Main.GetComponent<Cinemachine.CinemachineVirtualCamera>().Follow = GameObject.FindGameObjectWithTag("Player").transform;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
