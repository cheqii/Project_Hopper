using Character;
using Character.Monster;
using UnityEngine;

public class FlyMonster : MonsterType
{
    public override void AttackBehavior()
    {
        Debug.Log("Fly Monster Attack");
    }
}
