using System;
using Character;
using Interaction;
using Interface;
using ObjectPool;
using UnityEngine;
using Random = UnityEngine.Random;

public class Chest : ObjectInGame, IInteraction
{
    [Header("Min-Max coin to spawn")]
    [SerializeField] private int minCoinSpawn;
    [SerializeField] private int maxCoinSpawn;

    [Header("Item Prefab to Spawn")]
    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private GameObject potionPrefab;
    
    [Header("Interactable Object")]
    [SerializeField] private InteractableObject interactableObject;

    [Header("Animator")]
    [SerializeField] private Animator animator;
    private void Start()
    {
        InitialObject();
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
    }

    protected override void TriggerAction(Player player)
    {
        base.TriggerAction(player);
    }

    private void InitialObject()
    {
        interactableObject.Interactable = this;
        animator.SetTrigger("Default");
    }
    public void InteractWithObject(int damage = default)
    {
        animator.SetTrigger("Interact");
        
        SpawnItem();
    }

    private void SpawnItem()
    {
        if (_player.Health != _player.MaxHealth)
        {
            // find 33% to spawn a health potion
            var randomChance = Random.Range(0, 1);
            
            if (randomChance <= 0.33f)
            {
                print("Yay! this chest drop a potion");
                Instantiate(potionPrefab, transform.position += Vector3.up * 2, Quaternion.identity);
            }
        }
        else
        {
            // spawn coins
            var randomCoin = Random.Range(minCoinSpawn, maxCoinSpawn);
            for (int i = 0; i <= randomCoin; i++)
            { 
                Instantiate(coinPrefab, transform.position += Vector3.up * 2, Quaternion.identity);
            }
        }
    }
}