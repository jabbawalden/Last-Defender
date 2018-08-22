using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeDemon : Enemy
{
    
    [SerializeField] private GameObject _projectile;
    [SerializeField] private GameObject _shootOrigin;
    [SerializeField] private float _projSpeed;
    public GameObject rayOriginObject;
    [SerializeField] private int _shootRange;

    // Use this for initialization
    void Start()
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
    void Update()
    {
        GetDistance();
        Vector3 newPlayerPosition = new Vector3(Player.transform.position.x, Player.transform.position.y - 0.5f, Player.transform.position.z);
        Direction = (newPlayerPosition - transform.position).normalized;
        
        switch (enemyState)
        {
            case EnemyState.Run:
                RunBehaviour();
                RayView();
                break;
            case EnemyState.Attack:
                AttackBehaviour();
                RayView();
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
            EnemyAnimator.SetBool("Walk", false);
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

        if (DistanceToPlayer <= _shootRange && playerInSight && ReturnPlayerLife())
        {
            enemyState = EnemyState.Attack;
        }

        if (CurrentHealth <= 0)
        {
            enemyState = EnemyState.Dead;
        }
    }

    public void RayView()
    {
        //Vector3 direction = transform.position - Player.transform.position;
        Vector3 rayOrigin = rayOriginObject.transform.position;
        Debug.DrawRay(rayOrigin, Direction * 40, Color.blue);
        RaycastHit hit;

        if (Physics.Raycast(rayOrigin, Direction * 40, out hit))
        {
            if (hit.collider.CompareTag("Player"))
            {
                playerInSight = true;  
            }
            else
            {
                playerInSight = false;
            }

        }

    }


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
        if (EnemyAnimator.GetBool("Attack") == false)
            StartCoroutine(WalkRoutine());

        PlayGrowl();
    }

    IEnumerator WalkRoutine()
    {
        EnemyAnimator.SetBool("Walk", true);
        yield return new WaitForSeconds(0.2f);
        MovementPattern();
    }

    public void AttackBehaviour()
    {
        float randomFireRate = Random.Range(1.5f, 2.1f);
        FireRate = randomFireRate;

        EnemyAnimator.SetBool("Walk", false);
        Agent.velocity = Vector3.zero;
        transform.LookAt(Player.transform.position);

        if (Time.time > NewFireRate)
        {
            NewFireRate = Time.time + FireRate;
            StartCoroutine(AttackRoutine());
        }
    }

    public void FireBehaviour()
    {
        GameObject shot1 = Instantiate(_projectile, _shootOrigin.transform.position, Quaternion.LookRotation(Direction));
        shot1.GetComponent<Rigidbody>().velocity = Direction * _projSpeed;
    }

    IEnumerator AttackRoutine()
    {
        yield return new WaitForSeconds(0.15f);
        EnemyAnimator.SetBool("Attack", true);
        yield return new WaitForSeconds(0.4f);
        FireBehaviour();
        yield return new WaitForSeconds(1f);
        EnemyAnimator.SetBool("Attack", false);
    }


    IEnumerator DeathBehaviour()
    {
        Agent.velocity = Vector3.zero;
        yield return new WaitForSeconds(0.5f);
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
