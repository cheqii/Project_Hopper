using System;
using System.Collections;
using UnityEngine;

namespace TilesScript
{
    public class LoopingTile : TilesBlock
    {
        [SerializeField] protected Animator animator;
        
        [SerializeField] private float flashWarningTime;
        [SerializeField] private float holdAttackTime;

        [SerializeField] private bool isWarning;
        [SerializeField] private bool isAttacking;

        [SerializeField] private bool spearAttack;
        
        private WaitForSeconds _waiting;
        private WaitForSeconds _flashWarning;
        private WaitForSeconds _holdAttack;

        private void OnEnable()
        {
            StartCoroutine(LoopBehavior());
        }

        protected override void Start()
        {
            base.Start();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if(Type == TilesType.Cloud) return;
            if(spearAttack)
                SpearAttack();
        }

        public override void SetToInitialTile(Vector3 startPos = default)
        {
            base.SetToInitialTile(startPos);
            isWarning = false;
            isAttacking = false;
            spearAttack = false;
        }

        protected override void StartAction()
        {
            base.StartAction();
            _waiting = new WaitForSeconds(delay);
            _flashWarning = new WaitForSeconds(flashWarningTime);
            _holdAttack = new WaitForSeconds(holdAttackTime);
        }

        public void SpearAttack()
        {
            if (playerOnTile == null) return;
            _Player.TakeDamage(1);
            print("player take damage from spear tiles");
        }

        #region -Loop Behavior Methods-

        private void OnHold()
        {
            isWarning = true;
            spearAttack = false;
        }

        private void SetWarning()
        {
            isWarning = false;
            animator.SetTrigger("Flashing");
            isAttacking = true;
        }

        private void SetAction()
        {
            animator.SetTrigger("DoAction");
            isAttacking = false;
            spearAttack = true;
        }

        #endregion

        private IEnumerator LoopBehavior()
        {
            while (true)
            {
                OnHold();
                yield return _waiting;

                if(!isWarning) continue;
                SetWarning();
                yield return _flashWarning;
            
                if(!isAttacking) continue;
                SetAction();
                yield return _holdAttack;
            }
        }
    }
}
