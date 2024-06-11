using System;
using ObjectPool;
using TilesScript;
using UnityEngine;

namespace Character.Monster
{
    public class Monster : Character
    {
        [SerializeField] protected float preAttackDelay;
        [SerializeField] protected float cooldownAttack;
        [SerializeField] protected bool isStunned;
        
        void Start()
        {
            ResetMonsterStatus();
        }

        public void ResetMonsterStatus()
        {
            health = maxHealth;
        }

        public override void TakeDamage(int damage)
        {
            base.TakeDamage(damage);
            if(health <= 0)
                PoolManager.ReleaseObject(gameObject);
        }

        protected virtual void Attack()
        {
            print("attack player");
        }

        protected virtual void OnTriggerEnter2D(Collider2D other)
        {
            if(other.CompareTag("Player"))
                Attack();
        }

        protected void OnCollisionEnter2D(Collision2D other)
        {
            
        }
    }
}
