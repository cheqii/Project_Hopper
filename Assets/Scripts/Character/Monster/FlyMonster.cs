using System.Collections;
using Character;
using Character.Monster;
using Interface;
using DG.Tweening;
using UnityEngine;

public class FlyMonster : Monster, ILooping
{
    [Header("(Min - Max) Cooldown for random range")]
    [SerializeField] private float minCooldown = 2f;
    [SerializeField] private float maxCooldown = 4f;

    [SerializeField] private float warningTime = 0.66f;

    private bool isWarning;
    
    private void Update()
    {
        MonsterCooldown();
    }
    
    public override void Attack()
    {
        base.Attack();
    }

    public void SetWarning()
    {
        isWarning = true;
        timer = cooldownAttack;
        print("set warning");
    }

    public IEnumerator LoopBehavior()
    {
        // cooldownAttack = Random.Range(2, 4);
        return null;
    }

    protected override void MonsterCooldown()
    {
        cooldownAttack = Random.Range(minCooldown, maxCooldown);
        
        if(playerDetect == null) return;
            
        // if (!isAttacking && timer <= 0)
        //     MonsterPreAttack();
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
