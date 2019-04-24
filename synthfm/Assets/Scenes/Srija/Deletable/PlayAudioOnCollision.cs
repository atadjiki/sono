using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudioOnCollision : MonoBehaviour
{
    AudioSource source;
    public AudioClip[] SoundsToPlay;
    public SoundGenerator soundGenerator;
    
    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        if (source == null)
            Debug.LogWarning("Did not find an Audio Source component on this GameObject. This script will probably die.");

        if (SoundsToPlay.Length != 0)
            return; //We're done here.
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (source.isPlaying)
            return;
        if (collision.gameObject.tag == "Player")
        {
            int i = Random.Range(0, SoundsToPlay.Length);
            source.PlayOneShot(SoundsToPlay[i], 1.0f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (source.isPlaying)
            return;
        if (collision.gameObject.tag == "Player")
        {
            int i = Random.Range(0, SoundsToPlay.Length);
            source.PlayOneShot(SoundsToPlay[i], 1.0f);
        }

    }

}
