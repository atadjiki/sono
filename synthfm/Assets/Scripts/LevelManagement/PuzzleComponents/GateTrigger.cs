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



    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        parent = GetComponentInParent<GatePuzzle>();

        if (GetComponentInParent<GatePuzzle>() != null)
        {
            //   Debug.Log(this.name + " - found parent");
            partOfPuzzle = true;
        }


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
                //audioSource.Play();
                Debug.Log("Hit gate");
                
                GateReact.SetFloat("React", 40);

                GateReact.SetFloat("Emission", 0);
                print(GateReact.GetFloat("React"));
                if (!collided)
                {
                    NotifyPuzzle();
                }
                collided = true;
                
                GateReact.SetVector3("Position", new Vector3 (0, 5, 0));
                StartCoroutine(GateReaction());


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
