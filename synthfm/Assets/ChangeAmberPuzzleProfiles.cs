using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeAmberPuzzleProfiles : MonoBehaviour
{
    private ChangeColor cc;
    // Start is called before the first frame update
    void Start()
    {
        cc = GameObject.Find("Main Camera").GetComponent<ChangeColor>();
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(gameObject.tag == "Amber1" && collision.gameObject.tag == "Player")
        {
            StartCoroutine(cc.changeColor(cc.dark, cc.firstamberPuzzleColor[2], cc.firstamberPuzzleColor[3]));
        }
        else if (gameObject.tag == "Amber2" && collision.gameObject.tag == "Player")
        {
            StartCoroutine(cc.changeColor(cc.dark, cc.secondamberPuzzleColor[2], cc.secondamberPuzzleColor[3]));
        }
        else if (gameObject.tag == "Amber3" && collision.gameObject.tag == "Player")
        {
            StartCoroutine(cc.changeColor(cc.dark, cc.thirdamberPuzzleColor[2], cc.thirdamberPuzzleColor[3]));
        }
    }
}
