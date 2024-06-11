using System.Collections;
using System.Collections.Generic;
using Character.Monster;
using ObjectPool;
using TilesScript;
using UnityEngine;
using UnityEngine.Serialization;

public enum InteractableType
{
    None,
    Chest,
    Door,
    Object,
    Monster
}

public class Interactable : MonoBehaviour
{
    [SerializeField] private InteractableType type = InteractableType.None;
    public InteractableType Type => type;

    [SerializeField] private MonoBehaviour anyObject;

    public void InteractToMonster(int damage)
    {
        Monster monster = anyObject as Monster;
        if (monster != null) monster.TakeDamage(damage);
        print($"attack {monster.name}");
    }
    public void InteractToObject()
    {
        PoolManager.ReleaseObject(gameObject);
        print("released");
    }
}
