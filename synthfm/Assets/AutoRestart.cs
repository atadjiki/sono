using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using InControl;

public class AutoRestart : MonoBehaviour
{

    PlayerInput.InputBindings inputBindings;

    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        print(timer);
        if(!Input.anyKeyDown)
        {
            timer += Time.deltaTime;
        }
        else
        {
            timer = 0f;
        }

        if(timer >=60f)
        {
            //Scene loadedLevel = SceneManager.GetActiveScene();
            SceneManager.LoadScene("Hub");
            timer = 0f;
        }
    }
}
