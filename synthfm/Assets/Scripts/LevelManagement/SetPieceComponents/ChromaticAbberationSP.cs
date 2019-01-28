using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class ChromaticAbberationSP : SetPiece
{

    public float multiplier;
    private float defaultAmount;

    private PostProcessVolume postProcess;

    // Start is called before the first frame update
    void Start()
    {
        base.Initialize();
        postProcess = GameObject.Find("PostProcessLayer").GetComponent<PostProcessVolume>();
    }


    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        base.TriggerEnter(collision);

        if (postProcess != null)
        {
            ChromaticAberration chromaticAberration;
            postProcess.profile.TryGetSettings(out chromaticAberration);
            if (chromaticAberration != null) {
                defaultAmount = chromaticAberration.intensity.value;
                chromaticAberration.intensity.value = multiplier * defaultAmount;
                Debug.Log("Chromatic Abberation = " + chromaticAberration.intensity.value);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        base.TriggerExit(collision);

        if (postProcess != null)
        {
            ChromaticAberration chromaticAberration;
            postProcess.profile.TryGetSettings(out chromaticAberration);
            if (chromaticAberration != null)
            {
                chromaticAberration.intensity.value = defaultAmount;
                Debug.Log("Chromatic Abberation = " + chromaticAberration.intensity.value);
            }
        }
    }
}
