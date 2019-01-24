using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateTrigger : MonoBehaviour
{
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Vector3 normalizedVelocity = collision.GetComponent<Rigidbody2D>().velocity.normalized;
            float angle = Vector3.Dot(transform.up, normalizedVelocity);
            if(angle > 0.5f)
            {
                //audioSource.clip = AssetManager.instance.gateTones[0];
                audioSource.Play();
            }
        }
    }
}
