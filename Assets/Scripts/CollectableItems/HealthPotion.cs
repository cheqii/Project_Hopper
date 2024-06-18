using Character;
using UnityEngine;

namespace CollectableItems
{
    public class HealthPotion : CollectableItem
    {
        private bool _collect;

        protected override void OnTriggerEnter2D(Collider2D other)
        {
            base.OnTriggerEnter2D(other);
        }

        protected override void TriggerAction(Player player)
        {
            base.TriggerAction(player);
            player.FullHeal();
            GameManager._instance.UpdatePlayerHealthUI(true);
            Destroy(gameObject);
        }
    }
}
