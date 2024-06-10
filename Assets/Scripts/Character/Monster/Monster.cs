using System;
using TilesScript;
using UnityEngine;

namespace Character.Monster
{
    public class Monster : Character
    {
        [SerializeField] protected float preAttackDelay;
        [SerializeField] protected float cooldownAttack;
        [SerializeField] protected bool isStunned;
        
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
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
            if (other.collider.CompareTag("Ground"))
            {
                var tile = other.gameObject.GetComponent<TilesBlock>();
                if(tile.Type == TilesType.Normal) return;
                gameObject.SetActive(false);
                print("monster touch tiles");
            }
            // gameObject.transform.parent.gameObject.SetActive(true);
        }
    }
}
