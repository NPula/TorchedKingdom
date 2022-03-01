using System;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : Singleton<EventManager> 
{
  private Dictionary<string, Action<Dictionary<string, object>>> eventDictionary;

    void Init() 
    {
        if (eventDictionary == null) 
        {
            eventDictionary = new Dictionary<string, Action<Dictionary<string, object>>>();
        }
    }

    protected override void Awake()
    {
        // initialize the event dictionary
        base.Awake();
        Init();
    }

    public static void StartListening(string eventName, Action<Dictionary<string, object>> listener) 
    {
        Action<Dictionary<string, object>> thisEvent;
    
        if (Instance.eventDictionary.TryGetValue(eventName, out thisEvent)) 
        {
            thisEvent += listener;
            Instance.eventDictionary[eventName] = thisEvent;
        } 
        else 
        {
            thisEvent += listener;
            Instance.eventDictionary.Add(eventName, thisEvent);
        }
    }

    public static void StopListening(string eventName, Action<Dictionary<string, object>> listener) 
    {
        if (Instance == null) return;

        Action<Dictionary<string, object>> thisEvent;

        if (Instance.eventDictionary.TryGetValue(eventName, out thisEvent)) 
        {
            thisEvent -= listener;
            Instance.eventDictionary[eventName] = thisEvent;
        }
    }

    public static void TriggerEvent(string eventName, Dictionary<string, object> message) 
    {
        Action<Dictionary<string, object>> thisEvent = null;
        
        if (Instance.eventDictionary.TryGetValue(eventName, out thisEvent)) 
        {
            thisEvent.Invoke(message);
        }
    }
}