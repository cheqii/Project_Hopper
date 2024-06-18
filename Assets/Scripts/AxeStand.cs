using System;
using System.Collections;
using System.Collections.Generic;
using Character;
using DG.Tweening;
using Interaction;
using Interface;
using ObjectPool;
using UnityEngine;

public class AxeStand : ObjectInGame, IInteraction
{
    [Header("Interactable Object")]
    [SerializeField] private InteractableObject interactableObject;
    
    [SerializeField] private float waitingTime;
    private WaitForSeconds _wait;

    [SerializeField] private Vector3 axeRotateLeft;
    [SerializeField] private Vector3 axeRotateRight;

    [SerializeField] private Transform rotateObject;

    private void OnEnable()
    {
        StartCoroutine(LoopBehavior());
    }

    private void Start()
    {
        _wait = new WaitForSeconds(waitingTime);
    }

    public override void SetToInitialObject(Vector3 startPos = default)
    {
        base.SetToInitialObject(startPos);
        interactableObject.Interactable = this;
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
    }

    protected override void TriggerAction(Player player)
    {
        base.TriggerAction(player);
        player.TakeDamage(damage);
    }

    public void InteractWithObject(int damage = default)
    {
        PoolManager.ReleaseObject(gameObject);
    }

    private IEnumerator LoopBehavior()
    {
        while (true)
        {
            //rotate left
            rotateObject.transform.DOLocalRotate(axeRotateLeft, waitingTime);
            
            yield return _wait;
            
            //rotate right
            rotateObject.transform.DOLocalRotate(axeRotateRight, waitingTime);
            yield return _wait;
        }
    }
}
