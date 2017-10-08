using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SpawnManager
{
    
    public static void ClearSpawns()
    {
        string[] tags = new string[] { "Trap", "Animal"};
        foreach (string tag in tags)
        {
            foreach (GameObject obj in GameObject.FindGameObjectsWithTag(tag))
            {
                GameObject.Destroy(obj);
            }
        }
    }
    public static void SpawnAnimal()
    {
        foreach (string spawn in StaticResources.SpawnPool)
        {
            float chance = 1f / StaticResources.SpawnPool.Length;
            if (Random.value < chance)
            {
                float rndx = Random.Range(StaticResources.LeftCameraLimit,StaticResources.RightCameraLimit);
                float rndy = Random.Range(StaticResources.BotCameraLimit, StaticResources.TopCameraLimit);
                GameObject spawnedobj = (GameObject)Resources.Load(spawn);
                spawnedobj.transform.position = new Vector3(rndx, rndy,0);
                GameObject.Instantiate(spawnedobj);
            }
        }
    }
}
