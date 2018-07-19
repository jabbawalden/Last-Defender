using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class DemonController : MonoBehaviour {

    //set enemyID variable
    public string enemyID = "Undefined";

    public NavMeshAgent agent;
    private GameObject _player;
    private CharacterMotor _pCharMotor;
    public float distance;
    private float _projSpeed;
    private Vector3 direction;

    private float _newFireRate;
    [SerializeField] private float _fireRate;
    [SerializeField] private GameObject _projectile;
    [SerializeField] private GameObject _shootOrigin;
    [SerializeField] private float _shootSeconds;

    [SerializeField] private bool _isAttacking;
    [SerializeField] private float _attackRate;
    [SerializeField] private float _newAttack;

    [SerializeField] int type;

    private GlobalEnemyStats _globalEnemyStats;
    private ShootableBox _shootableBox;
    public bool playerInRange;
    //1 = speed, 2 = strong, 3 = range

    private GameManager _gameManager;
    // Use this for initialization
    void Start()
    {
        if (enemyID == "Undefined")
        {
            Debug.LogError("Enemy ID not generated");
        }

        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        if (_gameManager.deadEnemies.Contains(enemyID))
        {
            Destroy(gameObject);
            return;
        }


        _globalEnemyStats = GameObject.Find("GlobalEnemyStats").GetComponent<GlobalEnemyStats>();
        playerInRange = false;
        _projSpeed = _globalEnemyStats.projectileSpeed * Time.deltaTime;
        _player = GameObject.Find("PlayerMain");
        _pCharMotor = GameObject.Find("PlayerMain").GetComponent<CharacterMotor>();
        _shootableBox = GetComponent<ShootableBox>();
        

    }

    // Update is called once per frame
    void Update()
    {

        if (_pCharMotor.health >= 1 && playerInRange == true)
        {
            EnemyFollow(type);
            transform.LookAt(_player.transform.position);
        }
        else if (_shootableBox.currentHealth < 3)
        {
            EnemyFollow(type);
            transform.LookAt(_player.transform.position);
        }

        direction = (_player.transform.position - transform.position).normalized;

        if (type == 3)
        {
            RayView();
        }

        if (_isAttacking)
        {
            DamagePlayer();
        }

    }

    public void RayView()
    {
        Vector3 direction = transform.forward;
        Vector3 rayOrigin = transform.position;
        Debug.DrawRay(rayOrigin, direction * 40, Color.blue);
        RaycastHit hit;

        if (Physics.Raycast(rayOrigin, direction * 40, out hit))
        {
            if (hit.collider.CompareTag("Player"))
            {
                FirePlayer();

            }

        }

    }
    private void EnemyFollow(int c)
    {
        var targetposition = (transform.position - _player.transform.position).normalized * distance + _player.transform.position;
        //find direction, * distance with player position added.
        int r = Random.Range(0, 3);

        if (c == 1)
        {
            agent.SetDestination(_pCharMotor.hitPos[r].transform.position);
            agent.speed = _globalEnemyStats.speed_Speed;
        }

        if (c == 2)
        {
            agent.SetDestination(_player.transform.position);
            agent.speed = _globalEnemyStats.speed_Strength;
        }

        if (c == 3)
        {
            agent.SetDestination(targetposition);
            agent.speed = _globalEnemyStats.speed_Range;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _isAttacking = true;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _isAttacking = false;
        }

    }

    private void DamagePlayer()
    {
        _attackRate = 1;

        if (Time.time > _newAttack)
        {
            _newAttack = Time.time + _attackRate;
            _pCharMotor.health--;
        }
           
  
    }

    private void FirePlayer()
    {   
        if (Time.time > _newFireRate)
        {
            _newFireRate = Time.time + _fireRate;
            GameObject shot1 = Instantiate(_projectile, _shootOrigin.transform.position, Quaternion.LookRotation(direction));
            shot1.GetComponent<Rigidbody>().velocity = direction * _projSpeed;
        }
    }

    //if ID is not in deadEnemies, add it to list so we know which enemies are dead
    public void OnKill()
    {
        if (!_gameManager.deadEnemies.Contains(enemyID))
        {
            _gameManager.deadEnemies.Add(enemyID);
        }

    }

    private void OnEnable()
    {
        GameEvents.EventEnemyHit += EnemyHit;
    }

    private void OnDisable()
    {
        GameEvents.EventEnemyHit -= EnemyHit;
    }

    void EnemyHit()
    {
        Debug.Log("Play Enemy Hit Sound");
        //Play sound
    }
} 
