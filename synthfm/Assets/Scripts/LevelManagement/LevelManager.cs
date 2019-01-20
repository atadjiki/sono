using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    public GameObject player;
    [HideInInspector]
    public AudioSource playerAudioSource;

    public GameObject fragmentPrefab;
    public AudioClip[] audioFragments;
    public GameObject visualizerRing;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        playerAudioSource = player.GetComponent<AudioSource>();
        playerAudioSource.clip = audioFragments[0];
        playerAudioSource.Play();
    }
}
