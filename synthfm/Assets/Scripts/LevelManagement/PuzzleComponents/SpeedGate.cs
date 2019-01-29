using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedGate : MonoBehaviour
{

    private AudioSource audioSource;
    private Rigidbody2D player;

    public float multiplier;

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Rigidbody2D>();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Vector3 normalizedVelocity = collision.GetComponent<Rigidbody2D>().velocity.normalized;
            float angle = Vector3.Dot(transform.up, normalizedVelocity);
            if (angle > 0.5f)
            {
                //audioSource.clip = AssetManager.instance.gateTones[0];
                //audioSource.Play();

                player.velocity *= multiplier;

                Debug.Log("Speed gate triggered");
            }

        }
    }

}
