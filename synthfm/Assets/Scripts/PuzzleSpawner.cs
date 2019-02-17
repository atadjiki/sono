using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class PuzzleSpawner : MonoBehaviour
{
    //    PuzzleSpawner - drag and drop class to spawn any type of puzzle(or compound puzzle)
    //- specify how many sub-puzzles you want 
    //- what type of puzzles
    //- change status to complete once all puzzles are done
    //- spawn a 2D collider around the puzzle area
    //- spawn a VCam to correspond to the puzzle area(also spawn a center object)
    //- support for custom colliders and cameras
    //- users can extend this and add any additional logic
    //- maybe specify what assets they want to use as wel
    //- TL:DR drag and drop puzzles with little to no setup

    public List<Puzzle> Puzzles = new List<Puzzle>();

    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.AddComponent<CircleCollider2D>();
        GameObject center = new GameObject();
        center.name = "Center";
        center.transform.parent = this.transform;

        GameObject VCam = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/Cameras/CM_Puzzle"));
        VCam.name = "CM_" + this.name;
        VCam.transform.parent = GameObject.Find("Camera Rig").transform;

    }

    // Update is called once per frame
    void Update()
    {
    }
}
