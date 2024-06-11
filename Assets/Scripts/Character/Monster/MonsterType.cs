using Interface;
using UnityEngine;

namespace Character.Monster
{
    public class MonsterType : MonoBehaviour, IMonster
    {
        public virtual void Attack()
        {
            print("This is monster type attack");
        }
    }
}
