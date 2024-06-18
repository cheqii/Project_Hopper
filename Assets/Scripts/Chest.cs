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

    [SerializeField] private bool isOpen;
    [SerializeField] private bool standOnChest;
    
    [Header("Animator")]
    [SerializeField] private Animator animator;

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
    }

    protected override void TriggerAction(Player player)
    {
        base.TriggerAction(player);
        standOnChest = true;
    }

    public override void SetToInitialObject(Vector3 startPos = default)
    {
        base.SetToInitialObject(startPos);
        interactableObject.Interactable = this;
        animator.SetTrigger("Default");
        isOpen = false;
    }
    
    public void InteractWithObject(int damage = default)
    {
        if(isOpen) return;
        // if (!standOnChest) return;
        isOpen = true;
        animator.SetTrigger("Interact");
        SpawnItem();
    }

    private void SpawnCoin()
    {
        var randomCoin = Random.Range(minCoinSpawn, maxCoinSpawn);
        print($"spawn {randomCoin} coin");
        for (int i = 0; i < randomCoin; i++)
        { 
            var coin = Instantiate(coinPrefab, transform.position, Quaternion.identity);
            coin.transform.SetParent(transform);
        }
    }

    private void SpawnItem()
    {
        if (_player.Health != _player.MaxHealth)
        {
            // find 33% to spawn a health potion
            var randomChance = Random.Range(0, 100);
            
            if (randomChance <= 33)
            {
                print("Yay! this chest drop a potion");
                var potion = Instantiate(potionPrefab, transform.position, Quaternion.identity);
                potion.transform.SetParent(transform);
            }
            else
                SpawnCoin();
        }
        else
            SpawnCoin();
    }
}