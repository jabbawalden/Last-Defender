using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrongDemon : Enemy
{
    public bool animationActive;

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
        animationActive = false;
        TriggerCollider.center = new Vector3(0.53f, 0.99f, 0.75f);
        TriggerCollider.size = new Vector3(1.87f, 1.99f, 2.31f);
    }

    protected override void MovementPattern()
    {
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
        }

        if (DistanceToPlayer <= 8)
        {
            enemyState = EnemyState.Run;
        }

        if (DistanceToPlayer <= 3)
        {
            enemyState = EnemyState.Attack;
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
        Agent.isStopped = false;
        MovementPattern();
        EnemyAnimator.SetBool("Run", true);

    }

    public void AttackBehaviour()
    {
        EnemyAnimator.SetBool("Run", false);

        if (Time.time > NewFireRate && !animationActive)
        {
            NewFireRate = Time.time + FireRate;
            StartCoroutine(AttackRoutine());
        }
        
    }

    IEnumerator AttackRoutine()
    {
        EnemyAnimator.SetBool("Idle to Attack1", true);
        Agent.isStopped = true;
        animationActive = true;
        yield return new WaitForSeconds(2.5f);
        animationActive = false;
        EnemyAnimator.SetBool("Idle to Attack1", false);

    }

    public void DeathBehaviour()
    {
        Agent.isStopped = true;
    }
}
