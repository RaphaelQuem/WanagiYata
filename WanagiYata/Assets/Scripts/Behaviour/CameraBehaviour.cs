using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{

    public GameObject player;
    public float speed;
    void Start()
    {

    }

    void Update()
    {

        if (player != null)
        {

            Vector3 movVector = player.transform.position - transform.position;
            movVector.z = 0;
            Vector3 cameraPos = transform.position + movVector * Time.deltaTime * speed;
            transform.position = ConstrainedVector(cameraPos);

        }

    }
    private Vector3 ConstrainedVector(Vector3 pos)
    {
        GameObject map = GameObject.FindGameObjectWithTag("Map");

        pos.x = Math.Max(map.transform.position.x - 1.95f, pos.x);
        pos.x = Math.Min(map.transform.position.x + 1.95f, pos.x);

        pos.y = Math.Max(map.transform.position.y - 0.57f, pos.y);
        pos.y = Math.Min(map.transform.position.y + 0.57f, pos.y);

        return pos;
    }
}
