using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleBehavior : MonoBehaviour
{

    public enum State { OFF, ON, Error };

    [Header("Change this to private later !!!")]
    // TODO :: chnage to private once done with debuging
    public ColorManager[] colorPlates;

    public int currentSequence = 0;
    public bool IsComplete;

    [Header("Audio Source")]
      public AudioSource PuzzleaudioSource;

    [Header("AudioTones")]
    public AudioClip[] pianoKeys;
    public AudioClip errorTone;

    // shaking effect
    //private Transform transform;
    //private float shakeDuration;
    //private float shakeMagnitude = 0.8f;
    //private float dampingSpeed = 1.0f;
    //Vector3 initialPosition;


    private void Awake()
    {
        //if (transform == null)
        //{
        //    transform = GetComponent(typeof(Transform)) as Transform;
        //}
    }

    // Start is called before the first frame update
    void Start()
    {
        IsComplete = false;
        PuzzleaudioSource = this.GetComponent<AudioSource>();
        
        //audioSource.clip = pianoKeys[0];
        Debug.Log(transform.Find("ColorPlates").childCount);
        //for(int i=0; i < transform.Find("ColorPlates").childCount; i++)
        {
            //   ColorManager x = transform.Find("ColorPlates").GetChild(1).GetComponent<ColorManager>();
        }

      //  transform = this.GetComponent<Transform>();
        //initialPosition = transform.localPosition;
        //shakeDuration = 0;
    }

    // Update
    private void Update()
    {
       // shakeDuration = 2;
        //if (shakeDuration > 0)
        //{
        //    transform.localPosition = initialPosition + Random.insideUnitSphere * shakeMagnitude;

        //    shakeDuration -= Time.deltaTime * dampingSpeed;
        //}
        //else
        //{
        //    shakeDuration = 0f;
        //    transform.localPosition = initialPosition;
        //}
    }

    public void notify(Notifier i_notifier)
    {
        if (!IsComplete)
        {
            // check if it matche to current sequence and change color
            if (currentSequence == i_notifier.seqNo)
            {
                int toPlay = currentSequence % pianoKeys.Length;
                PuzzleaudioSource.clip = pianoKeys[toPlay];
                PuzzleaudioSource.Play();

                colorPlates[currentSequence].changeToActive();
                i_notifier.changeState(State.ON);

                currentSequence++;
            }
            else // error state
            {
                foreach (ColorManager _cm in colorPlates)
                {
                    _cm.changeToFail();
                }

                // Play error tone
                PuzzleaudioSource.clip = errorTone;
                PuzzleaudioSource.Play();

                i_notifier.changeState(State.OFF);

                currentSequence = 0;

              //  shakeDuration = 2f;
            }
        }
        if (currentSequence == 18)
        {
             IsComplete = true;
            currentSequence = 0;
        }
    }
}
