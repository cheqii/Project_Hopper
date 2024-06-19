using System;
using System.Collections;
using System.Collections.Generic;
using Character;
using UnityEngine;

public class TrackingPlayer : MonoBehaviour
{
    [SerializeField] private Player _player;

    [SerializeField] private ObjectInGame.ObjectInGame fireball;
    
    private Vector3 trackingPos;
    public Vector3 TrackingPos => trackingPos;
    
    // Start is called before the first frame update
    void Start()
    {
        _player = fireball._Player;
        trackingPos = new Vector3(transform.position.x, _player.transform.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        // transform.position = new Vector3(transform.position.x, _player.transform.position.y);
    }

    private void LateUpdate()
    {
        WarningTracking();
    }

    private Vector3 Tracking(float playerYPosition)
    {
        trackingPos.y = playerYPosition;
        return trackingPos;
    }

    public void WarningTracking()
    {
        if(_player == null) return;
        transform.position = Tracking(_player.transform.position.y);
    }
}
