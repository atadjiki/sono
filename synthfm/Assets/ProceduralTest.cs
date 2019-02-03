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
 
    private GameObject right;
    private GameObject rightPuzzle;

    private GameObject up;
    private GameObject upPuzzle;

    private GameObject down;
    private GameObject downPuzzle;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetKeyDown(KeyCode.P)){

            DespawnAll();
            TakeSnapshot();
            SpawnPuzzles();
        }

    }

    void TakeSnapshot()
    {
        //get the screen dimensions for the player
     //   Vector3 screenPos = mainCamera.WorldToScreenPoint(playerPos.transform.TransformPoint(Vector3.zero));

        float playerX = playerPos.transform.position.x;
        float playerY = playerPos.transform.position.y;

        left = new GameObject();
        left.name = "Left";
        left.tag = "Left";
        left.transform.position = new Vector2(playerX - mainCamera.pixelWidth/2, playerY);
        left.AddComponent<BoxCollider2D>();
        left.GetComponent<BoxCollider2D>().size = new Vector2(mainCamera.pixelWidth/2, mainCamera.pixelHeight/2);
        left.GetComponent<BoxCollider2D>().isTrigger = true;


        right = new GameObject();
        right.name = "Right";
        right.tag = "Right";
        right.transform.position = new Vector2(playerX + mainCamera.pixelWidth/2, playerY);
        right.AddComponent<BoxCollider2D>();
        right.GetComponent<BoxCollider2D>().size = new Vector2(mainCamera.pixelWidth/2, mainCamera.pixelHeight/2);
        right.GetComponent<BoxCollider2D>().isTrigger = true;

        up = new GameObject();
        up.name = "Up";
        up.tag = "Up";
        up.transform.position = new Vector2(playerX, playerY + mainCamera.pixelWidth / 2);
        up.AddComponent<BoxCollider2D>();
        up.GetComponent<BoxCollider2D>().size = new Vector2(mainCamera.pixelWidth / 2, mainCamera.pixelHeight / 2);
        up.GetComponent<BoxCollider2D>().isTrigger = true;

        down = new GameObject();
        down.name = "Down";
        down.tag = "Down";
        down.transform.position = new Vector2(playerX, playerY - mainCamera.pixelWidth / 2);
        down.AddComponent<BoxCollider2D>();
        down.GetComponent<BoxCollider2D>().size = new Vector2(mainCamera.pixelWidth / 2, mainCamera.pixelHeight / 2);
        down.GetComponent<BoxCollider2D>().isTrigger = true;

    }

    private void SpawnPuzzles()
    {
       
        int puzzleIndex = Random.Range(0, puzzles.Count - 1);


        leftPuzzle = Instantiate(puzzles[puzzleIndex], left.transform);
        
        GameObject leftCam = Instantiate(cinCamera);
        leftCam.transform.parent = leftPuzzle.transform;
        leftPuzzle.GetComponent<SetPiece>().setPieceCamera = leftCam.GetComponent<Cinemachine.CinemachineVirtualCamera>();
        leftCam.GetComponent<Cinemachine.CinemachineVirtualCamera>().enabled = false;

        leftPuzzle.GetComponent<SetPiece>().setPieceCamera = leftCam.GetComponent<Cinemachine.CinemachineVirtualCamera>();
        leftPuzzle.GetComponent<SetPiece>().setPieceCamera.Follow = leftPuzzle.transform;
        leftPuzzle.name = "Left Puzzle";
        leftPuzzle.tag = "Left";

        Debug.Log("Left puzzle at " + leftPuzzle.transform.position.ToString());


        rightPuzzle = Instantiate(puzzles[puzzleIndex], right.GetComponent<Transform>());
        
        GameObject rightCam = Instantiate(cinCamera);
        rightCam.transform.parent = rightPuzzle.transform;
        rightPuzzle.GetComponent<SetPiece>().setPieceCamera = rightCam.GetComponent<Cinemachine.CinemachineVirtualCamera>();
        rightCam.GetComponent<Cinemachine.CinemachineVirtualCamera>().enabled = false;


        rightPuzzle.GetComponent<SetPiece>().setPieceCamera = rightCam.GetComponent<Cinemachine.CinemachineVirtualCamera>();
        rightPuzzle.GetComponent<SetPiece>().setPieceCamera.Follow = rightPuzzle.transform;
        rightPuzzle.name = "Right Puzzle";
        rightPuzzle.tag = "Right";

        Debug.Log("Right puzzle at " + rightPuzzle.transform.position.ToString());

        upPuzzle = Instantiate(puzzles[puzzleIndex], up.GetComponent<Transform>());

        GameObject upCam = Instantiate(cinCamera);
        upCam.transform.parent = upPuzzle.transform;
        upPuzzle.GetComponent<SetPiece>().setPieceCamera = upCam.GetComponent<Cinemachine.CinemachineVirtualCamera>();
        upCam.GetComponent<Cinemachine.CinemachineVirtualCamera>().enabled = false;


        upPuzzle.GetComponent<SetPiece>().setPieceCamera = upCam.GetComponent<Cinemachine.CinemachineVirtualCamera>();
        upPuzzle.GetComponent<SetPiece>().setPieceCamera.Follow = upPuzzle.transform;
        upPuzzle.name = "Up Puzzle";
        upPuzzle.tag = "Up";

        Debug.Log("Up puzzle at " + upPuzzle.transform.position.ToString());

        downPuzzle = Instantiate(puzzles[puzzleIndex], down.GetComponent<Transform>());

        GameObject downCam = Instantiate(cinCamera);
        downCam.transform.parent = downPuzzle.transform;
        downPuzzle.GetComponent<SetPiece>().setPieceCamera = downCam.GetComponent<Cinemachine.CinemachineVirtualCamera>();
        downCam.GetComponent<Cinemachine.CinemachineVirtualCamera>().enabled = false;


        downPuzzle.GetComponent<SetPiece>().setPieceCamera = downCam.GetComponent<Cinemachine.CinemachineVirtualCamera>();
        downPuzzle.GetComponent<SetPiece>().setPieceCamera.Follow = downPuzzle.transform;
        downPuzzle.name = "Down Puzzle";
        downPuzzle.tag = "Down";

        Debug.Log("Down puzzle at " + downPuzzle.transform.position.ToString());

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //TO DO: Add if condition to check tag to zoom in to appropriate camera
        DespawnObjects(collision.gameObject.tag);

    }

    private void DespawnObjects(string dir)
    {

        List<GameObject> toDestroy = new List<GameObject>();

        if (dir != "Up")
        {
            Debug.Log("Destroying Up Puzzles");
            toDestroy.AddRange(GameObject.FindGameObjectsWithTag("Up"));

        }
        else if(dir != "Down")
        {
            Debug.Log("Destroying Down Puzzles");
            toDestroy.AddRange(GameObject.FindGameObjectsWithTag("Down"));
        }
        else if(dir != "Right")
        {
            Debug.Log("Destroying Right Puzzles");
            toDestroy.AddRange(GameObject.FindGameObjectsWithTag("Right"));
        }
        else if(dir != "Left")
        {
            Debug.Log("Destroying Left Puzzles");
            toDestroy.AddRange(GameObject.FindGameObjectsWithTag("Left"));
        }

        foreach (GameObject item in toDestroy)
        {
            Destroy(item);
        }
    }

    private void DespawnAll()
    {

        List<GameObject> toDestroy = new List<GameObject>();

        toDestroy.AddRange(GameObject.FindGameObjectsWithTag("Left"));
        toDestroy.AddRange(GameObject.FindGameObjectsWithTag("Right"));
        toDestroy.AddRange(GameObject.FindGameObjectsWithTag("Up"));
        toDestroy.AddRange(GameObject.FindGameObjectsWithTag("Down"));

        foreach(GameObject item in toDestroy)
        {
            Destroy(item);
        }

    }
}
