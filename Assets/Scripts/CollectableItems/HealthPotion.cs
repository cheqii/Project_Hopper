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
            if(!_collect) return;
            print("get health");
            base.TriggerAction(player);
            _collect = true;
            player.IncreasePlayerHealth(value);
            Destroy(gameObject, 1f);
        }
    }
}
