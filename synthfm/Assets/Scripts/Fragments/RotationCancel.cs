using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationCancel : MonoBehaviour
{
    void Update()
    {
        transform.rotation = Quaternion.identity;
    }
}
