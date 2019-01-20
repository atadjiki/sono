using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    private float rotationSpeed;
    public float rotationSpeedMin;
    public float rotationSpeedMax;

    private void Start()
    {
        rotationSpeed = Random.Range(rotationSpeedMin, rotationSpeedMax);
    }

    private void Update()
    {
        transform.Rotate(transform.up * Time.deltaTime * rotationSpeed);
    }
}
