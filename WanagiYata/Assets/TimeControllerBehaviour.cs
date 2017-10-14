using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeControllerBehaviour : MonoBehaviour {
    public float daytimeduration;
    public int currentDay;
    public float currentTime;
	// Use this for initialization
	void Start () {
        currentDay = 1;
        currentTime = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if (currentTime >= 60)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("sunSprite");
            currentTime = 0;
        }
        else if (currentTime > 30)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("moonSprite");
        }
        currentTime += Time.deltaTime;

	}
}
