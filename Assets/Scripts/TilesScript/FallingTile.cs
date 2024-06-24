using System;
using Interface;
using DG.Tweening;
using ObjectPool;
using UnityEngine;

namespace TilesScript
{
    public class FallingTile : TilesBlock
    {
        [Header("Animator")]
        [SerializeField] private Animator animator;

        [SerializeField] private bool isFalling;

        public override void SetToInitialTile(Vector3 startPos = default)
        {
            base.SetToInitialTile(startPos);
            isFalling = false;
        }

        public override void ReleaseTile()
        {
            if(isFalling)
                base.ReleaseTile();
        }

        public override void CheckPlayerOnTile()
        {
            base.CheckPlayerOnTile();
            if(playerOnTile == null) return;
            isFalling = true;
            animator.SetTrigger("Flashing");
            Invoke(nameof(OnStep), delay);
        }

        protected override void OnCollisionEnter2D(Collision2D other)
        {
            base.OnCollisionEnter2D(other);
        }

        private void OnStep()
        {
            animator.ResetTrigger("Flashing");
            var endPos = new Vector3(transform.position.x, Vector3.down.y * 4.5f);
            transform.DOMove(endPos, 1f);
        }
    }
}
