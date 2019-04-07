using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResetLevel : MonoBehaviour
{
    [SerializeField] private float restartTimer;
    [SerializeField] private Image m_Image;

    private bool m_Fading;
    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        timer = 0.0f;
        //m_Fading = true;
        //StartCoroutine(FadeIn());
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= restartTimer)
        {
            StartCoroutine(FadeOut());
            m_Fading = false;
        }

        if(Input.GetKeyDown(KeyCode.R))
        {
            Scene loadedLevel = SceneManager.GetActiveScene();
            SceneManager.LoadScene(loadedLevel.buildIndex);
        }
    }

    IEnumerator FadeOut()
    {
        if(m_Fading == false)
        {
            m_Image.CrossFadeAlpha(1, 5.0f, false);
        }
        Debug.Log("Fade Out Screen!");

        yield return new WaitForSeconds(7.5f);

        Scene loadedLevel = SceneManager.GetActiveScene();
        SceneManager.LoadScene(loadedLevel.buildIndex);
    }

    IEnumerator FadeIn()
    {
        if (m_Fading == true)
        {
            m_Image.CrossFadeAlpha(0, 8.0f, true);
        }
        Debug.Log("Fade In Screen!");
        yield return 0;
    }
}
