using System;
using System.Collections;
using Character;
using Interface;
using ObjectPool;
using UnityEngine;
using UnityEngine.InputSystem;

namespace TilesScript
{
    public class BrokenTile : TilesBlock
    {
        protected override void CheckPlayerOnTile()
        {
            base.CheckPlayerOnTile();
            Invoke(nameof(OnStep), delay);
        }

        protected override void OnCollisionEnter2D(Collision2D other)
        {
            base.OnCollisionEnter2D(other);
        }

        private void OnStep()
        {
            ReleaseTile();
        }
    }
}
