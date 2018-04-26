using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
using Assets;
using Assets.Scripts.Model;
using Assets.Scripts.Managers.Interactions;
using Newtonsoft.Json;
using System.Linq;

public class EventManager : MonoBehaviour
{

    private Dictionary<string, EventSeries> eventDictionary;
    private static EventInteraction interactionBase;
    private static EventManager eventManager;
    private static int interactionIterator = 0;
    private static int dialogueIterator = 0;
    public static EventManager instance
    {
        get
        {
            if (!eventManager)
            {
                eventManager = FindObjectOfType(typeof(EventManager)) as EventManager;

                if (!eventManager)
                {
                    Debug.LogError("There needs to be one active EventManger script on a GameObject in your scene.");
                }
                else
                {
                    eventManager.Init();
                }
            }

            return eventManager;
        }
    }

    void Init()
    {
        if (eventDictionary == null)
        {
            eventDictionary = new Dictionary<string, EventSeries>();
        }
    }

    public static void StartListening(string eventName, UnityAction<List<EventModel>, string> listener)
    {
        EventSeries thisEvent = null;
        if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.AddListener(listener);
        }
        else
        {
            thisEvent = new EventSeries();
            thisEvent.AddListener(listener);
            instance.eventDictionary.Add(eventName, thisEvent);
        }
    }

    public static void StopListening(string eventName, UnityAction<List<EventModel>, string> listener)
    {
        if (eventManager == null) return;
        EventSeries thisEvent = null;
        if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.RemoveListener(listener);
        }
    }

    public static string[] GetText(string eventName)
    {
        TextAsset asset = Resources.Load("Translations/Dialogues/PtBr/EventDialogues", typeof(TextAsset)) as TextAsset;
        interactionBase = JsonConvert.DeserializeObject<EventInteraction>(asset.text);

        var x = interactionBase.Interactions.FirstOrDefault(i => i.Id.Equals(eventName));


        if (dialogueIterator < x.Dialogues.Count() -1)
        {
            var y = x.Dialogues[dialogueIterator];



            if (interactionIterator >= y.Texts.Count())
            {
                y = x.Dialogues[++dialogueIterator];
                interactionIterator = 0;
            }

            return new string[] { y.Name, y.Texts[interactionIterator++] };
        }
        else
        {
            dialogueIterator = 0;
            interactionIterator = 0;
            return new string[] { "", "" };
        }
    }

    public static void TriggerEvent(string eventName, List<EventModel> eventList)
    {
        EventSeries thisEvent = null;
        if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.Invoke(eventList, eventName);
        }
    }
}