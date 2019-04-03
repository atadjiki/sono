using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeElement : MonoBehaviour
{

    public bool startFade = false;
    private bool endFade = false;
    private float zDistance = 90f;

    private Vector3 behind;

    private float maxFrames = 3f;
    private float currentFrames = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //DEBUG ONLY
        Debug.Log("Collided!");
            StartFade();
      
    }

    private void Awake()
    {
        behind = this.transform.position;
        behind.z = Mathf.Abs(zDistance);
    }

    public void StartFade()
    {

        startFade = true;
    }

    private void Update()
    {
        if (startFade && !endFade)
        {

            if (GetComponent<BoxCollider2D>() != null)
            {
                GetComponent<BoxCollider2D>().enabled = false;
            }

            if(currentFrames >= maxFrames)
            {
                this.transform.position = Vector3.Lerp(this.transform.position, behind, Time.deltaTime);
                currentFrames = 0;
            }
            else
            {
                currentFrames++;
            }
               


            if (Vector3.Distance(this.transform.position, behind) <= 1.0f)
            {
                startFade = false;
                endFade = true;
                Debug.Log("Faded: \n" + this.transform.position.ToString() + ", " + behind.ToString());
                this.gameObject.SetActive(false);
            }

        }

    }
  
    }
