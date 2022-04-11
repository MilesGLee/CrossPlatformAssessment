using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateWorld : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private GameObject spawn;

    void Update()
    {
        if (CheckToSpawn())
            GenerateCube();
    }

    bool CheckToSpawn() 
    {
        GameObject[] list = GameObject.FindGameObjectsWithTag("World");
        if (list.Length < 20)
            return true;
        else
            return false;
    }

    void GenerateCube() 
    {
        Vector3 spawnLocation;
        spawnLocation.x = player.position.x + Random.Range(5.0f, 10.0f);
        spawnLocation.y = Random.Range(-5.0f, 5.0f);
        spawnLocation.z = 0;

        DestroyIfTooFar spawned = Instantiate(spawn, spawnLocation, Quaternion.identity).GetComponent<DestroyIfTooFar>();
        spawned.Target = player;
        spawned.Distance = 20;
    }
}
