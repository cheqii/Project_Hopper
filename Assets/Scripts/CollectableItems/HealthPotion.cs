using Character;
using ObjectPool;
using UnityEngine;

namespace CollectableItems
{
    public class HealthPotion : ObjectInGame.ObjectInGame
    {
        protected override void OnTriggerEnter2D(Collider2D other)
        {
            base.OnTriggerEnter2D(other);
        }

        protected override void TriggerAction(Player player)
        {
            base.TriggerAction(player);
            SoundManager.Instance.PlaySFX("Potion");
            player.FullHeal(1);
            GameManager._instance.UpdatePlayerHealthUI(true);
            PoolManager.ReleaseObject(gameObject);
        }
    }
}
