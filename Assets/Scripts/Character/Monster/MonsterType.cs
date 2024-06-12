using Interface;
using UnityEngine;

namespace Character.Monster
{
    public class MonsterType : MonoBehaviour, IMonster
    {
        public virtual void AttackBehavior()
        {
            print("This is monster type attack");
        }
    }
}
