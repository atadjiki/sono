using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowAhead : MonoBehaviour
{

    private GameObject player;

    public float maxAhead = 20f;
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

    }

    private void Update()
    {

        if(currentFrame == everyNframes)
        {
            playerPos = player.GetComponent<Transform>().position;
            playerDirection = player.GetComponent<Transform>().up;
            playerRotation = player.GetComponent<Transform>().rotation;

            //scale the value between min and max ceilings
            currentAhead = (player.GetComponent<Rigidbody2D>().velocity.sqrMagnitude) / (maxAhead);

            //calculate current value 
            //scale the value between min and max ceilings
            currentAhead = (currentAhead - 0) / (maxAhead - 0);

            //calculate current value 
            currentAhead = (maxAhead - 0) - currentAhead;

            currentAhead *= -1;

            this.transform.position = playerPos + playerDirection * currentAhead;
            Debug.Log("Current ahead of player - " + currentAhead);
            currentFrame = 0;
        }
        else
        {
            currentFrame++;
        }

    }
}
