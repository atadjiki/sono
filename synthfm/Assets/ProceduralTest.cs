using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralTest : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private GameObject playerPos;
    [SerializeField] private List<GameObject> puzzles;
    [SerializeField] private GameObject cinCamera;

    private GameObject left;
    private GameObject leftPuzzle;
    private GameObject rightPuzzle;
    private GameObject right;
    private GameObject above;
    private GameObject below;
    private GameObject temp;

    // Start is called before the first frame update
    void Start()
    {
        TakeSnapshot();
        SpawnPuzzles();
    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetKeyDown(KeyCode.P)){

            TakeSnapshot();
            SpawnPuzzles();
        }

    }

    void TakeSnapshot()
    {
        //get the screen dimensions for the player
        Vector3 screenPos = mainCamera.WorldToScreenPoint(playerPos.transform.TransformPoint(playerPos.transform.position));

        left = new GameObject();
        left.name = "Left";
        left.transform.position = new Vector2(playerPos.transform.position.x - Screen.width/2, screenPos.y - (Screen.height/2));
        left.AddComponent<BoxCollider2D>();
        left.GetComponent<BoxCollider2D>().size = new Vector2(Screen.width/2, Screen.height/2);
        left.GetComponent<BoxCollider2D>().isTrigger = true;
        left.tag = "Left";

        right = new GameObject();
        right.name = "Right";
        right.transform.position = new Vector2(playerPos.transform.position.x + Screen.width/2, screenPos.y - (Screen.height / 2));
        right.AddComponent<BoxCollider2D>();
        right.GetComponent<BoxCollider2D>().size = new Vector2(Screen.width/2, Screen.height/2);
        right.GetComponent<BoxCollider2D>().isTrigger = true;
        right.tag = "Right";

        Debug.Log("Screen Width: " + Screen.width);
    }

    private void SpawnPuzzles()
    {
       
        int puzzleIndex = Random.Range(0, puzzles.Count - 1);

        GameObject leftTransform = new GameObject();
        leftTransform.transform.position = new Vector2(left.transform.position.x, 
            left.transform.position.y); 


        leftPuzzle = Instantiate(puzzles[puzzleIndex], leftTransform.GetComponent<Transform>());
        
        temp = GameObject.Instantiate(cinCamera);
        temp.transform.parent = leftPuzzle.transform;
        leftPuzzle.GetComponent<SetPiece>().setPieceCamera = temp.GetComponent<Cinemachine.CinemachineVirtualCamera>();
        temp.GetComponent<Cinemachine.CinemachineVirtualCamera>().enabled = false;
        leftPuzzle.GetComponent<SetPiece>().setPieceCamera = temp.GetComponent<Cinemachine.CinemachineVirtualCamera>();
        leftPuzzle.GetComponent<SetPiece>().setPieceCamera.Follow = leftPuzzle.transform;
        leftPuzzle.name = "Left Puzzle";

        Debug.Log("Left puzzle at " + leftPuzzle.transform.position.ToString());


        GameObject rightTransform = new GameObject();
        rightTransform.transform.position = new Vector2(right.transform.position.x,
            right.transform.position.y);


        rightPuzzle = Instantiate(puzzles[puzzleIndex], rightTransform.GetComponent<Transform>());
        
        temp = GameObject.Instantiate(cinCamera);
        temp.transform.parent = rightPuzzle.transform;
        rightPuzzle.GetComponent<SetPiece>().setPieceCamera = temp.GetComponent<Cinemachine.CinemachineVirtualCamera>();
        temp.GetComponent<Cinemachine.CinemachineVirtualCamera>().enabled = false;


        rightPuzzle.GetComponent<SetPiece>().setPieceCamera = temp.GetComponent<Cinemachine.CinemachineVirtualCamera>();
        rightPuzzle.GetComponent<SetPiece>().setPieceCamera.Follow = rightPuzzle.transform;
        rightPuzzle.name = "Right Puzzle";


        Debug.Log("Right puzzle at " + rightPuzzle.transform.position.ToString());

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //TO DO: Add if condition to check tag to zoom in to appropriate camera

        //temp.GetComponent<Cinemachine.CinemachineVirtualCamera>().enabled = true;
      //  DespawnObjects(collision.gameObject.tag);

    }

    private void DespawnObjects(string dir)
    {
        if(dir == "Up")
        {
           
            
        }
        else if(dir == "Bottom")
        {

        }
        else if(dir == "Right")
        {
            Debug.Log("Destroying Left Puzzle");
            Destroy(left);
            Destroy(leftPuzzle);
        }
        else if(dir == "Left")
        {
            Debug.Log("Destroying Right Puzzle");
            Destroy(right);
            Destroy(rightPuzzle);
        }
    }
}
