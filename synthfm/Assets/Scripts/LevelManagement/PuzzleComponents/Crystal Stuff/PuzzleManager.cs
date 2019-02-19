using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public enum State { OFF, ON, Error };

    public GameObject[] Crystals = new GameObject[4];

    private ColorIt _colorIt;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Notify(NotifierP i_notifier)
    {
      //  Debug.Log(i_notifier.gameObject.name);

        // if it is rock then disable all
        if (i_notifier.gameObject.name == "Rock")
        {
            // check sequencce number
            int seq = i_notifier.seqNo;

            // deactivate all crystalls
            for(int i=1; i<Crystals[seq].transform.childCount; i++)
            { //0th is Rock
                _colorIt = Crystals[seq].transform.GetChild(i).GetComponent<ColorIt>();
                _colorIt.changeToFail();
            }

        }
        else
        {
            _colorIt = i_notifier.gameObject.transform.GetComponent<ColorIt>();
            _colorIt.changeToActive();
        }
  
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
