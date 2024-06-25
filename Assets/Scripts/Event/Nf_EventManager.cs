using System;
using System.Collections.Generic;
using UnityEngine;

namespace Event
{
    public class Nf_EventManager : MonoBehaviour
    {
        public static Nf_EventManager _instance;

        public Nf_GameEventDictionary EventDictionary;
        private Dictionary<string, Nf_GameEvent> eventDictionary;

        private void Awake()
        {
            if (_instance == null) _instance = this;
            else Destroy(gameObject);
        }

        private void OnEnable()
        {
            eventDictionary = EventDictionary.ToDictionary();
        }

        private void OnDisable()
        {
            eventDictionary.Clear();
        }
        
        public Nf_GameEvent GetEvent(string eventName)
        {
            eventDictionary.TryGetValue(eventName, out var gameEvent);
            return gameEvent;
        }

        public void RaiseEvent(string eventName, Component sender = default, object data = default)
        {
            if (eventDictionary.TryGetValue(eventName, out var gameEvent))
            {
                gameEvent.Raise(sender, data);
            }
        }
    }
}

[Serializable]
public class Nf_ListenerDictProperty
{
    public string key;
    public Nf_EventListener eventListener;
}

[Serializable]
public class Nf_EventListenerDictionary
{
    public List<Nf_ListenerDictProperty> listenerDict;
    public Dictionary<string, Nf_EventListener> ToDictionary()
    {
        Dictionary<string, Nf_EventListener> newListeners = new Dictionary<string, Nf_EventListener>();

        foreach (var dictionary in listenerDict)
        {
            newListeners.Add(dictionary.key, dictionary.eventListener);
        }

        return newListeners;
    }
}
