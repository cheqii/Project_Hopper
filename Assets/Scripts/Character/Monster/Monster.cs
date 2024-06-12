using System;
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
        
        protected float timer;

        #region -Unity Event Methods-

        private void Update()
        {
            MonsterCooldown();
        }

        protected virtual void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                print("found player");
                playerDetect = GetPlayer(other);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
                playerDetect = null;
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
            print("attack monster");
            TakeDamage(damage);
        }

        #region -Pre-Attack & Delay Calculate-

        protected void MonsterPreAttack()
        {
            isAttacking = true;
            timer = preAttackDelay;
            print("pre-attack");
        }

        private void MonsterCooldown()
        {
            if(playerDetect == null) return;
            
            if (!isAttacking && timer <= 0)
                MonsterPreAttack();
            
            if (isAttacking)
            {
                timer -= Time.deltaTime;
                if (timer <= 0f)
                {
                    animator.SetTrigger("Attack");
                    isAttacking = false;
                    timer = cooldownAttack;
                    print("attack and cooldown");
                }
            }
            else
            {
                timer -= Time.deltaTime;
                if (timer <= 0f)
                {
                    timer = 0f;
                }
            }
        }

        #endregion
    }
}
