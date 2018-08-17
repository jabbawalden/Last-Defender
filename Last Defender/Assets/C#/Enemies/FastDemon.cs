﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastDemon : Enemy
{
    public GameObject rayOriginObject;

    private void OnEnable()
    {
        //MAYBE an event for death

    }

    private void OnDisable()
    {

    }
    // Use this for initialization
    void Start()
    {
        if (gameManager.deadEnemies.Contains(enemyID))
        {
            enemyState = EnemyState.Dead;
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
    void Update()
    {
        GetDistance();
        newPlayerPosition = new Vector3(Player.transform.position.x, Player.transform.position.y - 1f, Player.transform.position.z);
        Direction = (newPlayerPosition - transform.position).normalized;

        switch (enemyState)
        {
            case EnemyState.Run:
                RunBehaviour();
                break;
            case EnemyState.Attack:
                AttackBehaviour();
                break;
            case EnemyState.Dead:
                DeathBehaviour();
                break;
            case EnemyState.Idle:
                IdleBehaviour();
                break;
        }

        if (CurrentHealth < MaxHealth && ReturnPlayerLife())
        {
            attackPlayer = true;
        }

        if (DistanceToPlayer > aggressionDistance)
        {
            enemyState = EnemyState.Idle;
            EnemyAnimator.SetBool("Run", false);
        }

        if (!attackPlayer && DistanceToPlayer <= aggressionDistance && ReturnPlayerLife())
        {
            enemyState = EnemyState.Run;
        }
        else if (attackPlayer && ReturnPlayerLife())
        {
            enemyState = EnemyState.Run;
        }

        if (DistanceToPlayer <= 2.4f && CurrentHealth > 0 && ReturnPlayerLife())
        {
            enemyState = EnemyState.Attack;
            RotateTowards();
        }

        if (CurrentHealth <= 0)
        {
            enemyState = EnemyState.Dead;
        }

    }

  
    public void IdleBehaviour()
    {
        Agent.velocity = Vector3.zero;
        EnemyAnimator.SetBool("Run", false);
    }

    public void ShoutBehaviour()
    {
        Agent.velocity = Vector3.zero;
    }

    public void RunBehaviour()
    {
        if (canMove)
            StartCoroutine(RunRoutine());

        PlayGrowl();
    }

    IEnumerator RunRoutine()
    {
        EnemyAnimator.SetBool("Run", true);
        yield return new WaitForSeconds(0.12f);
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
        canMove = false;
        yield return new WaitForSeconds(0.13f);
        EnemyAnimator.SetBool("Attack2", true);
        yield return new WaitForSeconds(0.17f);
        PlayerStrike = true;
        yield return new WaitForSeconds(0.33f);
        PlayerStrike = false;
        yield return new WaitForSeconds(0.36f);
        EnemyAnimator.SetBool("Attack2", false);
        canMove = true;
    }

    

    public void DeathBehaviour()
    {
        Agent.velocity = Vector3.zero;
        EnemyAnimator.SetBool("Dead", true);
        BoxCollider.enabled = false;
        Joints.SetActive(false);
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
