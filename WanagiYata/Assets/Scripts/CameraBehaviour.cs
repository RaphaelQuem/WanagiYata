using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{

    // Use this for initialization
    public GameObject player;
    public float speed;
    void Start()
    {

    }

    // Update is called once per frame
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

        pos.x = Math.Max(StaticResources.LeftCameraLimit, pos.x);
        pos.x = Math.Min(StaticResources.RightCameraLimit, pos.x);

        pos.y = Math.Max(StaticResources.BotCameraLimit, pos.y);
        pos.y = Math.Min(StaticResources.TopCameraLimit, pos.y);

        return pos;
    }
}
