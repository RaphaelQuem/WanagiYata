using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpBehaviour : MonoBehaviour {

    public Transform destination;
    public int DestinatioRow;
    public int DestinationCol;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Equals("Player"))
        {
            StaticResources.MapRow = DestinatioRow;
            StaticResources.MapColumn = DestinationCol;
            SpawnManager.ClearSpawns();
            SpawnManager.SpawnAnimal();
            other.transform.position = destination.position;
        }
        
    }
    
}
