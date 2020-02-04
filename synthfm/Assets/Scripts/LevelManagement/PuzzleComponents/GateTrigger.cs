using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.VFX;

public class GateTrigger : MonoBehaviour
{
    private AudioSource audioSource;
    private GatePuzzle parent;
    [SerializeField]
    private bool partOfPuzzle = false;
    public bool ignoreAngle = false;
    public bool collided = false;
    public VisualEffect GateReact;
    public Animator animator;
    public string animationName;

    private bool reacting = false;
    private bool shrinking = false;
    private bool expanding = false;
    private bool resize = false;
    private Vector3 defaultSize;
    public Vector3 shrinkSize = new Vector3(0.5f, 0.5f, 0.5f);
    public Vector3 expandSize = new Vector3(1.5f, 1.5f, 1.5f);
    public float lerpTime = 0.05f;


    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        parent = GetComponentInParent<GatePuzzle>();

        if (GetComponentInParent<GatePuzzle>() != null)
        {
            //   Debug.Log(this.name + " - found parent");
            partOfPuzzle = true;
        }

        defaultSize = this.gameObject.transform.localScale;

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Vector3 normalizedVelocity = collision.GetComponent<Rigidbody2D>().velocity.normalized;
            float angle = Vector3.Dot(transform.up, normalizedVelocity);
            if (angle > 0.5f || ignoreAngle)
            {
                //audioSource.clip = AssetManager.instance.gateTones[0];
                audioSource.volume = 0.8f;
                audioSource.Play();
                
                // Debug.Log("Hit gate");
                if (animator != null)
                {
                    animator.SetTrigger(animationName);
                }

                if (!collided)
                {
                    GateReact.SetFloat("React", 40);
                    GateReact.SetFloat("Emission", 0);
                    NotifyPuzzle();
                    GateReact.SetVector3("Position", new Vector3(0, 5, 0));
                    StartCoroutine(GateReaction());
                }
                collided = true;

            }
            else
            {
                Debug.Log("Hit gate at wrong angle");
            }

        }
    }


    private void NotifyPuzzle()
    {
        if (partOfPuzzle)
        {
            if (GetComponentInParent<Puzzle>() != null)
            {
                GetComponentInParent<Puzzle>().GateTriggered(this);
            }
        }

    }
    IEnumerator GateReaction()
    {
        yield return new WaitForSeconds(.5f);
        GateReact.SetFloat("React", -100);

    }

    IEnumerator LerpGate()
    {
        reacting = true;
        expanding = false;
        shrinking = true;

        while (reacting)
        {
            if (shrinking && this.gameObject.transform.localScale != shrinkSize)
            {
                Vector3.Lerp(this.gameObject.transform.localScale, shrinkSize, lerpTime);
            }
            else if (shrinking && this.gameObject.transform.localScale == shrinkSize)
            {
                shrinking = false;
                resize = false;
                expanding = true;
            }

            if (expanding && this.gameObject.transform.localScale != expandSize)
            {
                Vector3.Lerp(this.gameObject.transform.localScale, expandSize, lerpTime);
            }
            else if (expanding && this.gameObject.transform.localScale == shrinkSize)
            {
                shrinking = false;
                expanding = false;
                resize = true;
            }

            if (resize && this.gameObject.transform.localScale != defaultSize)
            {
                Vector3.Lerp(this.gameObject.transform.localScale, defaultSize, lerpTime);
            }
            else if (resize && this.gameObject.transform.localScale == defaultSize)
            {
                shrinking = false;
                expanding = false;
                resize = false;
                reacting = false;
            }
        }

        yield return new WaitForSeconds(0);
    }

    public void PlayAudioClip(AudioClip clip)
    {
        //audioSource.clip = clip;
        //AudioSource.PlayClipAtPoint(clip, transform.position);
        //audioSource.Play();

        GameObject tempGO = new GameObject("TempAudio");
        tempGO.transform.position = transform.position;
        AudioSource aSource = tempGO.AddComponent<AudioSource>();
        aSource.spatialBlend = 0f;
        aSource.clip = clip;
        aSource.Play();
        Destroy(tempGO, clip.length);
    }

    public void setPartOfPuzzle(bool input)
    {
        partOfPuzzle = input;
    }

    public void setIgnoreAngle(bool input)
    {
        ignoreAngle = input;
    }
}
