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
            Invoke(nameof(BrokeTile), delay);
        }

        protected override void OnCollisionEnter2D(Collision2D other)
        {
            if (!other.gameObject.CompareTag("Player")) return;
            base.OnCollisionEnter2D(other);
        }

        void BrokeTile()
        {
            PoolManager.ReleaseObject(gameObject);
            print("broken Tile");
        }
    }
}
