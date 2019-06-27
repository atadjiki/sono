using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeFiberColorProfiles : MonoBehaviour
{
    private ChangeColor cc;
    // Start is called before the first frame update
    void Start()
    {
        cc = GameObject.Find("Main Camera").GetComponent<ChangeColor>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (gameObject.tag == "Fiber1" && collision.gameObject.tag == "Player")
        {
            StartCoroutine(cc.changeColor(cc.saturated, cc.firstFiberPuzzleColor[2], cc.firstFiberPuzzleColor[3]));
        }
        else if (gameObject.tag == "Fiber2" && collision.gameObject.tag == "Player")
        {
            StartCoroutine(cc.changeColor(cc.saturated, cc.secondFiberPuzzleColor[2], cc.secondFiberPuzzleColor[3]));
        }
        else if (gameObject.tag == "Fiber3" && collision.gameObject.tag == "Player")
        {
            StartCoroutine(cc.changeColor(cc.saturated, cc.thirdFiberPuzzleColor[2], cc.thirdFiberPuzzleColor[3]));
     
        }
    }
}
