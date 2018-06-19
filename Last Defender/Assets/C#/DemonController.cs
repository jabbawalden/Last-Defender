using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DemonController : MonoBehaviour {

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

    [SerializeField] int type;

    private GlobalEnemyStats _globalEnemyStats;
    private ShootableBox _shootableBox;
    public bool playerInRange;
    //1 = speed, 2 = strong, 3 = range


    // Use this for initialization
    void Start ()
    {
        _globalEnemyStats = GameObject.Find("GlobalEnemyStats").GetComponent<GlobalEnemyStats>();
        playerInRange = false;
        _projSpeed = _globalEnemyStats.projectileSpeed * Time.deltaTime;
        _player = GameObject.Find("_PlayerMove");
        _pCharMotor = GameObject.Find("_PlayerMove").GetComponent<CharacterMotor>();
        _shootableBox = GetComponent<ShootableBox>();
        
    }
	
	// Update is called once per frame
	void Update ()
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
        
    }

    public void RayView()
    {
        Vector3 direction = transform.forward;
        Vector3 rayOrigin = transform.position;
        Debug.DrawRay(rayOrigin, direction * 40, Color.blue);
        RaycastHit hit;

        if (Physics.Raycast(rayOrigin, direction * 40, out hit))
        {
            if (hit.collider.tag == "Player")
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
        if (other.tag == "Player")
        {
            StartCoroutine(DamagePlayer());
        }
       
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            StopCoroutine(DamagePlayer());
        }

    }

    private IEnumerator DamagePlayer()
    {
        while (_pCharMotor.health >= 1)
        {
            yield return new WaitForSeconds(1);

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
}
