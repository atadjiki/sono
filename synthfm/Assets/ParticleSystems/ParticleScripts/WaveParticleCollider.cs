using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.VFX;
public class WaveParticleCollider: MonoBehaviour
{
    public VisualEffect Waves;
    public GameObject Player;
    public Vector3 PlayerLocation;

    // Start is called before the first frame update
    void Start()
    {
        if(Player == null)
        {
            Player = GameObject.Find("Player");
        }
    }

    // Update is called once per frame
    void Update()
    {
        PlayerLocation = Player.transform.position;
        
        
    }
}
