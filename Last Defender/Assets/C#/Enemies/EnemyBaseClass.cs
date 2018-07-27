using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Audio;

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
    [System.Serializable]
    public enum EnemyState { Idle, Shout, Run, Attack, Death }
    public enum EnemySound { Active, Silent}

    public EnemyState enemyState;
    public EnemySound enemySound;

    public Animator EnemyAnimator;
    public AudioSource EnemyAudio;
    public AudioClip[] EnemyGrowl;
    public AudioClip EnemyDeathGrowl;
    public AudioMixerGroup EnemyAudioMixer;
    public bool growlPlayed;
    public GameObject bodyCollision;

    public string enemyID = "Undefined";

    public GameObject Player;
    public BoxCollider BoxCollider;
    public Rigidbody RB;
    public int Health;
    public float MovementSpeed;
    public GameManager gameManager;
    public int EnemyDamage;
    public float FireRate;
    public float NewFireRate;
    public bool PlayerStrike;

    public NavMeshAgent Agent;
    public Vector3 Direction;
    public float DistanceToPlayer;
    public float aggressionDistance;
    public bool attackPlayer;

    //only classes that inherit from Enemy can see this field. 
    //abstract = must be overriden by inheriting class
    protected abstract void MovementPattern();

    private void Awake()
    {
        //add common components
        RB = gameObject.AddComponent<Rigidbody>();
        RB.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezePositionZ;
        RB.collisionDetectionMode = CollisionDetectionMode.Continuous;
        EnemyAudio = gameObject.AddComponent<AudioSource>();
        EnemyAudio.spatialBlend = 1;
        EnemyAudio.maxDistance = 1000;
        EnemyAudio.outputAudioMixerGroup = EnemyAudioMixer;
        EnemyAudio.minDistance = 0.5f;
        growlPlayed = false;
        RB.isKinematic = true;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        Player = GameObject.Find("PlayerMain");
        gameObject.tag = "Enemy";
        Agent = gameObject.GetComponent<NavMeshAgent>();
        EnemyAnimator = GetComponent<Animator>();
        BoxCollider = GetComponent<BoxCollider>();
    }

   

    public void GetDistance()
    {
        DistanceToPlayer = Vector3.Distance(transform.position, Player.transform.position);
    }

    public void PlayGrowl()
    {
        int r = Random.Range(0, EnemyGrowl.Length + 1);
        if (enemySound == EnemySound.Active && !growlPlayed)
        {
            EnemyAudio.PlayOneShot(EnemyGrowl[r]);
            growlPlayed = true;
        }
    }

    public void PlayDeathGrowl()
    {
        EnemyAudio.PlayOneShot(EnemyDeathGrowl);
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

