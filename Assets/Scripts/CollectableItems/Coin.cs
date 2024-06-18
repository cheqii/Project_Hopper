using Character;
using UnityEngine;

namespace CollectableItems
{
    public class Coin : CollectableItem
    {
        private bool _collect;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                var player = other.GetComponent<Player>();
                TriggerAction(player);
            }
        }

        protected override void TriggerAction(Player player)
        {
            if(_collect) return;
            print("get coin");
            base.TriggerAction(player);
            _collect = false;
            GameManager._instance.UpdatePlayerScore(value);
            Destroy(gameObject, 1f);
        }
    }
}
