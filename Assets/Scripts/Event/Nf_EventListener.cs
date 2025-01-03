using Event;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class Nf_CustomGameEvent : UnityEvent<Component, object>{} 

public class Nf_EventListener : MonoBehaviour
{
    public string eventName;
    public Nf_CustomGameEvent Response;

    private bool _isListenerRegistered;

    private void OnEnable()
    {
        RegisterListener();
        UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        if (Nf_EventManager._instance != null)
            UnregisterListener();
        UnityEngine.SceneManagement.SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, UnityEngine.SceneManagement.LoadSceneMode mode)
    {
        RegisterListener();
    }

    private void RegisterListener()
    {
        if (_isListenerRegistered || Nf_EventManager._instance == null) return;
        var gameEvent = Nf_EventManager._instance.GetEvent(eventName);
        
        if (gameEvent == null) return;
        gameEvent.RegisterListener(OnEventRaise);
        _isListenerRegistered = true;
    }

    private void UnregisterListener()
    {
        if (!_isListenerRegistered || Nf_EventManager._instance == null) return;
        var gameEvent = Nf_EventManager._instance.GetEvent(eventName);
        
        if (gameEvent == null) return;
        gameEvent.UnRegisterListener(OnEventRaise);
        _isListenerRegistered = false;
    }

    public void OnEventRaise(Component sender, object data)
    {
        Response.Invoke(sender, data);
    }
}
