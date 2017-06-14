using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalBehaviour : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Transform closestTrap = GetClosestTrap(GameObject.FindGameObjectsWithTag("Trap"));

        if(closestTrap !=null)
        {
            Vector3 movVector = closestTrap.position - transform.position ;
            movVector.Normalize();
       

            gameObject.transform.position = gameObject.transform.position + movVector * Time.deltaTime;
        }

    }

    Transform GetClosestTrap(GameObject[] traps)
    {
        Transform tMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = transform.position;
        foreach (GameObject t in traps)
        {
            float dist = Vector3.Distance(t.transform.position, currentPos);
            if (dist < minDist)
            {
                tMin = t.transform;
                minDist = dist;
            }
        }
        return tMin;
    }

}
