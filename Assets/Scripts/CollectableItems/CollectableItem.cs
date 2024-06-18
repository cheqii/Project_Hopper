using System;
using Character;
using UnityEngine;

namespace CollectableItems
{
    public class CollectableItem : MonoBehaviour
    {
        [SerializeField] protected int value;

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
