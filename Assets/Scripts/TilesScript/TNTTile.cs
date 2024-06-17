using System.Collections.Generic;
using ObjectPool;
using UnityEngine;

namespace TilesScript
{
    public class TNTTile : TilesBlock
    {
        [Header("Animator")]
        [SerializeField] private Animator animator;
        [SerializeField] private bool checkExplodeTile;
        
        [SerializeField] private List<GameObject> foundToDestroy;

        public override void SetToInitialTile(Vector3 startPos = default)
        {
            base.SetToInitialTile(startPos);
            checkExplodeTile = false;
            foundToDestroy.Clear();
        }

        public override void CheckPlayerOnTile()
        {
            base.CheckPlayerOnTile();
            
            if(playerOnTile == null) return;
            if(checkExplodeTile) return;
            animator.SetTrigger("Flashing");
            Invoke(nameof(OnStep), delay);
        }

        protected override void OnCollisionEnter2D(Collision2D other)
        {
            base.OnCollisionEnter2D(other);
        }

        private void ReleaseObjectExploded()
        {
            var leftTile = PoolManager.GetInstanceByXPosition(transform.position.x - 1);
            var rightTile = PoolManager.GetInstanceByXPosition(transform.position.x + 1);

            if (leftTile != null)
                PoolManager.ReleaseObject(leftTile);

            if (rightTile != null)
                PoolManager.ReleaseObject(rightTile);
        }

        private void OnStep()
        {
            animator.SetTrigger("Explode");
            checkExplodeTile = true;
            Invoke(nameof(ReleaseObjectExploded), delay);
        }
    }
}
