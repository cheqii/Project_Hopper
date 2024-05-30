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
    // Player.
    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // if()
    }
}
