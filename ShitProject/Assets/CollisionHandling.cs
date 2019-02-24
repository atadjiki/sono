using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollisionHandling : MonoBehaviour
{

    [SerializeField] private Slider slider;
    [SerializeField] private Text text;
     private float timer;
   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(slider.value == 1.0f)
        {
            print("You lose fuck censor boards");
            Time.timeScale = 0;
        }
        timer += Time.deltaTime;
        text.text = timer.ToString();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Hazard")
        {
            if(slider.value < 1.0f)
            {
                slider.value += 0.05f;

            }
        }
        else if(collision.gameObject.tag == "Invis")
        {
            StartCoroutine(GoInvis());
        }
    }

    IEnumerator GoInvis()
    {
        gameObject.GetComponent<SpriteRenderer>().color = new Color(gameObject.GetComponent<SpriteRenderer>().color.r, gameObject.GetComponent<SpriteRenderer>().color.g, gameObject.GetComponent<SpriteRenderer>().color.b, 0.5f);
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        yield return new WaitForSeconds(1.5f);
        gameObject.GetComponent<SpriteRenderer>().color = new Color(gameObject.GetComponent<SpriteRenderer>().color.r, gameObject.GetComponent<SpriteRenderer>().color.g, gameObject.GetComponent<SpriteRenderer>().color.b, 1f);
        gameObject.GetComponent<BoxCollider2D>().enabled = true;


    }
}
