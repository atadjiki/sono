using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hub : MonoBehaviour
{

    /*
     * Contains puzzles 
     */

    public Puzzle[] puzzleList;
    private Queue<Puzzle> puzzles;
    private bool hubComplete;

    // Start is called before the first frame update
    void Start()
    {
        puzzles = new Queue<Puzzle>(puzzleList);
        hubComplete = false;
    }

    public bool getStatus()
    {
        return hubComplete;
    }  

    public Puzzle nextPuzzle()
    {
        if(puzzles.Count == 0)
        {
            hubComplete = true;
            return null;
        }
        return puzzles.Dequeue();
    }
}
