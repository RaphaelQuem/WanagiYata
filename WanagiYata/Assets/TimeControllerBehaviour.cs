using Assets.Scripts.Resource;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeControllerBehaviour : MonoBehaviour
{
    public float daytimeduration;
    private GameObject sun;
    private GameObject[] heroLights;

    // Use this for initialization
    void Start()
    {
        StaticResources.CurrentDay = 1;
        StaticResources.CurrentTime = 0;
        sun = GameObject.FindGameObjectWithTag("SunLight");
        heroLights = GameObject.FindGameObjectsWithTag("HeroLight");
    }

    // Update is called once per frame
    void Update()
    {
        if (StaticResources.CurrentTime >= 60 || StaticResources.CurrentTime < 30)
        {
            
            if (sun.GetComponent<Light>().intensity < 1.5f)
            {
                sun.GetComponent<Light>().intensity += (sun.GetComponent<Light>().intensity + 0.015f > 1.5f? 1.5f - sun.GetComponent<Light>().intensity:0.015f);
                foreach(var heroLight in heroLights)
                    heroLight.GetComponent<Light>().intensity = 1.5f - sun.GetComponent<Light>().intensity;
            }
            gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("sunSprite");
            StaticResources.DayTime = DayTime.Day;
            if(StaticResources.CurrentTime >= 60)
                StaticResources.CurrentTime = 0;
        }
        else if (StaticResources.CurrentTime > 30)
        {
            
            if (sun.GetComponent<Light>().intensity > 0)
            {
                sun.GetComponent<Light>().intensity -= (sun.GetComponent<Light>().intensity - 0.015f < 0f ? sun.GetComponent<Light>().intensity : 0.015f);
                foreach (var heroLight in heroLights)
                    heroLight.GetComponent<Light>().intensity = 1.5f - sun.GetComponent<Light>().intensity;
            }
            StaticResources.DayTime = DayTime.Night;
            gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("moonSprite");
        }
        StaticResources.CurrentTime += Time.deltaTime;

    }

}
