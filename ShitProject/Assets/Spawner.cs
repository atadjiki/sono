using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Spawner : MonoBehaviour
{
    // Start is called before the first frame update


    public List<GameObject> stuffList;
    public List<GameObject> spawnList;


    private void Start()
    {



        StartCoroutine(spawnStuff());
    }
    // Update is called once per frame
    IEnumerator spawnStuff()
    {

        while (true)
        {
            int r1 = Random.Range(0, 5);

            int r = Random.Range(0, 3);
            Instantiate(stuffList[r], spawnList[r1].transform);
            print("HERE");
            yield return new WaitForSeconds(1.25f);
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            Scene loadedLevel = SceneManager.GetActiveScene();
            SceneManager.LoadScene(loadedLevel.buildIndex);
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            Application.Quit();
        }



    }
}
