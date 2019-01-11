using UnityEngine;
using System.Collections;

public class WallController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter(Collision hit)
    {
        //scale hit positions against the 15 columns in front of the wall
        int columnNumber = Mathf.RoundToInt(((hit.transform.position.x + 8.5f) / 16f) * 15f);

        GameObject gObj = GameObject.Find("HarmonicCube (" + columnNumber + ")");
        if (gObj != null)
        {
            ColumnController col = gObj.GetComponent<ColumnController>();
            gObj.transform.position = new Vector3(gObj.transform.position.x, hit.transform.position.y - 5, gObj.transform.position.z);
            col.setAmplitude(hit.transform.position.y / 50f);
        }

        if (hit.gameObject.name.Contains("Missile"))
            Destroy(hit.gameObject);


    }
}
