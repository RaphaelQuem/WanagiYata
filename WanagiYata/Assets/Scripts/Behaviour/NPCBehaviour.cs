using Assets.Scripts.Extension;
using Assets.Scripts.StateMachine;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class NPCBehaviour : MonoBehaviour
{
    private const string dialogueSource = "Shilah.json";
    public bool IsColliding { get; private set; }

    void Start()
    {
        var x = string.Concat(StaticResources.DialogueFolder,dialogueSource) ;

        using (StreamReader reader = new StreamReader(x))
        {
            var y = reader.ReadToEnd();
            dynamic tree = JObject.Parse(y);
            var z = JsonConvert.DeserializeObject(y);
            var g = tree.days;

        }
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
