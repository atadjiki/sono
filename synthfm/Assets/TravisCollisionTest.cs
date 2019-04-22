using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.VFX;
public class TravisCollisionTest : MonoBehaviour
{
    [SerializeField] private VisualEffect vfx;

    public CircleCollider2D cc;
    // Start is called before the first frame update
    void Start()
    {
        //Physics2D.IgnoreLayerCollision(1, 9);
        Physics2D.IgnoreCollision(GameObject.Find("Player").GetComponent<PolygonCollider2D>(), cc);


    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            //TO DO: For Travis - Instantiate your VFX here!
            vfx.Play();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
        }
    }
}