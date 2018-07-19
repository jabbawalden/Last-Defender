﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//enemy types: Meele, fast meele, ranged

//enemy movement speed
//enemy attack type - ranged or meele
//[OnTriggerStay + trigger collider + coroutine to time when damage is dealt according to animation]
//[will also need to switch back to idle or run depending on state]
//multiple states may be required
//enemy attack damage
//enemy attack rate

public class Enemy<T> where T : Enemy
{
    public GameObject gameObject;
    public T scriptComponent; 

    public Enemy(string name)
    {
        gameObject = new GameObject(name);
        scriptComponent = gameObject.AddComponent<T>();

    }

}

//abrasct is a modifier
public abstract class Enemy : MonoBehaviour
{
    public string enemyID = "Undefined";

    public GameObject Player;

    public Rigidbody Body;
    public BoxCollider TriggerCollider;
    public int Health;
    public float MovementSpeed;
    public GameManager gameManager;

    public float EnemyDamage;
    public float FireRate;
    public float NewFireRate;
    public bool PlayerInRange;

    public NavMeshAgent Agent;
    public Vector3 Direction;

    //only classes that inherit from Enemy can see this field. 
    //abstract = must be overriden by inheriting class
    protected abstract void MovementPattern();

    private void Awake()
    {
        //add common components
        Body = gameObject.AddComponent<Rigidbody>();
        Body.constraints = RigidbodyConstraints.FreezeRotationX;
        TriggerCollider = gameObject.AddComponent<BoxCollider>();
        TriggerCollider.isTrigger = true;
        Body.collisionDetectionMode = CollisionDetectionMode.Continuous;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        gameObject.tag = "Enemy";
    }

    //initialize method - insert all unique values to be determined at instantiation
    public virtual void Initialized(float movementSpeed, Vector3 direction, Vector3 position) 
    {
        MovementSpeed = movementSpeed;
        Direction = direction;
        transform.position = position;
    }

 
}

/*
public class StrongDemon : Enemy
{
    private CharacterMotor _pCharMotor;

    private void Start()
    {
        TriggerCollider.center = new Vector3(0.53f, 0.99f, 0.87f);
        TriggerCollider.size = new Vector3(1.87f, 1.99f, 2.31f);
        _pCharMotor = GameObject.Find("PlayerMain").GetComponent<CharacterMotor>();

    }

    protected override void MovementPattern()
    {
        Agent.SetDestination(_pCharMotor.transform.position);
    }
}

public class SpeedDemon : Enemy
{

}

public class RangedDemon : Enemy
{
    private CharacterMotor _pCharMotor;

    private void Start()
    {
        _pCharMotor = GameObject.Find("PlayerMain").GetComponent<CharacterMotor>();
    }

    protected override void MovementPattern()
    {
        int r = Random.Range(0, 3);
        Agent.SetDestination(_pCharMotor.hitPos[r].transform.position);
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

    public void FirePlayer()
    {
        if (Time.time > NewFireRate)
        {
            NewFireRate = Time.time + FireRate;
            
            GameObject shot1 = Instantiate(_projectile, _shootOrigin.transform.position, Quaternion.LookRotation(direction));
            shot1.GetComponent<Rigidbody>().velocity = direction * _projSpeed;
            
        }
    }
    
}
*/

