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
        newPlayerPosition = new Vector3(Player.transform.position.x, Player.transform.position.y - 0.5f, Player.transform.position.z);
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
                StartCoroutine(DeathBehaviour());
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
            Agent.velocity = Vector3.zero;
        }

        if (!attackPlayer && DistanceToPlayer <= aggressionDistance && ReturnPlayerLife())
        {
            enemyState = EnemyState.Run;
        }
        else if (attackPlayer && ReturnPlayerLife())
        {
            enemyState = EnemyState.Run;
        }

        if (DistanceToPlayer <= 2.7f && CurrentHealth > 0 && ReturnPlayerLife())
        {
            enemyState = EnemyState.Attack;
            RotateTowards();
        }

        if (CurrentHealth <= 0)
        {
            enemyState = EnemyState.Dead;
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
        if (canMove)
            StartCoroutine(RunRoutine());

        PlayGrowl();
    }

    IEnumerator RunRoutine()
    {
        EnemyAnimator.SetBool("Run", true);
        yield return new WaitForSeconds(0.14f);
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
        yield return new WaitForSeconds(0.2f);
        EnemyAnimator.SetBool("Idle to Attack1", true);
        yield return new WaitForSeconds(0.3f);
        PlayerStrike = true;
        yield return new WaitForSeconds(0.63f);
        PlayerStrike = false;
        yield return new WaitForSeconds(0.7f);
        EnemyAnimator.SetBool("Idle to Attack1", false);
        canMove = true;
    }

    public void HitActivate()
    {
        StartCoroutine(HitBehaviour());
    }

    IEnumerator HitBehaviour()
    {
        Agent.speed = 0;
        yield return new WaitForSeconds(0.05f);
        EnemyAnimator.SetTrigger("GetHit");
        yield return new WaitForSeconds(0.5f);
        Agent.speed = MovementSpeed;
    }

    IEnumerator DeathBehaviour()
    {
        Agent.velocity = Vector3.zero;
        yield return new WaitForSeconds(0.1f);
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
