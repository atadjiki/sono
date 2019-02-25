using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boost : MonoBehaviour
{
    private Rigidbody2D playerRigidBody;
    private Transform playerTransform;
    [SerializeField]
    private float accelerationBoost = 1.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        playerRigidBody = player.GetComponent<Rigidbody2D>();
        playerTransform = player.transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        playerRigidBody.AddForce((transform.up) * accelerationBoost);
        //playerRigidBody.velocity
        //Debug.Log("trigger");
    }
}
