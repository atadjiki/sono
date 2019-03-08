using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class VignetteSP : SetPiece
{
    public float min = 0.41f;
    public float max = 0.5f;
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
        if (target == null)
        {
            target = GameObject.Find("Player").GetComponent<Transform>();
        }
        if (centerTransform == null)
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

            Vignette vignette;
            postProcess.profile.TryGetSettings(out vignette);
            if (vignette != null)
            {

                //the closer the player is to the target, apply more effect
                float distance = Vector3.Distance(this.transform.position, target.position);

                //scale the value between min and max ceilings
                distance = (distance - min) / (max - min);

                //calculate current value 
                currentAmount = (max - min) - distance;

                vignette.intensity.value = Mathf.Abs(currentAmount)/10000;
                Debug.Log("Vignette = " + vignette.intensity.value);
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        base.TriggerEnter(collision);
        apply = true;

        if (postProcess != null)
        {
            Vignette vignette;
            postProcess.profile.TryGetSettings(out vignette);
            if (vignette != null)
            {

                //the closer the player is to the target, apply more effect
                defaultAmount = vignette.intensity.value;

                Debug.Log("Default Vignette = " + defaultAmount);
            }
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        base.TriggerExit(collision);
        apply = false;

        if (postProcess != null)
        {
            Vignette vignette;
            postProcess.profile.TryGetSettings(out vignette);
            if (vignette != null)
            {
                vignette.intensity.value = defaultAmount;
                Debug.Log("Vignette = " + vignette.intensity.value);
            }
        }
    }
}
