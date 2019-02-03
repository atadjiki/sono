using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralTest : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private GameObject playerPos;
    [SerializeField] private List<GameObject> puzzles;

    private GameObject left;
    private GameObject leftPuzzle;
    private GameObject right;
    private GameObject above;
    private GameObject below;

    // Start is called before the first frame update
    void Start()
    {
        
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
        left.transform.position = new Vector2(screenPos.x - (Screen.width*1.1f), screenPos.y - (Screen.height/2));
        left.AddComponent<BoxCollider2D>();
        left.GetComponent<BoxCollider2D>().size = new Vector2(Screen.width, Screen.height);
        left.GetComponent<BoxCollider2D>().isTrigger = true;
        left.tag = "Left";
    }

    private void SpawnPuzzles()
    {
       
        int puzzleIndex = Random.Range(0, puzzles.Count - 1);

        GameObject leftTransform = new GameObject();
        leftTransform.transform.position = new Vector2(Random.Range(left.transform.position.x, left.transform.position.x + Screen.width/2), 
            leftTransform.transform.position.y); 


        leftPuzzle = Instantiate(puzzles[puzzleIndex], leftTransform.GetComponent<Transform>());
        leftPuzzle.AddComponent<Cinemachine.CinemachineVirtualCamera>();
        leftPuzzle.GetComponent<SetPiece>().setPieceCamera = leftPuzzle.GetComponent<Cinemachine.CinemachineVirtualCamera>();
        leftPuzzle.GetComponent<SetPiece>().setPieceCamera.Follow = leftPuzzle.transform;
        
        leftPuzzle.name = "Left Puzzzle";
        
        Debug.Log("Left puzzle at " + leftPuzzle.transform.position.ToString());

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DespawnObjects(collision.gameObject.tag);
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

        }
        else if(dir == "Left")
        {

        }
    }
}
