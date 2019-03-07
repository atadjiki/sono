using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Mixer
{
    public AudioMixer mixer;
    [HideInInspector]
    public int NumberOfAudioTracks;
    [HideInInspector]
    public AudioSource[] sources;

    public void CreateSources(GameObject gameObject)
    {
        sources = new AudioSource[NumberOfAudioTracks];
        for (int i = 0; i < NumberOfAudioTracks; i++)
        {
            sources[i] = gameObject.AddComponent<AudioSource>();
            sources[i].loop = true;
            sources[i].playOnAwake = false;
            sources[i].outputAudioMixerGroup = mixer.FindMatchingGroups("Master")[0]; //todo: assign to different mixer groups
        }

        Debug.Assert(sources.Length == NumberOfAudioTracks);
    }

    public void Load(ScorePattern scorePattern)
    {
        Debug.Assert(scorePattern.clips.Length == sources.Length);
        for (int i = 0; i < sources.Length; i++)
            sources[i].clip = scorePattern.clips[i];
    }

    public void Unload()
    {
        for (int i = 0; i < sources.Length; i++)
            sources[i].clip = null;
    }

    public void Play()
    {
        for (int i = 0; i < sources.Length; i++)
            sources[i].Play();
    }

    public void Pause()
    {
        for (int i = 0; i < sources.Length; i++)
            sources[i].Pause();
    }
}

// TODO: need to add parameters to mixer groups to control them
public class ScoreManager : MonoBehaviour
{
    [Header("Score Patterns")]
    public ScorePattern[] scorePatterns;

    [Header("Docks")]
    public Mixer[] docks;
    private int CurrentActiveDock;

    [SerializeField]
    private int NoOfAudioTracksPerDock = 3;

    [Header("Metronome")]
    [Header("BPM")]
    public double bpm; //todo: actually implement time signature-based counting
    [Header("Time Signature")]
    public int Numerator;
    public int Denominator;


    double nextTick = 0.0F; // The next tick in dspTime
    double sampleRate = 0.0F;
    bool ticked = false;

    private double DefaultCrossfadeTime = 0.5f;

    public static ScoreManager _instance;

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        else if (_instance != this)
            Destroy(gameObject);

        double startTick = AudioSettings.dspTime;
        sampleRate = AudioSettings.outputSampleRate;


        Debug.Assert(bpm != 0);
        nextTick = startTick + (60.0 / bpm);

        for (int i = 0; i < docks.Length; i++)
        {
            docks[i].NumberOfAudioTracks = NoOfAudioTracksPerDock;
            docks[i].CreateSources(gameObject);
        }
    }

    private void Start()
    {
        // Setting up our initial state
        LoadPattern(0, 0);
        LoadPattern(1);
        Play(0);
    }

    // this is coroutine hell
    public void Crossfade()
    {
        int OtherDock = (CurrentActiveDock + 1) % docks.Length;
        Play(OtherDock);
        FadeOut(CurrentActiveDock);
        FadeIn(OtherDock);
        StartCoroutine(UnloadDockAfterSeconds(CurrentActiveDock, OtherDock));
    }

    public void CrossfadeOnNextTick()
    {
        StartCoroutine(CrossFadeOnNextTick());
    }

    IEnumerator UnloadDockAfterSeconds(int DockToUnload, int newCurrentDock)
    {
        float timer = 0;
        while(timer <= DefaultCrossfadeTime)
        {
            timer += Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        docks[DockToUnload].Pause();
        //docks[DockToUnload].Unload();
        CurrentActiveDock = newCurrentDock;
    }

    IEnumerator CrossFadeOnNextTick()
    {
        while (!ticked)
            yield return new WaitForSeconds(Time.deltaTime);
        Debug.Log("Crossfading now");
        int OtherDock = (CurrentActiveDock + 1) % docks.Length;
        Play(OtherDock);
        FadeOut(CurrentActiveDock);
        FadeIn(OtherDock);
        StartCoroutine(UnloadDockAfterSeconds(CurrentActiveDock, OtherDock));
    }

    public void FadeIn(int DockIndex)
    {

        AudioMixerSnapshot[] mixerSnapshots = { docks[DockIndex].mixer.FindSnapshot("On") };
        float[] mixerWeights = { 1 };
        docks[DockIndex].mixer.TransitionToSnapshots(mixerSnapshots, mixerWeights, (float)DefaultCrossfadeTime);
    }

    public void FadeOut(int DockIndex)
    {

        AudioMixerSnapshot[] mixerSnapshots = { docks[DockIndex].mixer.FindSnapshot("Off") };
        float[] mixerWeights = { 1 };
        docks[DockIndex].mixer.TransitionToSnapshots(mixerSnapshots, mixerWeights, (float)DefaultCrossfadeTime);
    }

    public void Play(int DockIndex)
    {
        docks[DockIndex].Play();
    }


    //These are all different ways you can load the next pattern into the other dock
    public void LoadPattern(int Index)
    {
        int DockIndex = (CurrentActiveDock + 1) % docks.Length;
        LoadPattern(Index, DockIndex);
    }

    public void LoadPattern(ScorePattern pattern)
    {
        int DockIndex = (CurrentActiveDock + 1) % docks.Length;
        LoadPattern(pattern, DockIndex);
    }

    public void LoadPattern(ScorePattern pattern, int DockIndex)
    {
        docks[DockIndex].Load(pattern);
    }

    public void LoadPattern(int Index, int DockIndex)
    {
        docks[DockIndex].Load(scorePatterns[Index]);
    }


    // The Update functions mostly just handle updating the metronome
    void FixedUpdate()
    {
        double timePerTick = 60.0f / bpm;
        DefaultCrossfadeTime = timePerTick;
        double dspTime = AudioSettings.dspTime;

        while (dspTime >= nextTick)
        {
            ticked = false;
            nextTick += timePerTick;
        }

    }

    //yup. Still just the metronome.
    void LateUpdate()
    {
        if (!ticked && nextTick >= AudioSettings.dspTime)
        {
            ticked = true;
            //BroadcastMessage("OnTick");
            //Debug.Log("Tick");
        }
    }

    /* This code might come in handy for manual mixing
    float[] AddAudioData(float[] track1, float[] track2)
    {
        float[] result = new float[Mathf.Min(track1.Length, track2.Length)];

        for (int i = 0; i < result.Length; i++)
            result[i] = ClampToValidRange((track1[i] + track2[i]) / 2);

        return result;
    }

    private float ClampToValidRange(float value)
    {
        float min = -1.0f;
        float max = 1.0f;
        return (value < min) ? min : (value > max) ? max : value;
    }
    */
}
