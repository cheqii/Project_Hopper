using System;
using ObjectPool;
using TilesScript;
using UnityEngine;
using UnityEngine.Serialization;

namespace Character.Monster
{
    public class Monster : Character
    {
        [SerializeField] protected float preAttackDelay;
        [SerializeField] protected float cooldownAttack;
        [SerializeField] protected bool isStunned;

        [SerializeField] private Player playerDetect;
        
        [SerializeField] private MonsterType monster;

        [Header("Animator")]
        [SerializeField] private Animator animator;

        void Start()
        {
            ResetMonsterStatus();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                playerDetect = GetPlayer(other);
                animator.SetTrigger("Attack");
                // Attack();
            }
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if(playerDetect != null)
                animator.SetTrigger("Attack");
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
                playerDetect = null;
        }

        public void ResetMonsterStatus()
        {
            health = maxHealth;
        }

        private void Attack() // set this method in animation event
        {
            if(playerDetect == null) return; 
            
            playerDetect.TakeDamage(attackDamage);
            monster.AttackBehavior();
        }

        public override void TakeDamage(int damage)
        {
            base.TakeDamage(damage);
            animator.SetTrigger("Hurt");
            
            if (health <= 0)
                animator.SetTrigger("Dead");
        }

        public void ReleaseMonster() // set in animation event to call after dead anim end
        {
            PoolManager.ReleaseObject(gameObject);
        }

        public void InteractToObject(int damage)
        {
            TakeDamage(damage);
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
    }
}
