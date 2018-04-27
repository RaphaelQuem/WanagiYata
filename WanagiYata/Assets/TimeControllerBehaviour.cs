using Assets.Scripts.Resource;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeControllerBehaviour : MonoBehaviour
{
    public float daytimeduration;

    // Use this for initialization
    void Start()
    {
        StaticResources.CurrentDay = 1;
        StaticResources.CurrentTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (StaticResources.CurrentTime >= 60 || StaticResources.CurrentTime < 30)
        {
            var obj = GameObject.FindGameObjectWithTag("SunLight");
            if (obj.GetComponent<Light>().intensity < 1.1f)
            {
                obj.GetComponent<Light>().intensity += 0.05f;
            }
            gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("sunSprite");
            StaticResources.DayTime = DayTime.Day;
            if(StaticResources.CurrentTime >= 60)
                StaticResources.CurrentTime = 0;
        }
        else if (StaticResources.CurrentTime > 30)
        {
            var obj = GameObject.FindGameObjectWithTag("SunLight");
            if (obj.GetComponent<Light>().intensity > 0)
            {
                obj.GetComponent<Light>().intensity -= 0.05f;
            }
            StaticResources.DayTime = DayTime.Night;
            gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("moonSprite");
        }
        StaticResources.CurrentTime += Time.deltaTime;

    }

}
