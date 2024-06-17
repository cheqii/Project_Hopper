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
        public bool IsFalling => isFalling;

        public override void SetToInitialTile(Vector3 startPos = default)
        {
            base.SetToInitialTile(startPos);
            isFalling = false;
        }

        protected override void CheckPlayerOnTile()
        {
            base.CheckPlayerOnTile();
            if(playerOnTile == null) return;
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
            isFalling = true;
            var endPos = new Vector3(transform.position.x, Vector3.down.y * 4.5f);
            transform.DOMove(endPos, 1f);
        }
    }
}
