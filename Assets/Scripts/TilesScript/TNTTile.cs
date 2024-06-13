using System;
using System.Collections.Generic;
using Interface;
using ObjectPool;
using DG.Tweening;
using UnityEngine;

namespace TilesScript
{
    public class TNTTile : TilesBlock, IOnStep
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

        protected override void CheckPlayerOnTile()
        {
            base.CheckPlayerOnTile();
            if(checkExplodeTile) return;
            animator.SetTrigger("Flashing");
            Invoke(nameof(OnStep), delay);
        }

        protected override void OnCollisionEnter2D(Collision2D other)
        {
            if (!other.gameObject.CompareTag("Player")) return;
            base.OnCollisionEnter2D(other);
        }

        private void OnTriggerEnter2D(Collider2D other) // to check the nearest tile +- 1 tile block
        {
            if(!checkExplodeTile) return;
            if (other.CompareTag("Player") || other.CompareTag("Detector") || other.CompareTag("TNT")) return;
            
            foundToDestroy.Add(other.gameObject);
            Invoke(nameof(ReleaseObjectExploded), delay);
        }

        private void ReleaseObjectExploded()
        {
            foreach (var obj in foundToDestroy)
            {
                if(obj == null) continue;
                PoolManager.ReleaseObject(obj);
            }
        }

        public void OnStep()
        {
            animator.SetTrigger("Explode");
            checkExplodeTile = true;
        }
    }
}
