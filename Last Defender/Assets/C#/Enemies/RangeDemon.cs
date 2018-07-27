using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeDemon : Enemy
{
    Vector3 direction;
    [SerializeField] private GameObject _projectile;
    [SerializeField] private GameObject _shootOrigin;
    [SerializeField] private float _projSpeed;
    public GameObject rayOriginObject;
    public bool playerInSight;

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
        Vector3 newPlayerPosition = new Vector3(Player.transform.position.x, Player.transform.position.y - 0.5f, Player.transform.position.z);
        direction = (newPlayerPosition - transform.position).normalized;
        

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
            case EnemyState.Death:
                StartCoroutine(DeathBehaviour());
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

        if (DistanceToPlayer <= 20f && playerInSight)
        {
            enemyState = EnemyState.Attack;
        }

        if (Health <= 0)
        {
            enemyState = EnemyState.Death;
        }
    }

    public void RayView()
    {
        //Vector3 direction = transform.position - Player.transform.position;
        Vector3 rayOrigin = rayOriginObject.transform.position;
        Debug.DrawRay(rayOrigin, direction * 40, Color.blue);
        RaycastHit hit;

        if (Physics.Raycast(rayOrigin, direction * 40, out hit))
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
        Agent.isStopped = true;
    }

    public void ShoutBehaviour()
    {
        Agent.isStopped = true;
    }

    public void RunBehaviour()
    {
        if (EnemyAnimator.GetBool("Attack") == false)
        {
            StartCoroutine(WalkRoutine());
        }

    }

    IEnumerator WalkRoutine()
    {
        EnemyAnimator.SetBool("Walk", true);
        yield return new WaitForSeconds(0.3f);
        Agent.isStopped = false;
        MovementPattern();
    }

    public void AttackBehaviour()
    {
        float randomFireRate = Random.Range(1.5f, 2.1f);
        FireRate = randomFireRate;

        EnemyAnimator.SetBool("Walk", false);
        Agent.isStopped = true;

        transform.LookAt(Player.transform.position);

        if (Time.time > NewFireRate)
        {
            NewFireRate = Time.time + FireRate;
            StartCoroutine(AttackRoutine());
        }

    }

    public void FireBehaviour()
    {
        GameObject shot1 = Instantiate(_projectile, _shootOrigin.transform.position, Quaternion.LookRotation(direction));
        shot1.GetComponent<Rigidbody>().velocity = direction * _projSpeed;
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
        Agent.isStopped = true;
        yield return new WaitForSeconds(0.5f);
        EnemyAnimator.SetBool("Dead", true);
        BoxCollider.enabled = false;
        Agent.radius = 0;
        EnemyAudio.PlayOneShot(EnemyDeathGrowl);
        OnKill();

    }

    public void OnKill()
    {
        if (!gameManager.deadEnemies.Contains(enemyID))
            gameManager.deadEnemies.Add(enemyID);
    }
}
