using UnityEngine;
using System.Collections;

public class RightFootController : MonoBehaviour {

	public bool shouldPlay = false;

	void Update()
    {
    if(shouldPlay)
		GetComponent<Animation>().Play();
	else
		GetComponent<Animation>().Stop();
    }
}
