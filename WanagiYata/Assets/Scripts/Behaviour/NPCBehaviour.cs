using Assets.Scripts.Extension;
using Assets.Scripts.StateMachine;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Assets.Scripts.Managers.Interactions;
using Assets.Scripts.Resource;

public class NPCBehaviour : MonoBehaviour
{
    public string dialogueSource;
    public NPCInteractionManager DialogueManager { get; private set; }
    public bool IsColliding { get; private set; }

    void Start()
    {
        TextAsset asset = Resources.Load("Translations/Dialogues/PtBr/" + dialogueSource, typeof(TextAsset)) as TextAsset;

        DialogueManager = JsonConvert.DeserializeObject<NPCInteractionManager>(asset.text);

    }

    void Update()
    {

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
}
