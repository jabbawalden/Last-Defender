using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DemonController : MonoBehaviour {

    public NavMeshAgent agent;
    private GameObject _player;
    private CharacterMotor _pCharMotor;
    public float distance;
    public float projSpeed;
    private Vector3 direction;

    [SerializeField] private GameObject _projectile;
    [SerializeField] private GameObject _shootOrigin;
    [SerializeField] int type;
    //1 = speed, 2 = strong, 3 = range


    // Use this for initialization
    void Start ()
    {
        projSpeed *= Time.deltaTime;
        _player = GameObject.Find("_PlayerMove");
        _pCharMotor = GameObject.Find("_PlayerMove").GetComponent<CharacterMotor>();
        StartCoroutine(FirePlayer());
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (_pCharMotor.health >= 1)
        {
            EnemyFollow(type);  
        }

        direction = (_player.transform.position - transform.position).normalized;

    }

    private void EnemyFollow(int c)
    {
        var targetposition = (transform.position - _player.transform.position).normalized * distance + _player.transform.position;
        //find direction, * distance with player position added.
        int r = Random.Range(0, 3);

        if (c == 1)
        {
            agent.SetDestination(_pCharMotor.hitPos[r].transform.position);
            agent.speed = 7;
        }

        if (c == 2)
        {
            agent.SetDestination(_player.transform.position);
            agent.speed = 4;
        }

        if (c == 3)
        {
            agent.SetDestination(targetposition);
            
            agent.speed = 5;
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
        while (_pCharMotor.health > 1)
        {
            yield return new WaitForSeconds(1);

            _pCharMotor.health--;
        }
    }

    private IEnumerator FirePlayer()
    {

        while (true)
        {
            yield return new WaitForSeconds(1);
            
            GameObject shot1 = Instantiate(_projectile, _shootOrigin.transform.position, Quaternion.LookRotation(direction));
            shot1.GetComponent<Rigidbody>().velocity = direction * projSpeed;
        }
    }
}
