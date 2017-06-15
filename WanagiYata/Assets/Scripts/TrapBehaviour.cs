using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapBehaviour : MonoBehaviour {

    public GameObject vaca;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Equals("Animal"))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }

}
