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

        [SerializeField] private MonsterType monster;

        void Start()
        {
            ResetMonsterStatus();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if(other.CompareTag("Player"))
                Attack();
        }

        public void ResetMonsterStatus()
        {
            health = maxHealth;
        }

        private void Attack()
        {
            monster.Attack();
        }

        public override void TakeDamage(int damage)
        {
            base.TakeDamage(damage);
            // if(health <= 0)
            //     PoolManager.ReleaseObject(gameObject);
        }

        public void InteractToObject(int damage)
        {
            TakeDamage(damage);
        }
    }
}
