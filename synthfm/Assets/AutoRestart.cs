using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using InControl;

public class AutoRestart : MonoBehaviour
{

    PlayerInput.InputBindings inputBindings;

    public bool ListenForMidiInput = false;
    private PlayerInput.TurntableController player;

    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0f;
        player = GameObject.Find("Player").GetComponent<PlayerInput.TurntableController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ListenForMidiInput && !player.IsMidiInput())
        {
            timer += Time.deltaTime;
        }
        else if(!Input.anyKeyDown)
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
