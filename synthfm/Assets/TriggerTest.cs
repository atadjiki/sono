using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(1, UnityEngine.SceneManagement.LoadSceneMode.Additive);

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("HERE");
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(1,UnityEngine.SceneManagement.LoadSceneMode.Additive);
    }
}
