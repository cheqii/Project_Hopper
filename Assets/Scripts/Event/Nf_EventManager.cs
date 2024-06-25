using System;
using System.Collections.Generic;
using UnityEngine;

namespace Event
{
    public class Nf_EventManager : MonoBehaviour
    {
        public static Nf_EventManager _instance;

        public Nf_GameEventDictionary EventDictionary;
        private Dictionary<PlayerState, Nf_GameEvent> _dictionary;

        private void Awake()
        {
            if (_instance == null) _instance = this;
            else Destroy(gameObject);
        }

        private void OnEnable()
        {
            _dictionary = EventDictionary.ToDictionary();
        }

        private void OnDisable()
        {
            _dictionary.Clear();
        }
        
        public Nf_GameEvent GetEvent(PlayerState state)
        {
            _dictionary.TryGetValue(state, out var gameEvent);
            return gameEvent;
        }

        public void RaiseEvent(PlayerState state, Component sender = default, object data = default)
        {
            if (_dictionary.TryGetValue(state, out var gameEvent))
                gameEvent.Raise(sender, data);
        }
    }
}
