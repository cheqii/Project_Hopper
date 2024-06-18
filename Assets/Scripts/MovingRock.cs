using System;
using System.Collections;
using System.Collections.Generic;
using Character;
using DG.Tweening;
using UnityEngine;

public class MovingRock : ObjectInGame
{
    [SerializeField] private float interval; 
    private WaitForSeconds _movingInterval;
    
    [Header("Position to move rock")]
    [SerializeField] private float yTopPosition;
    [SerializeField] private float yBottomPosition;

    [Space]
    [SerializeField] private bool atTop;
    public bool AtTop
    {
        get => atTop;
        set => atTop = value;
    }
    
    [SerializeField] private bool atBottom;
    public bool AtBottom
    {
        get => atBottom;
        set => atBottom = value;
    }
    
    private void OnEnable()
    {
        StartCoroutine(LoopBehavior());
    }

    private void Start()
    {
        _movingInterval = new WaitForSeconds(interval);
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

    private void MoveRockDown()
    {
        transform.DOLocalMoveY(yBottomPosition, interval);
        atTop = false;
        atBottom = true;
    }

    private void MoveRockUp()
    {
        transform.DOLocalMoveY(yTopPosition, interval);
        atTop = true;
        atBottom = false;
    }

    private IEnumerator LoopBehavior()
    {
        while (true)
        {
            yield return _movingInterval;
            MoveRockDown();

            yield return _movingInterval;
            
            if(!atBottom) continue;
            MoveRockUp();
        }
    }
}
