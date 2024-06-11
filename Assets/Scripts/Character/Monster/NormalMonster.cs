using System;
using ObjectPool;
using TilesScript;
using UnityEngine;

namespace Character.Monster
{
    public class NormalMonster : MonsterType
    {
        public override void Attack()
        {
            Debug.Log("Normal Monster Attack");
        }
    }
}
