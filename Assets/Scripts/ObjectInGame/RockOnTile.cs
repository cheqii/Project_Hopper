using System;
using Character;
using TilesScript;
using UnityEngine;

namespace ObjectInGame
{
    public class RockOnTile : ObjectInGame
    {
        [SerializeField] private TilesBlock _tilesBlock;

        private void Start()
        {
            _player = _tilesBlock._Player;
        }

        protected override void OnTriggerEnter2D(Collider2D other)
        {
            base.OnTriggerEnter2D(other);
        }

        protected override void TriggerAction(Player player)
        {
            base.TriggerAction(player);
            _player.TakeDamage(value);
        }
    }
}
