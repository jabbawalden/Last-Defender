﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrongDemon : Enemy
{

    private void OnEnable()
    {
        //MAYBE an event for death
        
    }

    private void OnDisable()
    {
        
    }
    // Use this for initialization
    void Start ()
    {
        if (gameManager.deadEnemies.Contains(enemyID))
        {
            Destroy(gameObject);
            return;
        }
        PlayerStrike = false;
    }

    protected override void MovementPattern()
    {
        if (Agent != null)
        Agent.SetDestination(Player.transform.position);
    }

    // Update is called once per frame
    void Update ()
    {
        GetDistance();

        switch (enemyState)
        {
            case EnemyState.Run:
                RunBehaviour();
                break;
            case EnemyState.Attack:
                AttackBehaviour();
                break;
            case EnemyState.Death:
                StartCoroutine(DeathBehaviour());
                break;
            case EnemyState.Idle:
                IdleBehaviour();
                break;

        }

        if (CurrentHealth < MaxHealth)
        {
            attackPlayer = true;
        }

        if (DistanceToPlayer > aggressionDistance)
        {
            enemyState = EnemyState.Idle;
            EnemyAnimator.SetBool("Run", false);
            Agent.velocity = Vector3.zero;
        }

        if (!attackPlayer && DistanceToPlayer <= aggressionDistance)
        {
            enemyState = EnemyState.Run;
        }
        else if (attackPlayer)
        {
            enemyState = EnemyState.Run;
        }

        if (DistanceToPlayer <= 3f)
        {
            enemyState = EnemyState.Attack;
        }

        if (CurrentHealth <= 0)
        {
            enemyState = EnemyState.Death;
        }

        /*
		if (Health <= 0)
        {
            OnKill();
            //can't call event here
        }
        
        if (AgressionFieldActive)
        {
            MovementPattern();
        }
        */
    }

    /*
    public void OnKill()
    {
        if (!gameManager.deadEnemies.Contains(enemyID))
        {
            gameManager.deadEnemies.Add(enemyID);
            //death
        }
    }
    */
    public void IdleBehaviour()
    {
        Agent.velocity = Vector3.zero;
    }

    public void ShoutBehaviour()
    {
        Agent.velocity = Vector3.zero;
    }

    public void RunBehaviour()
    {
        if (EnemyAnimator.GetBool("Idle to Attack1") == false)
        {
            StartCoroutine(RunRoutine());
        }
        PlayGrowl();
    }

    IEnumerator RunRoutine()
    {
        EnemyAnimator.SetBool("Run", true);
        yield return new WaitForSeconds(0.2f);
        MovementPattern();
    }

    public void AttackBehaviour()
    {
        EnemyAnimator.SetBool("Run", false);
        Agent.velocity = Vector3.zero;
        if (Time.time > NewFireRate)
        {
            NewFireRate = Time.time + FireRate;
            StartCoroutine(AttackRoutine());
        } 
    }

    IEnumerator AttackRoutine()
    {
        yield return new WaitForSeconds(0.2f);
        EnemyAnimator.SetBool("Idle to Attack1", true);
        yield return new WaitForSeconds(0.3f);
        PlayerStrike = true;
        yield return new WaitForSeconds(0.63f);
        PlayerStrike = false;
        yield return new WaitForSeconds(0.7f);
        EnemyAnimator.SetBool("Idle to Attack1", false);
    }


    IEnumerator DeathBehaviour()
    {
        Agent.velocity = Vector3.zero;
        yield return new WaitForSeconds(0.1f);
        EnemyAnimator.SetBool("Dead", true);
        BoxCollider.enabled = false;
        Agent.radius = 0;
        PlayDeathGrowl();
        OnKill();

    }

    public void OnKill()
    {
        if (!gameManager.deadEnemies.Contains(enemyID))
            gameManager.deadEnemies.Add(enemyID);
    }
}
