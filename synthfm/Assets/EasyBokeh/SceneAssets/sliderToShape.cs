using UnityEngine;
using System.Collections;

public class sliderToShape : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Shaper(float f){
	
		Camera.main.GetComponent<EasyBokeh> ()._shape = (int)f;
	
	}
}
