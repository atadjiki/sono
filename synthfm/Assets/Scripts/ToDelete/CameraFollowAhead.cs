using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowAhead : MonoBehaviour
{

    private GameObject player;

    public float maxAhead = 20f;
    public float minAhead;
    private float currentAhead = 0f;

    private Vector2 playerPos; 
    private Vector2 playerDirection; 
    private Quaternion playerRotation;

    public int everyNframes = 60;
    private int currentFrame;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        currentFrame = 0;
        minAhead = maxAhead / 5;

    }

    private void Update()
    {

        if(currentFrame == everyNframes)
        {
            playerPos = player.GetComponent<Transform>().position;
            playerDirection = player.GetComponent<Transform>().up;
            playerRotation = player.GetComponent<Transform>().rotation;

            float velocity = player.GetComponent<Rigidbody2D>().velocity.SqrMagnitude();

            //scale the value between min and max ceilings
            currentAhead = velocity / (maxAhead);

            //calculate current value 
            //scale the value between min and max ceilings
            currentAhead = (currentAhead - minAhead) / (maxAhead - minAhead);

            //calculate current value 
            currentAhead = (maxAhead - minAhead) - currentAhead;

            currentAhead *= -1;


        //    currentAhead += Mathf.Abs((playerRotation.z)) * maxAhead;

            this.transform.position = playerPos + playerDirection * currentAhead;
            Debug.Log("Current ahead of player - " + currentAhead);
            Debug.Log("Velocity " + velocity);
            currentFrame = 0;
        }
        else
        {
            currentFrame++;
        }

    }
}
