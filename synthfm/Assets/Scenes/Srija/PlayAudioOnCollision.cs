using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudioOnCollision : MonoBehaviour
{
    AudioSource source;
    public AudioClip SoundToPlay;
    public SoundGenerator soundGenerator;
    
    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        if (source == null)
            Debug.LogWarning("Did not find an Audio Source component on this GameObject. This script will probably die.");

        if (SoundToPlay != null)
            return; //We're done here.

        // if not, we'll need to pick up a sound from the Sound Generator. We can either define one beforehand or find the (hopefully only) one in the scene.
        // If we don't have one already
        if (soundGenerator == null)
        {
            soundGenerator = FindObjectOfType<SoundGenerator>();
            if (soundGenerator == null) //whoops guess no sound then
            {
                Debug.LogWarning("Can't find a sound to play. No Sound Generator available. I hope you like the silence.");
                return;
            }
        }
        // yayy we got one
        SoundToPlay = soundGenerator.GetRandomSound();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (source.isPlaying)
            return;
        if(collision.gameObject.tag == "Player")
            source.PlayOneShot(SoundToPlay, Random.Range(0.3f, 0.6f));
    }

}
