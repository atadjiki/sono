using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/* This script if primarily for 
 * >> In-Game Menu navigation
 * >>

*/

public enum MenuStates { Resume, Restart, Exit };

public class InGameMenu : MonoBehaviour
{
    public GameObject obj_MenuPanel;

    public Text[] txt_Buttons = new Text[3];

    public Color color_Highlight;

    public MenuStates _currentState;

    // Start is called before the first frame update
    void Start()
    {
        //_currentState = (MenuStates)(+1);
    }

    private void OnEnable()
    {
        // _currentState = MenuStates.Resume;
        // img_Buttons[0].color = color_Highlight;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Do_Select(PlayerInput.TurntableController i_controller) // Do Actions Base on _currentState
    {
        switch (_currentState)
        {
            case MenuStates.Resume:
                i_controller.toggleMenu();
                break;

            case MenuStates.Restart:
                //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                SceneManager.LoadScene("Hub");
                Time.timeScale = 1;
                break;

            case MenuStates.Exit:
                Debug.Log("Exit Requested");
                Application.Quit();
                break;
        }
    }


    public void Resume(PlayerInput.TurntableController i_controller)
    {
        i_controller.toggleMenu();
    }

    public void Restart()
    {
        SceneManager.LoadScene("Hub");
        Time.timeScale = 1;
    }

    public void ExitGame()
    {
        Debug.Log("Exit Requested");
        Application.Quit();
    }


    public void scroll(bool up) // true = up , false = down
    {
        // change state and highlight
        if (up) // UP
        {
            if (!(_currentState == MenuStates.Resume)) // if its not resume state
            {
                if (_currentState == MenuStates.Restart)
                {
                    _currentState = MenuStates.Resume;
                    highlightButton(0);
                }
                else
                {
                    _currentState = MenuStates.Restart;
                    highlightButton(1);
                }
            }
        }
        else // Dwon
        {
            if (!(_currentState == MenuStates.Exit)) // if its not resume state // down
            {
                if (_currentState == MenuStates.Restart)
                {
                    _currentState = MenuStates.Exit;
                    highlightButton(2);
                }
                else
                {
                    _currentState = MenuStates.Restart;
                    highlightButton(1);
                }
            }
        }
    }

    void highlightButton(int i)
    {
        foreach (Text txt in txt_Buttons)
        {         
            txt.color = Color.white;
        }
        txt_Buttons[i].color = color_Highlight;
    }

    public void ActivatePannel(bool i_state)
    {
        obj_MenuPanel.SetActive(i_state);
        ScoreManager._instance.SetHighPassDuck(i_state);
    }
}
