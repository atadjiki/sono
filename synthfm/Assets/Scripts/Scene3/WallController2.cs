using UnityEngine;
using System.Collections;

public class WallController2 : MonoBehaviour {

    public GameObject playerObject;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter(Collision hit)
    {
        //scale hit positions against the 15 columns in front of the wall
        PlayerController3 player = playerObject.GetComponent<PlayerController3>();
        int columnNumber = (int)CsoundUnity.Remap(hit.transform.position.x, -8f, 8f, 0f, 63f);
        player.eqSettings[columnNumber].gameObject.transform.position = new Vector3(player.eqSettings[columnNumber].transform.position.x, hit.transform.position.y - 10, player.eqSettings[columnNumber].transform.position.z);

        player.setAmpForBand(columnNumber, CsoundUnity.Remap(hit.transform.position.y, 0f, 10f, 0f, 1f));
        
        if (hit.gameObject.name.Contains("Missile"))
            Destroy(hit.gameObject);


    }
}
