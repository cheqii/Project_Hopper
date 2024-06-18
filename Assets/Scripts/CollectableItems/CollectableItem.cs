using System;
using Character;
using ObjectPool;
using UnityEngine;

namespace CollectableItems
{
    public class CollectableItem : MonoBehaviour
    {
        [SerializeField] protected int value;
        [SerializeField] protected Vector2 boxSize;
        [SerializeField] protected float castDistance;
        [SerializeField] private LayerMask playerLayer;
        
        protected bool GetPlayer()
        {
            return Physics2D.BoxCast(transform.position, boxSize, 0f, Vector2.down, castDistance, playerLayer);
        }
        
        private void OnDrawGizmos()
        {
            Gizmos.DrawWireCube(transform.position - transform.up * castDistance, boxSize);
        }

        protected virtual void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                var player = other.GetComponent<Player>();
                TriggerAction(player);
            }
        }

        protected virtual void TriggerAction(Player player)
        {
            
        }
    }
}
