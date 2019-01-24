using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FragmentCase : MonoBehaviour
{
    public int fragmentNumber;
    private FragmentController fragment;

    private void Start()
    {
        fragment = Instantiate(LevelManager.instance.fragmentPrefab, transform).GetComponent<FragmentController>();
        fragment.SetClip(LevelManager.instance.audioFragments[fragmentNumber]);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if(collision.gameObject == LevelManager.instance.getPlayer())
        {
            fragment.Collect(LevelManager.instance.getPlayer().transform);
        }
       
    }
}
