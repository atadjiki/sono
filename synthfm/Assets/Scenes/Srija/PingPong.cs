using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PingPong : MonoBehaviour
{
    public GameObject pingPongBall;
    public Transform oneEnd, theOtherEnd;
    private float alpha;
    public float pingPongSpeed;
    public float deceleration;
    public float padding;
    public float speed;
    
    // Start is called before the first frame update
    void Start()
    {
        alpha = padding;
        speed = pingPongSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if ( alpha <= (0.0f + padding) )
        {
            //if (speed < 0)
            //    speed += deceleration;
            speed *= -1;
        }
        if( alpha >= (1.0f - padding) )
        {
            //if (speed > 0)
            //    speed -= deceleration;
            speed *= -1;
        }

        /*if( speed > 0 )
            speed = EasingFunctions.EaseInOutQuad(0, pingPongSpeed, alpha);
        if( speed < 0 )
            speed = EasingFunctions.EaseInOutQuad(0, pingPongSpeed, alpha);*/
        alpha += speed * Time.deltaTime;
    }

    void FixedUpdate()
    {
        //pingPongBall.transform.localPosition = Vector3.Lerp(oneEnd.localPosition, theOtherEnd.localPosition, alpha);
        pingPongBall.transform.localPosition = EasingFunctions.EaseInOutQuad(oneEnd.localPosition, theOtherEnd.localPosition, alpha);
    }
}
