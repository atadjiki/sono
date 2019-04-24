using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupScript : MonoBehaviour
{
    public Animator Pickup;
    public CircleCollider2D cc;
    // Start is called before the first frame update
    void Start()
    {
        foreach (Collider2D collider in GetComponentsInChildren<Collider2D>())
        {
            Physics2D.IgnoreCollision(collider, cc);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)

    {
        if(collision.gameObject == GameObject.Find("Player"))
        {
            Pickup.SetTrigger("Switch");
        }
           
    }
}
