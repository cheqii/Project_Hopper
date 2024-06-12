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
        [SerializeField] private Player playerDetect;
        
        [Header("Monster Type")]
        [SerializeField] private MonsterType monster;
        
        [Header("Interactable")]
        [SerializeField] private InteractableObject interactableObject;

        [Header("Animator")]
        [SerializeField] private Animator animator;

        [Space]
        [SerializeField] private bool isAttacking;
        
        private float timer;

        #region -Unity Event Methods-

        private void Update()
        {
            MonsterCooldown();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                playerDetect = GetPlayer(other);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
                playerDetect = null;
        }

        #endregion

        public void SetToInitialMonster()
        {
            interactableObject.Interactable = this;
            health = maxHealth;
        }

        public void Attack() // set this method in animation event
        {
            if(playerDetect == null) return;
            
            playerDetect.TakeDamage(attackDamage);
            monster.AttackBehavior();
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

        private Player GetPlayer(Component other = null)
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

        private void MonsterPreAttack()
        {
            isAttacking = true;
            timer = preAttackDelay;
            print("pre-attack");
        }

        private void MonsterCooldown()
        {
            if (!isAttacking && timer <= 0)
                MonsterPreAttack();
            
            if (isAttacking && playerDetect != null)
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
