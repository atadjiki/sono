using UnityEngine;
using System.Collections;

public class LeftFootController : MonoBehaviour {

	public bool shouldPlay = false;

	void Update()
    {
        if(shouldPlay)
		   GetComponent<Animation>().Play();
	    else
		   GetComponent<Animation>().Stop();

    }
}
