using Character;
using UnityEngine;

namespace CollectableItems
{
    public class Coin : CollectableItem
    {
        protected override void OnTriggerEnter2D(Collider2D other)
        {
            base.OnTriggerEnter2D(other);
        }

        protected override void TriggerAction(Player player)
        {
            print("get coin");
            base.TriggerAction(player);
            GameManager._instance.UpdatePlayerScore(value);
            Destroy(gameObject);
        }
    }
}
