using System;
using System.Collections;
using Character;
using Character.Monster;
using Interface;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class FlyMonster : Monster, ILooping
{
    [SerializeField] private bool isWarning;
    [SerializeField] private bool isFlying;

    [Header("(Min - Max) Cooldown for random range")]
    [SerializeField] private float minCooldown = 2f;
    [SerializeField] private float maxCooldown = 4f;

    [Space]
    [SerializeField] private float warningTime = 0.66f;


    private Vector3 flyingPos;

    private void Update()
    {
        LoopBehavior();
    }

    private void SetNullParent()
    {
        transform.SetParent(null);
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
        if(other.CompareTag("Player"))
            Invoke(nameof(SetNullParent), 0.5f); // to un-parent with tile and make the flying monster always follow in camera
    }

    // have to call this function because after flying monster attack and fly back player = null

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            playerDetect = GetPlayer(other);
    }

    public override void SetToInitialMonster(Vector3 startPos = default)
    {
        base.SetToInitialMonster(startPos);
        isFlying = false;
    }

    public override void Attack()
    {
        base.Attack();
    }

    #region -Flying Method-

    private void FlyingToThePlayerAndBack()
    {
        // use for flying back with invoke
        transform.DOMove(flyingPos, 1f);
    }
    private void FlyingToThePlayerAndBack(Vector3 destination, float duration)
    {
        // use for custom flying without invoke
        transform.DOMove(destination, duration);
    }

    #endregion

    private void SetWarning()
    {
        isWarning = true;
        timer = cooldownAttack;

        if (!isFlying)
        {
            flyingPos = new Vector3(3, transform.position.y + 1.5f);
            FlyingToThePlayerAndBack(flyingPos, 1f);

            isFlying = true;
        }
        print("set warning");
    }

    public void LoopBehavior()
    {
        if(playerDetect == null) return;
        
        cooldownAttack = Random.Range(minCooldown, maxCooldown);

        if(!isWarning && timer <= 0)
            SetWarning();
        
            
        if (isWarning)
        {
            timer -= Time.deltaTime;
            if (timer <= 0f)
            {
                animator.SetTrigger("Warning");
                isWarning = false;
                MonsterPreAttack();
                FlyingToThePlayerAndBack(playerDetect.transform.position, 1f);
                print("warning done");
            }
        }
        else if (isAttacking)
        {
            timer -= Time.deltaTime;
            if (timer <= 0f)
            {
                animator.SetTrigger("Attack");
                isAttacking = false;
                timer = cooldownAttack;
                
                Invoke(nameof(FlyingToThePlayerAndBack), 1f); // after the monster attack then flying back
                print("attack and cooldown");
            }
        }
        else
        {
            timer -= Time.deltaTime;
            if (timer <= 0f)
            {
                timer = 0f;
            }
        }
    }
    
}
