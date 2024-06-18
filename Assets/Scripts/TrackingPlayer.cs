using System;
using System.Collections;
using System.Collections.Generic;
using Character;
using UnityEngine;

public class TrackingPlayer : MonoBehaviour
{
    [SerializeField] private Player _player;

    private Vector3 trackingPos;
    // Start is called before the first frame update
    void Start()
    {
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
        transform.position = Tracking(_player.transform.position.y);
    }
}
