using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
using Assets;
using Assets.Scripts.Model;
using Assets.Scripts.Managers.Interactions;
using Newtonsoft.Json;

public class EventManager : MonoBehaviour
{

    private Dictionary<string, EventSeries> eventDictionary;
    private static EventInteraction interaction;
    private static EventManager eventManager;

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

    public static void StartListening(string eventName, UnityAction<List<EventModel>> listener)
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

    public static void StopListening(string eventName, UnityAction<List<EventModel>> listener)
    {
        if (eventManager == null) return;
        EventSeries thisEvent = null;
        if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.RemoveListener(listener);
        }
    }

    public static string GetText()
    {
        TextAsset asset = Resources.Load("Translations/Dialogues/PtBr/EventDialogues", typeof(TextAsset)) as TextAsset;

        interaction = JsonConvert.DeserializeObject<EventInteraction>(asset.text);
        return "";
    }

    public static void TriggerEvent(string eventName, List<EventModel> eventList)
    {
        EventSeries thisEvent = null;
        if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.Invoke(eventList);
        }
    }
}