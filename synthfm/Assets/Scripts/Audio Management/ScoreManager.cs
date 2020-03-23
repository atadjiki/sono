using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Mixer
{
    public AudioMixer mixer;
    public ScorePattern currentScorepattern;
    [HideInInspector] public int NumberOfAudioTracks;
    [HideInInspector] public AudioSource[] sources;
    [HideInInspector] public float maxVolume;
    AudioMixerGroup[] fragmentMixerGroups;
    public float fadeTime = 0.5f;

    public void CreateSources(GameObject gameObject)
    {
        fragmentMixerGroups = mixer.FindMatchingGroups("Fragment ");
        sources = new AudioSource[NumberOfAudioTracks];
        for (int i = 0; i < NumberOfAudioTracks; i++)
        {
            sources[i] = gameObject.AddComponent<AudioSource>();
            sources[i].loop = true;
            sources[i].playOnAwake = false;
            if (i == 0)
                sources[i].outputAudioMixerGroup = mixer.FindMatchingGroups("Main")[0];
            else
                sources[i].outputAudioMixerGroup = fragmentMixerGroups[i-1];
        }

        Debug.Assert(sources.Length == NumberOfAudioTracks);
    }

    public void Load(ScorePattern scorePattern, bool AllTracksActive)
    {
        Debug.Assert(scorePattern.clips.Length == sources.Length);
        Debug.Log("Loading " + scorePattern.name + "  -- " +  AllTracksActive);
        currentScorepattern = scorePattern;
        for (int i = 0; i < sources.Length; i++)
        {
            sources[i].clip = scorePattern.clips[i];
            if(AllTracksActive && i != 0)
                SetVolume(i, 0);
        }

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

    public void SetVolume(int mixerGroupIndex, float newVolume)
    {
        mixer.SetFloat("Vol Fragment " + mixerGroupIndex, newVolume);
    }

    public void SetMasterVolume(float newVolume)
    {
        mixer.SetFloat("Vol Master", newVolume);
    }

    public IEnumerator FadeOutMixerGroup(int mixerGroupIndex)
    {
        float timer = 0; //-80dB to 0dB
        while(timer <= fadeTime)
        {
            mixer.SetFloat("Vol Fragment " + mixerGroupIndex, Mathf.Lerp(maxVolume, -80.0f, timer / fadeTime));
            timer += Time.deltaTime;
            yield return null;
        }
        mixer.SetFloat("Vol Fragment " + mixerGroupIndex, -80);
        yield return null;
    }

    public IEnumerator FadeOutMasterMixer()
    {
        float timer = 0; //-80dB to 0dB
        while (timer <= fadeTime)
        {
            float newVol = Mathf.Lerp(maxVolume, -80f, (timer / fadeTime));
            //Debug.Log((timer / fadeTime));
            mixer.SetFloat("Vol Master", newVol);
           // mixer.GetFloat("Vol Master", out newVol);
            //Debug.Log(newVol);
            timer += Time.deltaTime;
            yield return null;
        }
        mixer.SetFloat("Vol Master", -80);
        yield return null;
    }

    public IEnumerator FadeInMixerGroup(int mixerGroupIndex)
    {
        float timer = 0; //-80dB to 0dB
        while (timer <= fadeTime)
        {
            float newVol = Mathf.Lerp(-80f, maxVolume, (timer / fadeTime));
            //Debug.Log((timer / fadeTime));
            mixer.SetFloat("Vol Fragment " + mixerGroupIndex, newVol);
            timer += Time.deltaTime;
            yield return null;
        }
        mixer.SetFloat("Vol Fragment " + mixerGroupIndex, maxVolume);
        yield return null;
    }

    public IEnumerator FadeInMasterMixer()
    {
        float timer = 0; //-80dB to 0dB
        while (timer <= fadeTime)
        {
            float newVol = Mathf.Lerp(-80f, maxVolume, (timer / fadeTime));
            //Debug.Log((timer / fadeTime));
            mixer.SetFloat("Vol Master", newVol);
            //mixer.GetFloat("Vol Master", out newVol);
            //Debug.Log(newVol);
            timer += Time.deltaTime;
            yield return null;
        }
        mixer.SetFloat("Vol Master", maxVolume);
        yield return null;
    }

    public void SetHighPassDuck(bool isActive)
    {
        if(isActive)
            mixer.SetFloat("HighPass Cutoff", 960.00f);
        else
            mixer.SetFloat("HighPass Cutoff", 10.00f);
    }

    public void SetLowPassCutoffLevel(int level)
    {
        switch(level)
        {
            case 1:
                mixer.SetFloat("LowPass Cutoff", 545.00f);
                break;
            case 2:
                mixer.SetFloat("LowPass Cutoff", 1200.00f);
                break;
            case 3:
                mixer.SetFloat("LowPass Cutoff", 3000.00f);
                break;
            default:
                mixer.SetFloat("LowPass Cutoff", 22000.00f);
                break;
        }
    }
}


public class ScoreManager : MonoBehaviour
{
    [Header("Score Patterns")]
    public ScorePattern[] scorePatterns;
    public ScorePattern VoidPattern;
    public ScorePattern finalCinematic;
    public ScorePattern CreditsPattern;

    [Header("Docks")]
    public Mixer[] docks;
    private int CurrentActiveDock;

    [SerializeField]
    private int NoOfAudioTracksPerDock = 3;
    public int NoOfVoidLevels = 3;
    [SerializeField] private int MaxVolume;

    [Header("Metronome")]
    [Header("BPM")]
    public double bpm; //todo: actually implement time signature-based counting
    public int numberOfBeatsToUseInCrossfade = 6;
    [Header("Time Signature")]
    public int Numerator;
    public int Denominator;


    double nextTick = 0.0F; // The next tick in dspTime
    double sampleRate = 0.0F;
    bool ticked = false;

    private double DefaultCrossfadeTime = 0.5f;

    public static ScoreManager _instance;

    public static ScoreManager GetInstance()
    {
        return _instance;
    }

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        else if (_instance != this)
            Destroy(gameObject);

        //DontDestroyOnLoad(gameObject);

        double startTick = AudioSettings.dspTime;
        sampleRate = AudioSettings.outputSampleRate;


        if(bpm != 0)
            nextTick = startTick + (60.0 / bpm);

        //float maxVolume;
        //docks[0].mixer.GetFloat("Vol Master", out maxVolume);

        for (int i = 0; i < docks.Length; i++)
        {
            docks[i].NumberOfAudioTracks = NoOfAudioTracksPerDock;
            docks[i].CreateSources(gameObject);
            docks[i].maxVolume = MaxVolume;
        }
    }

    private void Start()
    {
        // Setting up our initial state
        //LoadPattern(0, 0);
        LoadPatternOntoDock(0, docks[0].currentScorepattern);
        //LoadPattern(1, 1);
        LoadPatternOntoDock(1, docks[1].currentScorepattern);
        LoadPattern(1, false);
        CurrentActiveDock = 0;

        ResetToDefault(CurrentActiveDock);
        

        Play(0);

        Debug.Log("Resetting Audio");
    }

    public void ResetToDefault(int dockIndex)
    {
        docks[dockIndex].SetMasterVolume(MaxVolume);
        for (int i = 1; i <= 3; i++)
             docks[dockIndex].SetVolume(i, -80);
        docks[dockIndex].SetHighPassDuck(false);
        docks[dockIndex].SetLowPassCutoffLevel(-1);
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
        //Debug.Log("Crossfading now");
        int OtherDock = (CurrentActiveDock + 1) % docks.Length;
        Play(OtherDock);
        FadeOut(CurrentActiveDock);
        FadeIn(OtherDock);
        StartCoroutine(UnloadDockAfterSeconds(CurrentActiveDock, OtherDock));
    }

    public void FadeIn(int DockIndex)
    {
        docks[DockIndex].fadeTime = (float)DefaultCrossfadeTime;
        StartCoroutine(docks[DockIndex].FadeInMasterMixer());
    }

    public void FadeOut(int DockIndex)
    {

        docks[DockIndex].fadeTime = (float)DefaultCrossfadeTime;
        StartCoroutine(docks[DockIndex].FadeOutMasterMixer());
    }

    public void FadeOutMixerGroup(int mixerIndex)
    {
        StartCoroutine(docks[CurrentActiveDock].FadeOutMixerGroup(mixerIndex));
    }

    public void FadeInMixerGroup(int mixerIndex)
    {
        StartCoroutine(docks[CurrentActiveDock].FadeInMixerGroup(mixerIndex));
    }

    public void Play(int DockIndex)
    {
        docks[DockIndex].Play();
        bpm = docks[DockIndex].currentScorepattern.bpm;
    }


    //These are all different ways you can load the next pattern into the other dock
    public void LoadPattern(int Index, bool AllTracksActive)
    {
        int DockIndex = (CurrentActiveDock + 1) % docks.Length;
        LoadPattern(Index, DockIndex, AllTracksActive);
    }

    public void LoadPattern(ScorePattern pattern, bool AllTracksActive = false)
    {
        int DockIndex = (CurrentActiveDock + 1) % docks.Length;
        LoadPattern(pattern, DockIndex);
    }

    public void LoadPattern(ScorePattern pattern, int DockIndex, bool AllTracksActive = false)
    {
        docks[DockIndex].Load(pattern, AllTracksActive);
        if(!AllTracksActive) ResetToDefault(DockIndex);
    }

    public void LoadPattern(int Index, int DockIndex, bool AllTracksActive=false)
    {
        docks[DockIndex].Load(scorePatterns[Index], AllTracksActive);
        if(!AllTracksActive) ResetToDefault(DockIndex);
    }

    public void LoadPatternOntoDock(int DockIndex, ScorePattern pattern, bool AllTracksActive = false)
    {
        docks[DockIndex].Load(pattern, AllTracksActive);
        if (!AllTracksActive) ResetToDefault(DockIndex);
    }

    public void SetHighPassDuck(bool isActive)
    {
        docks[CurrentActiveDock].SetHighPassDuck(isActive);
    }

    // The Update functions mostly just handle updating the metronome
    void FixedUpdate()
    {
        double timePerTick = 60.0f / bpm;
        DefaultCrossfadeTime = numberOfBeatsToUseInCrossfade*timePerTick;
        docks[CurrentActiveDock].fadeTime = (float)DefaultCrossfadeTime;
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

    public void LoadVoidAtLevel(int VoidLevel)
    {
        LoadPattern(VoidPattern);
        docks[(CurrentActiveDock + 1) % 2].SetLowPassCutoffLevel(VoidLevel);
    }

    public void LoadPatternAtLevel(ScorePattern pattern, int level)
    {

    }

    public void ReplaceVoidWithFinal()
    {
        if(VoidPattern != finalCinematic)
            VoidPattern = finalCinematic;
    }

    /** Final Zone Handling **/

    public void LoadFinalZone()
    {
        LoadPattern(finalCinematic);
        docks[(CurrentActiveDock + 1) % 2].Pause();
        docks[(CurrentActiveDock + 1) % 2].sources[0].loop = false;
    }

    public void LoadCreditsTrack()
    {
        LoadPattern(CreditsPattern);
        docks[(CurrentActiveDock + 1) % 2].Pause();
    }

    public IEnumerator PlayCreditsAfterFinal()
    {
        yield return new WaitForSeconds(5);
        while (docks[CurrentActiveDock].sources[0].isPlaying)
            yield return null;
        LoadCreditsTrack();
        Crossfade();
        yield return null;
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
