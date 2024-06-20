using Character;
using ObjectPool;
using UnityEngine;

namespace CollectableItems
{
    public class HealthPotion : CollectableItem
    {
        protected override void OnTriggerEnter2D(Collider2D other)
        {
            base.OnTriggerEnter2D(other);
        }

        protected override void TriggerAction(Player player)
        {
            base.TriggerAction(player);
            player.FullHeal(1);
            GameManager._instance.UpdatePlayerHealthUI(true);
            PoolManager.ReleaseObject(gameObject);
        }
    }
}
