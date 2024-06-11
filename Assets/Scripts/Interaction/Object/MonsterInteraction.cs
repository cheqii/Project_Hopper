using Character.Monster;
using UnityEngine;

namespace Interaction.Object
{
    public class MonsterInteraction : Interactable
    {
        [SerializeField] private Monster monster;
        public override void InteractWithObject(int damage = default)
        {
            print("attack monster");
            monster.TakeDamage(damage);
        }
    }
}
