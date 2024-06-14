using System.Collections;
using UnityEngine;

namespace TilesScript
{
    public class LoopingTile : TilesBlock
    {
        [SerializeField] private Animator animator;
        
        [SerializeField] private float flashWarningTime;
        [SerializeField] private float holdAttackTime;

        [SerializeField] private bool isWarning;
        [SerializeField] private bool isAttacking;
        
        protected WaitForSeconds _waiting;
        protected WaitForSeconds _flashWarning;
        protected WaitForSeconds _holdAttack;

        protected override void Start()
        {
            base.Start();
        }

        public override void SetToInitialTile(Vector3 startPos = default)
        {
            base.SetToInitialTile(startPos);
            isWarning = false;
            isAttacking = false;
        }

        protected override void StartAction()
        {
            base.StartAction();
            _waiting = new WaitForSeconds(delay);
            _flashWarning = new WaitForSeconds(flashWarningTime);
            _holdAttack = new WaitForSeconds(holdAttackTime);

            StartCoroutine(LoopBehavior());
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player") && !isAttacking)
                _Player.TakeDamage(1);
        }

        #region -Loop Behavior Methods-

        private void OnHold()
        {
            animator.SetTrigger("Default");
            isWarning = true;
        }

        private void SetWarning()
        {
            isWarning = false;
            animator.SetTrigger("Flashing");
            isAttacking = true;
        }

        private void SetAttack()
        {
            animator.SetTrigger("Attack");
            isAttacking = false;
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
                SetAttack();
                yield return _holdAttack;
            }
        }
    }
}
