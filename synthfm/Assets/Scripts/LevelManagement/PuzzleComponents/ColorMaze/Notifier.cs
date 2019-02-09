using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Notifier : MonoBehaviour
{

    public int seqNo;
    public PuzzleBehavior puzzleBehavior;
    public PuzzleBehavior.State _curState;

    // Start is called before the first frame update
    void Start()
    {
        _curState = PuzzleBehavior.State.OFF;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
      //  if (!(_curState == PuzzleBehavior.State.ON))
        {
            puzzleBehavior.notify(this);
        }
    }

    public void changeState(PuzzleBehavior.State i_state) { _curState = i_state; }

    public PuzzleBehavior.State getState() { return _curState; }

}
