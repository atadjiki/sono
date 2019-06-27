using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeLatteColorProfiles : MonoBehaviour
{
    private ChangeColor cc;
    // Start is called before the first frame update
    void Start()
    {
        cc = GameObject.Find("Main Camera").GetComponent<ChangeColor>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (gameObject.tag == "Latte1" && collision.gameObject.tag == "Player")
        {
            StartCoroutine(cc.changeColor(cc.dark, cc.firstlattePuzzleColor[2], cc.firstlattePuzzleColor[3]));
        }
        else if (gameObject.tag == "Latte2" && collision.gameObject.tag == "Player")
        {
            StartCoroutine(cc.changeColor(cc.saturated, cc.secondlattePuzzleColor[2], cc.secondlattePuzzleColor[3]));
        }
        else if (gameObject.tag == "Latte3" && collision.gameObject.tag == "Player")
        {
            StartCoroutine(cc.changeColor(cc.saturated, cc.thirdlattePuzzleColor[2], cc.thirdlattePuzzleColor[3]));

        }
    }
}
