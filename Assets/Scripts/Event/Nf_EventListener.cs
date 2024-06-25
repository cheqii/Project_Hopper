using Event;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

[System.Serializable]
public class Nf_CustomGameEvent : UnityEvent<Component, object>{} 

public class Nf_EventListener : MonoBehaviour
{
    public PlayerState state;
    public Nf_CustomGameEvent Response;

    private bool _isListenerRegistered;

    private void OnEnable()
    {
        RegisterListener();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        if (Nf_EventManager._instance != null)
            UnregisterListener();
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        RegisterListener();
    }

    private void RegisterListener()
    {
        if (_isListenerRegistered || Nf_EventManager._instance == null) return;
        var gameEvent = Nf_EventManager._instance.GetEvent(state);
        
        if (gameEvent == null) return;
        gameEvent.RegisterListener(OnEventRaise);
        _isListenerRegistered = true;
    }

    private void UnregisterListener()
    {
        if (!_isListenerRegistered || Nf_EventManager._instance == null) return;
        var gameEvent = Nf_EventManager._instance.GetEvent(state);
        
        if (gameEvent == null) return;
        gameEvent.UnRegisterListener(OnEventRaise);
        _isListenerRegistered = false;
    }

    public void OnEventRaise(Component sender, object data)
    {
        Response.Invoke(sender, data);
    }
}
