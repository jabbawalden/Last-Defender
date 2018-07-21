using System.Collections;
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
                DeathBehaviour();
                break;
        }

        if (DistanceToPlayer <= aggressionDistance)
        {
            enemyState = EnemyState.Run;
        }

        if (DistanceToPlayer <= 2.8f)
        {
            enemyState = EnemyState.Attack;
        }

        if (Health <= 0)
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
        Agent.isStopped = true;
    }

    public void ShoutBehaviour()
    {
        Agent.isStopped = true;
    }

    public void RunBehaviour()
    {
        if (EnemyAnimator.GetBool("Idle to Attack1") == false)
        {
            StartCoroutine(RunRoutine());
        }

    }

    IEnumerator RunRoutine()
    {
        EnemyAnimator.SetBool("Run", true);
        yield return new WaitForSeconds(0.2f);
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
       
        yield return new WaitForSeconds(0.2f);
        EnemyAnimator.SetBool("Idle to Attack1", true);
        yield return new WaitForSeconds(0.3f);
        PlayerStrike = true;
        yield return new WaitForSeconds(0.65f);
        PlayerStrike = false;
        yield return new WaitForSeconds(0.78f);
        EnemyAnimator.SetBool("Idle to Attack1", false);
    }

    public void DeathBehaviour()
    {
        Agent.isStopped = true;
        EnemyAnimator.SetBool("Dead", true);
        bodyCollision.SetActive(false);
        Agent.radius = 0;
        OnKill();
    }

    public void OnKill()
    {
        if (!gameManager.deadEnemies.Contains(enemyID))
            gameManager.deadEnemies.Add(enemyID);
    }
}
