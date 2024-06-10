using System;
using System.Collections;
using Character;
using ObjectPool;
using UnityEngine;
using UnityEngine.InputSystem;

namespace TilesScript
{
    public class BrokenTiles : TilesBlock
    {
        protected override void CheckPlayerOnTile()
        {
            base.CheckPlayerOnTile();
            StartCoroutine(Broken(delay));
        }

        protected override void OnCollisionEnter2D(Collision2D other)
        {
            base.OnCollisionEnter2D(other);
        }

        IEnumerator Broken(float t)
        {
            yield return new WaitForSeconds(t);
            PoolManager.ReleaseObject(gameObject);
            print("broken Tile");
        }
    }
}
