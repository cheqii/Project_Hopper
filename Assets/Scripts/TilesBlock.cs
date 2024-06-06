using System;
using System.Collections;
using System.Collections.Generic;
using Character;
using DG.Tweening;
using ObjectPool;
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

public sealed class TilesBlock : MonoBehaviour
{
    private float camEdgeX;

    [SerializeField] private float delay;
    // Player.
    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        camEdgeX = Camera.main.ScreenToViewportPoint(Vector3.zero).x;
    }

    // Update is called once per frame
    void Update()
    {
        CheckObjectOutOfCameraLeftEdge(3f);
    }

    void CheckObjectOutOfCameraLeftEdge(float t = 0)
    {
        if (transform.position.x + 1 < camEdgeX)
        {
            PoolManager.ReleaseObject(gameObject);
        }

        // if (transform.position.x - 9 > camEdgeX)
        // {
        //     PoolManager.ReleaseObject(gameObject);
        //     // Destroy(gameObject);
        // }
    }

    public void BlockBehavior()
    {
        
    }


}
