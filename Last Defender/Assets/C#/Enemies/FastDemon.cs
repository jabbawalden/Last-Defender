using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastDemon : Enemy
{

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

        switch (enemyState)
        {
            case EnemyState.Run:
                RunBehaviour();
                break;
            case EnemyState.Attack:
                AttackBehaviour();
                break;
            case EnemyState.Death:
                DeathBehaviour();
                break;
        }

        if (!attackPlayer && DistanceToPlayer <= aggressionDistance)
        {
            enemyState = EnemyState.Run;
        }
        else if (attackPlayer)
        {
            enemyState = EnemyState.Run;
        }

        if (DistanceToPlayer <= 3.5f)
        {
            enemyState = EnemyState.Attack;
        }

        if (Health <= 0)
        {
            enemyState = EnemyState.Death;
        }

    
    }

  
    public void IdleBehaviour()
    {
        Agent.isStopped = true;
    }

    public void ShoutBehaviour()
    {
        Agent.isStopped = true;
    }

    public void RunBehaviour()
    {
        if (EnemyAnimator.GetBool("Attack2") == false)
        {
            StartCoroutine(RunRoutine());
        }

    }

    IEnumerator RunRoutine()
    {
        EnemyAnimator.SetBool("Run", true);
        yield return new WaitForSeconds(0.23f);
        Agent.isStopped = false;
        MovementPattern();
    }

    public void AttackBehaviour()
    {
        EnemyAnimator.SetBool("Run", false);
        Agent.isStopped = true;
        if (Time.time > NewFireRate)
        {
            NewFireRate = Time.time + FireRate;
            StartCoroutine(AttackRoutine());
        }

    }

    IEnumerator AttackRoutine()
    {

        yield return new WaitForSeconds(0.15f);
        EnemyAnimator.SetBool("Attack2", true);
        yield return new WaitForSeconds(0.21f);
        PlayerStrike = true;
        yield return new WaitForSeconds(0.44f);
        PlayerStrike = false;
        yield return new WaitForSeconds(0.5f);
        EnemyAnimator.SetBool("Attack2", false);
    }

    public void DeathBehaviour()
    {
        Agent.isStopped = true;
        EnemyAnimator.SetBool("Dead", true);
        BoxCollider.enabled = false;
        Agent.radius = 0;
        OnKill();
    }

    public void OnKill()
    {
        if (!gameManager.deadEnemies.Contains(enemyID))
            gameManager.deadEnemies.Add(enemyID);
    }
}
