using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidingZoneBehaviour : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Equals("Player"))
        {

            other.GetComponent<PlayerBehaviour>().CanHide = true;
        }

    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag.Equals("Player"))
        {

            other.GetComponent<PlayerBehaviour>().CanHide = false;
        }
    }
}
