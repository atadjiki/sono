using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnLotsOfPrefabs : MonoBehaviour //yeah... that's a typo in the class name. I'm too lazy to fix it.
{
    public GameObject ObjectToSpawn;
    public int NumberOfObjectsToSpawn = 50;
    public float SpawnRadius = 10.0f;

    //There's probably a better way to do this and I'm just making life difficult.
    void Awake()
    {
        // Basically defines a sphere (or in our case, a circle) and then picks a random point in it and spawns the prefab there.
        // Yes I totally stole and modified the SpawnPrefab script, because that one spawns over time.
        for (int i = 0; i < NumberOfObjectsToSpawn; i++)
        {
            Vector3 offset = Random.insideUnitCircle;
            offset.z = 0f;

            Instantiate(ObjectToSpawn, transform.position + offset * SpawnRadius, Quaternion.identity);
        }
    }
}
