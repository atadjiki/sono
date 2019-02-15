using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class Particlescripts : MonoBehaviour
{
    public ParticleSystem part;
    public GameObject particle;
    public List<ParticleCollisionEvent> collisionEvents;
    public Material newMaterial;
    // Start is called before the first frame update
    void Start()
    {
        collisionEvents = new List<ParticleCollisionEvent>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}

