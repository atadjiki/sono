using UnityEngine;
using System.Collections;

public class PlayerController2 : MonoBehaviour {

    public GameObject missile;
    float shootForce = 4;
	// Use this for initialization
	void Start () {
	
	}

    // Update is called once per frame
    void Update()
    {

        if (Input.GetButtonUp("Jump"))
        {
            Vector3 pos = new Vector3(transform.position.x,
                          transform.position.y + .25f,
                          transform.position.z + 0.5f);
            Vector3 newPos = transform.forward;
            newPos.y = newPos.y + 1;
            GameObject bulletClone = (GameObject)Instantiate(missile, pos, Quaternion.identity);
            bulletClone.gameObject.name = "Missile(Clone)";
            //missile.GetComponent<Rigidbody>().AddForce(transform.forward * missileVelocity, ForceMode.Impulse);
            bulletClone.GetComponent<Rigidbody>().AddForce(newPos * shootForce, ForceMode.Impulse);
            shootForce = 6;
        }
        if (Input.GetButton("Jump"))
        {
            shootForce += .4f;
        }

        transform.Rotate(new Vector3(0, Input.GetAxis("Horizontal") * 50 * Time.deltaTime, 0));
    }
}
