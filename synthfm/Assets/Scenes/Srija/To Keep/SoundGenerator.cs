using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundGenerator : MonoBehaviour
{
    public AudioClip[] sounds;

    public AudioClip GetRandomSound()
    {
        return sounds[Random.Range(0, sounds.Length)];
    }
}
