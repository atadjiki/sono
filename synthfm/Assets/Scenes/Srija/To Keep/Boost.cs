using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boost : MonoBehaviour
{
    private Rigidbody2D playerRigidBody;
    private PlayerInput.TurntableController controller;
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
        controller = player.GetComponent<PlayerInput.TurntableController>();
    }

    // Update is called once per frame
    void Update()
    {
        //it'd be nice if we could localize the acceleration assignment to here. 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        oldAccel = controller.acceleration;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        controller.acceleration += accelerationBoost;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
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
