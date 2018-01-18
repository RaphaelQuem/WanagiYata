using Assets.Scripts.Extension;
using Assets.Scripts.StateMachine;
using UnityEngine;

public class NPCBehaviour : MonoBehaviour
{
    public bool IsColliding { get; private set; }

    void Start()
    {
    }

    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Equals("Player"))
        {
            other.GetComponent<PlayerBehaviour>().CurrentAction = Assets.Scripts.Resource.PlayerAction.Talk;
            other.GetComponent<PlayerBehaviour>().ActionTarget = gameObject;
            Debug.Log("Fala");

        }

    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag.Equals("Player"))
        {
            other.GetComponent<PlayerBehaviour>().CurrentAction = Assets.Scripts.Resource.PlayerAction.None;
        }
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        IsColliding = true;
    }
    public void OnCollisionExit2D(Collision2D collision)
    {
        IsColliding = false;
    }
}
