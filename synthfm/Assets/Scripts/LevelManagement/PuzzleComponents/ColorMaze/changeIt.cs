using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeIt : MonoBehaviour
{
    private SpriteRenderer rendererThis;
    private Color defaultColor;

    private void Start()
    {
        rendererThis = this.GetComponent<SpriteRenderer>();
        defaultColor = rendererThis.color;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        rendererThis.color = Color.green;
        StartCoroutine(setBack());
    }

    IEnumerator setBack()
    {
        yield return new WaitForSeconds(5.0f);
        rendererThis.color = defaultColor;
    }

}
