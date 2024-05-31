using System;
using System.Collections;
using System.Collections.Generic;
using Character;
using UnityEngine;

public enum TilesType
{
    Normal,
    Falling,
    Broken,
    TNT,
    Spear,
    Rock,
    Axe,
    Cloud,
    Door
}
public class Tiles : MonoBehaviour
{
    [SerializeField] protected TilesType _tilesType;

    private float camEdge;
    // Player.
    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        camEdge = Camera.main.ScreenToViewportPoint(Vector3.zero).x;
    }

    // Update is called once per frame
    void Update()
    {
        CheckObjectOutOfCameraLeftEdge(1f);
    }

    void CheckObjectOutOfCameraLeftEdge(float delay = 0)
    {
        if (transform.position.x < camEdge)
        {
            Destroy(gameObject,delay);
        }
    }
    
    
}
