using Character;
using ObjectPool;
using UnityEngine;

namespace CollectableItems
{
    public class Coin : ObjectInGame.ObjectInGame
    {
        protected override void OnTriggerEnter2D(Collider2D other)
        {
            base.OnTriggerEnter2D(other);
        }

        protected override void TriggerAction(Player player)
        {
            base.TriggerAction(player);
            SoundManager.Instance.PlaySFX("GetCoin");
            GameManager._instance.UpdatePlayerScore(value);
            PoolManager.ReleaseObject(gameObject);
        }
    }
}
