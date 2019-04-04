using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Persist : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        SetupFiberPuzzles();
        SetupFiberFragments();
    }

    private void SetupFiberPuzzles()
    {

    }

    private void SetupFiberFragments()
    {

    }
}
