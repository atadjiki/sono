using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateTrigger : MonoBehaviour
{
    private AudioSource audioSource;
    private GatePuzzle parent;
    private bool partOfPuzzle = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        parent = GetComponentInParent<GatePuzzle>();

        if (GetComponentInParent<GatePuzzle>() != null)
        {
            Debug.Log(this.name + " - found parent");
            partOfPuzzle = true;
        }


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
                NotifyPuzzle();
            }
            
        }
    }

    private void NotifyPuzzle()
    {
        if (partOfPuzzle)
        {
            GetComponentInParent<GatePuzzle>().GateTriggered(this);
        }
        
    }

    public void PlayAudioClip(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();

    }
}
