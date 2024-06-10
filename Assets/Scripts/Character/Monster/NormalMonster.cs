using System;
using ObjectPool;
using TilesScript;
using UnityEngine;

namespace Character.Monster
{
    public class NormalMonster : Monster
    {
        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        protected override void Attack()
        {
            base.Attack();
        }

        protected override void OnTriggerEnter2D(Collider2D other)
        {
            base.OnTriggerEnter2D(other);
            if (other.gameObject.CompareTag("Player"))
            {
                print("Detect player");
                var player = other.gameObject.GetComponent<Player>();
                player.TakeDamage(1);
            }
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                print("Detect player");
                var player = other.gameObject.GetComponent<Player>();
                player.IsGrounded = true;
                print(player.IsGrounded);
            }
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Ground"))
            {
                var tile = other.gameObject.GetComponent<TilesBlock>();
                if (tile.Type == TilesType.Normal) return;
                PoolManager.ReleaseObject(gameObject);
                print($"this is {tile.Type} tile");
            }
        }
    }
}
