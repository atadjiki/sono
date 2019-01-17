using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    public GameObject player;
    public TurntableManager turntableManager;
    private float left;
    private float right;
    private float fade;

    // Start is called before the first frame update
    void Start()
    {
        turntableManager = FindObjectOfType<TurntableManager>();
        left = turntableManager.getLeft();
        right = turntableManager.getRight();
        fade = turntableManager.getFade();
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
