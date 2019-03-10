using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class ChromaticAbberationSP : SetPiece
{

    public float multiplier = 2;
    public float min = 0.25f;
    public float max = 10;
    public Transform centerTransform;
    public Transform target;
    private float defaultAmount;
    private float currentAmount;
    private bool apply = false;
    private PostProcessVolume postProcess;

    // Start is called before the first frame update
    void Start()
    {
        base.Initialize();
        if(target == null)
        {
            target = GameObject.Find("Player").GetComponent<Transform>();
        }
        if(centerTransform == null)
        {
            centerTransform = this.transform;
        }
        postProcess = GameObject.Find("PostProcessLayer").GetComponent<PostProcessVolume>();

    }


    // Update is called once per frame
    void Update()
    {
        if (apply)
        {
            applyEffect();
        }
       

    }

    void applyEffect()
    {

        if (postProcess != null)
        {
        
            ChromaticAberration chromaticAberration;
            postProcess.profile.TryGetSettings(out chromaticAberration);
            if (chromaticAberration != null)
            {

                //the closer the player is to the target, apply more effect
                float distance = Vector3.Distance(this.transform.position, target.position);

                //scale the value between min and max ceilings
                distance = (distance - min) / (max - min);

                //calculate current value 
                currentAmount = (max - min) - distance;

                chromaticAberration.intensity.value = currentAmount * multiplier;
                Debug.Log("Chromatic Abberation = " + chromaticAberration.intensity.value);
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        base.TriggerEnter(collision);
        apply = true;

        if (postProcess != null)
        {
            ChromaticAberration chromaticAberration;
            postProcess.profile.TryGetSettings(out chromaticAberration);
            if (chromaticAberration != null)
            {

                //the closer the player is to the target, apply more effect
                defaultAmount = chromaticAberration.intensity.value;

                Debug.Log("Default Chromatic Abberation = " + defaultAmount);
            }
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        base.TriggerExit(collision);
        apply = false;

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
