using System;
using System.Collections;
using System.Collections.Generic;
using Character;
using UnityEngine;

public class ObjectInGame : MonoBehaviour
{
    [SerializeField] protected Player _player;
    [SerializeField] protected int damage;

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            var player = other.gameObject.GetComponent<Player>();
            TriggerAction(player);
        }
    }

    protected virtual void TriggerAction(Player player)
    {
        
    }
}
