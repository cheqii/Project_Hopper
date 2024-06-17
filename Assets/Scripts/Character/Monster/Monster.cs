using System;
using System.Collections;
using Interaction;
using Interface;
using ObjectPool;
using UnityEngine;

namespace Character.Monster
{
    public class Monster : Character, IInteraction
    {
        [SerializeField] protected float preAttackDelay;
        [SerializeField] protected float cooldownAttack;
        [SerializeField] protected bool isStunned;

        [Header("Player")]
        [SerializeField] protected Player playerDetect;
        
        [Header("Interactable")]
        [SerializeField] private InteractableObject interactableObject;

        [Header("Animator")]
        [SerializeField] protected Animator animator;

        [Space]
        [SerializeField] protected bool isAttacking;

        protected WaitForSeconds _preAttack;
        protected WaitForSeconds _cooldownAttack;
        
        #region -Unity Event Methods-

        protected virtual void OnEnable()
        {
            // print("Hello player");
            // StartCoroutine(LoopBehavior());
        }

        private void OnDisable()
        {
            playerDetect = null;
        }

        private void Start()
        {
            _preAttack = new WaitForSeconds(preAttackDelay);
            _cooldownAttack = new WaitForSeconds(cooldownAttack);
        }

        protected virtual void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                playerDetect = GetPlayer(other);
                TriggerAction();
            }
        }
        
        #endregion

        public virtual void SetToInitialMonster(Vector3 startPos = default)
        {
            interactableObject.Interactable = this;
            health = maxHealth;
            transform.position = startPos;
        }

        public virtual void Attack() // set this method in animation event
        {
            if(playerDetect == null) return;
            
            playerDetect.TakeDamage(attackDamage);
        }

        public override void TakeDamage(int damage)
        {
            animator.SetTrigger("Hurt");
            base.TakeDamage(damage);
            
            if (health <= 0)
                animator.SetTrigger("Dead");
        }

        public void ReleaseMonster() // set in animation event to call after dead anim end
        {
            PoolManager.ReleaseObject(gameObject);
            playerDetect = null;
        }

        protected Player GetPlayer(Component other = null)
        {
            if (other != null)
            {
                var player = other.GetComponent<Player>();
                return player;
            }
            return null;
        }

        public void InteractWithObject(int damage = default)
        {
            TakeDamage(damage);
        }

        protected virtual void TriggerAction()
        {
            StartCoroutine(LoopBehavior());
        }

        #region -Attack Behavior Coroutine-

        protected virtual void AttackingBehavior()
        {
            animator.SetTrigger("Attack");
            isAttacking = false;
        }

        protected virtual IEnumerator LoopBehavior()
        {
            while (!isAttacking)
            {
                isAttacking = true;

                yield return _preAttack;
                AttackingBehavior();

                yield return _cooldownAttack;
            }
        }

        #endregion
    }
}
