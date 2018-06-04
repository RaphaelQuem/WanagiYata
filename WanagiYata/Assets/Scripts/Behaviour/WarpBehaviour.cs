using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WarpBehaviour : MonoBehaviour {

    public Transform destination;
    public int DestinatioRow;
    public int DestinationCol;
    public GameObject rightCurtain;
    public GameObject leftCurtain;
    public GameObject camera;
    private bool IsTransitioning;

    private void Start()
    {
        IsTransitioning = false;
    }
    private void Update()
    {

        if(IsTransitioning)
        {
            if (rightCurtain.transform.position.x > camera.transform.position.x || leftCurtain.transform.position.x < camera.transform.position.x)
            {
                var rightmov = (rightCurtain.transform.position - camera.transform.position);
                var leftmov = (leftCurtain.transform.position - camera.transform.position);

                rightmov.Normalize();
                leftmov.Normalize();
                rightCurtain.transform.position = (rightCurtain.transform.position + rightmov) *0.0002f;
                leftCurtain.transform.position =  (leftCurtain.transform.position + leftmov) * 0.0002f;
            }
            else
                SceneManager.LoadScene("Mapa");
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Equals("Player"))
        {
            IsTransitioning = true;
        }
        
    }
    
}
