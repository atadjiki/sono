using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    private float rotationSpeed;
    public float rotationSpeedMin;
    public float rotationSpeedMax;
    public bool onZ;

    private void Start()
    {
        rotationSpeed = Random.Range(rotationSpeedMin, rotationSpeedMax);
    }

    private void Update()
    {
        if(onZ)
            transform.Rotate(transform.forward * Time.deltaTime * rotationSpeed);
        else
            transform.Rotate(transform.up * Time.deltaTime * rotationSpeed);
    }
}
