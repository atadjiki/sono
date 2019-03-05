using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(AudioSource))]
public class ScoreManager : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] scoreTracks;

    private int NoOfAudioTracks = 4;

    AudioSource[] sources;
    public int BPM; //todo: actually implement time signature-based counting

    private AudioClip scoreToPlay;
    public static ScoreManager _instance;

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        else if (_instance != this)
            Destroy(gameObject);

        sources = new AudioSource[NoOfAudioTracks];
        for(int i = 0; i < NoOfAudioTracks; i++)
        {
            sources[i] = gameObject.AddComponent<AudioSource>();
        }

        /*
        float[] audioData1, audioData2;

        float[] theactualdata = new float[44100];
        scoreToPlay = AudioClip.Create("Score", 44100, scoreTracks[0].channels, 44100, false);

        //gameObject.AddComponent<AudioSource>();

        audioData1 = new float[scoreTracks[0].samples * scoreTracks[0].channels];
        scoreTracks[0].GetData(audioData1, 0);

        audioData2 = new float[scoreTracks[1].samples * scoreTracks[1].channels];
        scoreTracks[1].GetData(audioData2, 0);
        //Debug.Log(audioData.Length);
        theactualdata = AddAudioData(audioData1, audioData2);

        //AudioClip newclip = new AudioClip();
        scoreToPlay.SetData(theactualdata, 0);

        GetComponent<AudioSource>().PlayOneShot(scoreToPlay);
        */
    }

    float[] AddAudioData(float[] track1, float[] track2)
    {
        float[] result = new float[ Mathf.Min(track1.Length, track2.Length) ];

        for (int i = 0; i < result.Length; i++)
            result[i] = ClampToValidRange( (track1[i] + track2[i])/2 );

        return result;
    }

    private float ClampToValidRange(float value)
    {
        float min = -1.0f;
        float max = 1.0f;
        return (value < min) ? min : (value > max) ? max : value;
    }

    AudioSource FindFreeAudioSource()
    {
        foreach (var source in sources)
            if (!source.isPlaying)
                return source;
        return null;
    }

    public void StartPlayingClip(int index)
    {
        //todo
        AudioSource yays = FindFreeAudioSource();
        if (yays == null)
        {
            //oops
        }
        else
        {
            yays.loop = true;
            yays.clip = scoreTracks[index];
            yays.Play();
        }

        //also maybe have an overload with a clip name?
    }

    // we probably need coroutines for these, don't we
    public void Crossfade(AudioSource oldSource, AudioSource newSource)
    {

    }

    public void FadeIn(AudioSource source)
    {

    }

    public void FadeOut(AudioSource source)
    {

    }
}
