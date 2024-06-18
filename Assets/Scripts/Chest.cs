using System;
using Character;
using Interaction;
using Interface;
using ObjectPool;
using TilesScript;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class Chest : ObjectInGame, IInteraction
{
    [Header("Min-Max coin to spawn")]
    [SerializeField] private int minCoinSpawn;
    [SerializeField] private int maxCoinSpawn;

    [Header("Item Particle to Spawn")]
    [SerializeField] private ParticleSystem coinParticle;
    [SerializeField] private ParticleSystem potionParticle;
    
    [Header("Interactable Object")]
    [SerializeField] private InteractableObject interactableObject;

    [Space]
    [SerializeField] private bool isOpen;

    [Header("Animator")]
    [SerializeField] private Animator animator;

    private int amountCoin = 0;

    private void OnEnable()
    {
        SetToInitialObject();
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
    }

    protected override void TriggerAction(Player player)
    {
        base.TriggerAction(player);
    }

    public override void SetToInitialObject(Vector3 startPos = default)
    {
        base.SetToInitialObject(startPos);
        isOpen = false;
        interactableObject.Interactable = this;
        animator.SetTrigger("Default");
    }
    
    public void InteractWithObject(int damage = default)
    {
        if(isOpen) return;
        isOpen = true;
        animator.SetTrigger("Interact");
        SpawnItem();
    }

    private void SpawnCoin()
    {
        var randomCoin = Random.Range(minCoinSpawn, maxCoinSpawn);
        coinParticle.Emit(randomCoin);
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
                potionParticle.Emit(1);
            }
            else
                SpawnCoin();
        }
        else
            SpawnCoin();
    }
}