using Assets.Scripts.Extension;
using Assets.Scripts.StateMachine;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Assets.Scripts.Managers.Interactions;
using Assets.Scripts.Resource;
using Assets.Scripts.Model;
using System.Collections.Generic;

public class NPCBehaviour : MonoBehaviour
{
    public string dialogueSource;
    private List<EventModel> actionList;
    public NPCInteractionManager DialogueManager { get; private set; }
    public bool IsColliding { get; private set; }

    void Start()
    {
        actionList = new List<EventModel>();
        TextAsset asset = Resources.Load("Translations/Dialogues/PtBr/" + dialogueSource, typeof(TextAsset)) as TextAsset;

        DialogueManager = JsonConvert.DeserializeObject<NPCInteractionManager>(asset.text);
        EventManager.StartListening("teste", EventResponse);
    }

    void Update()
    {
        if (actionList.Count > 0)
        {
            var current = actionList[0];

            if (current != null)
            {
                if (current.DestinationX > 0 || current.DestinationY > 0)
                    MovementEvent(current);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Equals("Player"))
        {
            other.GetComponent<PlayerBehaviour>().CurrentAction = PlayerAction.Talk;
            other.GetComponent<PlayerBehaviour>().ActionTarget = gameObject;
            Debug.Log("Fala");

        }

    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag.Equals("Player"))
        {
            other.GetComponent<PlayerBehaviour>().CurrentAction = PlayerAction.None;
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
    public void EventResponse(List<EventModel> list, string eventName)
    {
            actionList = list;
    }
    private void MovementEvent(EventModel current)
    {
        if (current.Target != null && current.Target.Contains(dialogueSource))
        {
            if (current.DestinationX != null || current.DestinationY != null)
            {
                Vector2 currentObjective = new Vector2((int)current.DestinationX, (int)current.DestinationY);

                Vector2 movVector = currentObjective - (Vector2)this.gameObject.transform.position;
                if (movVector.x < 0.05 && movVector.y < 0.05)
                {
                    actionList.Remove(current);
                    return;
                }
                //_player.stateMch.Directorvector = movVector.normalized;
                this.gameObject.transform.position = this.gameObject.transform.position + (Vector3)movVector.normalized * Time.deltaTime * 3.5f;
            }
        }
    }
}
