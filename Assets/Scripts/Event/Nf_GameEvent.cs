using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Event
{
    [Serializable]
    public class Nf_GameEvent
    {
        private UnityEvent<Component, object> _event = new Nf_CustomGameEvent();

        public void Raise(Component sender, object data)
        {
            _event.Invoke(sender, data);
        }

        public void RegisterListener(UnityAction<Component, object> listener)
        {
            _event.AddListener(listener);
        }

        public void UnRegisterListener(UnityAction<Component, object> listener)
        {
            _event.RemoveListener(listener);
        }
    }

    [Serializable]
    public class Nf_GameEventDictProperty
    {
        public PlayerState key;
        public Nf_GameEvent gameEvent;
    }

    [Serializable]
    public class Nf_GameEventDictionary
    {
        public List<Nf_GameEventDictProperty> eventDict;

        public Dictionary<PlayerState, Nf_GameEvent> ToDictionary()
        {
            Dictionary<PlayerState, Nf_GameEvent> newEvent = new Dictionary<PlayerState, Nf_GameEvent>();

            foreach (var dictionary in eventDict)
            {
                newEvent.Add(dictionary.key, dictionary.gameEvent);
            }

            return newEvent;
        }

    }
}