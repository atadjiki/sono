using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boost : MonoBehaviour
{
    private Rigidbody2D playerRigidBody;
    private TurntableController controller;
    private Transform playerTransform;
    private float oldAccel;
    [SerializeField]
    private float accelerationBoost = 1.0f;

    [SerializeField]
    private float BoostHoldTime = 1.0f;
    private float boostHoldTimer;
    
    // Start is called before the first frame update
    void Start()
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        playerRigidBody = player.GetComponent<Rigidbody2D>();
        playerTransform = player.transform;
        controller = player.GetComponent<TurntableController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        oldAccel = controller.acceleration;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //playerRigidBody.AddForce((transform.up) * accelerationBoost);
        controller.acceleration += accelerationBoost;
        //playerRigidBody.velocity
        //Debug.Log("trigger");
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //oldAccel_mod = controller.AccelerationModifier;
        //controller.AccelerationModifier = ;
        StartCoroutine(deccelerateOnExit());
    }

    IEnumerator deccelerateOnExit()
    {
        boostHoldTimer = 0;
        while(boostHoldTimer <= BoostHoldTime)
        {
            boostHoldTimer += Time.deltaTime;
            controller.acceleration = EasingFunctions.EaseOutQuint(controller.acceleration, oldAccel, boostHoldTimer / BoostHoldTime);
            yield return null;
        }
        yield return null;
    }
}
