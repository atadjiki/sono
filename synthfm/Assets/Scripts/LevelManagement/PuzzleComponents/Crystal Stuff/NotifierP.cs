using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotifierP : MonoBehaviour
{
    public int seqNo;
    public PuzzleManager puzzleManager;
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
            puzzleManager.Notify(this);
        }
    }

    public void changeState(PuzzleBehavior.State i_state) { _curState = i_state; }

    public PuzzleBehavior.State getState() { return _curState; }

}
