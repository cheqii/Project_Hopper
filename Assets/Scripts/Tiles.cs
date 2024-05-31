using System;
using System.Collections;
using System.Collections.Generic;
using Character;
using DG.Tweening;
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

    [SerializeField] private float delay;
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

    void CheckObjectOutOfCameraLeftEdge(float t = 0)
    {
        if (transform.position.x + 2 < camEdge)
        {
            gameObject.SetActive(false);
        }
    }
    
    
}
