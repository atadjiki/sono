using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleBehavior : MonoBehaviour
{

    // TODO :: chnage to private once done with debuging
    public ColorManager[] colorManagers = new ColorManager[15];

    public int currentSequence=0;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(transform.Find("ColorPlates").childCount);
        //for(int i=0; i < transform.Find("ColorPlates").childCount; i++)
        {
         //   ColorManager x = transform.Find("ColorPlates").GetChild(1).GetComponent<ColorManager>();
        }
    }

    public void notify(int  i_seqNo)
    {
        // check if it matche to current sequence and change color
        if(currentSequence == i_seqNo)
        {
            colorManagers[currentSequence].changeToActive();
            currentSequence++;
        }
        else
        {
            foreach (ColorManager _cm in colorManagers)
            {
                _cm.changeToFail();
            }
            currentSequence = 0;
        }
    }
}
