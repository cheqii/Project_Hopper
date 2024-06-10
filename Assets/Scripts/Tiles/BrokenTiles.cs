using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Character;
using ObjectPool;
using UnityEngine;

public class BrokenTiles : TilesBlock
{
    [SerializeField] private float time = 0.5f;

    protected override void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if(other.gameObject.GetComponent<Player>().IsGrounded)
                StartCoroutine(Broken(time));
        }
    }

    IEnumerator Broken(float t)
    {
        yield return new WaitForSeconds(t);
        PoolManager.ReleaseObject(gameObject);
        print("broken Tile");
    }
}