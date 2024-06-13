using System;
using System.Collections;
using Character;
using Character.Monster;
using Interface;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class FlyMonster : Monster
{
    [SerializeField] private bool isWarning;
    [SerializeField] private bool isFlying;

    [Header("(Min - Max) Cooldown for random range")]
    [SerializeField] private float minCooldown = 2f;
    [SerializeField] private float maxCooldown = 4f;

    [Space]
    [SerializeField] private float warningTime = 0.66f;

    private Vector3 flyingPos;

    private void SetNullParent()
    {
        transform.SetParent(null);
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
    }

    protected override void TriggerAction()
    {
        base.TriggerAction();
        Invoke(nameof(SetNullParent), 0.5f); // to un-parent with tile and make the flying monster always follow in camera
        StartCoroutine(LoopBehavior());
    }

    public override void SetToInitialMonster(Vector3 startPos = default)
    {
        base.SetToInitialMonster(startPos);
        isAttacking = false;
        isWarning = false;
        isFlying = false;
    }

    public override void Attack()
    {
        base.Attack();
    }

    public override void TakeDamage(int damage)
    {
        animator.ResetTrigger("Attack");
        base.TakeDamage(damage);
        timer = 0;
        isAttacking = false;
        FlyingToThePlayerAndBack(flyingPos, 2f);
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

    #region -Monster Behavior-

    private void Clinging()
    {
        if (!isFlying)
        {
            flyingPos = new Vector3(3, transform.position.y + 1.5f);
            FlyingToThePlayerAndBack(flyingPos, 1f);

            isFlying = true;
        }
        
        isWarning = true;
        // print("Clinging");
    }

    private void WarningBehavior()
    {
        isWarning = false;
        animator.ResetTrigger("Warning");
        FlyingToThePlayerAndBack(playerDetect.transform.position, 1f);
        MonsterPreAttack();
        print("Warning");
    }

    private void AttackingBehavior()
    {
        animator.SetTrigger("Attack");
        isAttacking = false;

        Invoke(nameof(FlyingToThePlayerAndBack), 1f); // after the monster attack then flying back
        // print("Attack Behavior");
    }

    #endregion

    private IEnumerator LoopBehavior()
    {
        while (!isWarning)
        {
            cooldownAttack = Random.Range(minCooldown, maxCooldown);

            // if(!isAttacking)
            Clinging();
            
            //after isWarning is true wait for cooldownTime and call warning function
            yield return new WaitForSeconds(cooldownAttack);
                
            animator.SetTrigger("Warning");
            yield return new WaitForSeconds(warningTime); // warning for 0.66 secs
            
            WarningBehavior();

            // attack player behavior after warning function have call
            if (!isAttacking) continue;
            yield return new WaitForSeconds(preAttackDelay);
            
            // after the monster attack then flying back
            AttackingBehavior();

        }
    }
    
}
