using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondCharControlelr : MonoBehaviour
{
    public float speed;             //Floating point variable to store the player's movement speed.

    private Rigidbody2D rb2d;       //Store a reference to the Rigidbody2D component required to use 2D Physics.

    // Use this for initialization
    void Start()
    {
        //Get and store a reference to the Rigidbody2D component so that we can access it.
        rb2d = GetComponent<Rigidbody2D>();
    }

    //FixedUpdate is called at a fixed interval and is independent of frame rate. Put physics code here.
    void FixedUpdate()
    {
        //Store the current horizontal input in the float moveHorizontal.
        float moveHorizontal = Input.GetAxis("HorizontalC");

        //Store the current vertical input in the float moveVertical.
        float moveVertical = Input.GetAxis("VerticalC");

        //Use the two store floats to create a new Vector2 variable movement.
        Vector2 movement = new Vector2(moveHorizontal*speed, moveVertical*speed);

        //Call the AddForce function of our Rigidbody2D rb2d supplying movement multiplied by speed to move our player.
        //rb2d.AddForce(movement * speed);
        rb2d.velocity = movement;
        if (gameObject.transform.position.y > 17f)
        {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, 17f, gameObject.transform.position.z);
        }

        if (gameObject.transform.position.y < -14f)
        {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, -14f, gameObject.transform.position.z);
        }

        if (gameObject.transform.position.x < -41f)
        {
            gameObject.transform.position = new Vector3(-41f, gameObject.transform.position.y, gameObject.transform.position.z);
        }

        if (gameObject.transform.position.x > 42f)
        {
            gameObject.transform.position = new Vector3(42f, gameObject.transform.position.y, gameObject.transform.position.z);
        }

    }
}