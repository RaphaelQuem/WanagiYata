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
        var horzExtent = Camera.main.ortographicSize * Screen.width / Screen.height;

        var mapwidth = map.GetComponent<SpriteRenderer>().bounds.size.x;
        var stage = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

        pos.x = Math.Max(map.transform.position.x, pos.x);
        pos.x = Math.Min(map.transform.position.x + mapwidth - stage.x, pos.x);

        pos.y = Math.Max(StaticResources.BotCameraLimit, pos.y);
        pos.y = Math.Min(StaticResources.TopCameraLimit, pos.y);

        return pos;
    }
}
