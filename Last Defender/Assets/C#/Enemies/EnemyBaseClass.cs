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
    public enum EnemyState { Idle, Shout, Run, Attack, Dead, Hit}
    public enum EnemySound { Active, Silent}

    public EnemyState enemyState;
    public EnemySound enemySound;
    public Animator EnemyAnimator;
    public AudioSource EnemyAudio;
    public AudioClip[] EnemyGrowl;
    public AudioClip[] EnemyFootstep;
    public AudioClip EnemyDeathGrowl;
    public AudioMixerGroup EnemyAudioMixer;
    public bool growlPlayed;
    public bool deathGrowlPlayed;

    public GameObject bodyCollision;

    public string enemyID = "Undefined";

    public GameObject Player;
    public BoxCollider BoxCollider;
    public Rigidbody RB;
    public int CurrentHealth;
    public int MaxHealth;
    public float MovementSpeed;
    public GameManager gameManager;
    public int EnemyDamage;
    public float FireRate;
    public float NewFireRate;
    public bool PlayerStrike;
    public bool playerInSight;

    public GameObject Joints;
    public NavMeshAgent Agent;
    public Vector3 Direction;
    public float DistanceToPlayer;
    public float aggressionDistance;
    public bool attackPlayer;
    public bool canMove;
    public Vector3 newPlayerPosition;
    public CharacterMotor characterMotor;

    //only classes that inherit from Enemy can see this field. 
    //abstract = must be overriden by inheriting class
    protected abstract void MovementPattern();

    private void Awake()
    {
        //add common components
        canMove = true;
        characterMotor = GameObject.Find("PlayerMain").GetComponent<CharacterMotor>();
        RB = gameObject.AddComponent<Rigidbody>();
        RB.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezePositionZ;
        RB.collisionDetectionMode = CollisionDetectionMode.Continuous;
        EnemyAudio = gameObject.AddComponent<AudioSource>();
        EnemyAudio.spatialBlend = 1;
        EnemyAudio.maxDistance = 2600;
        EnemyAudio.outputAudioMixerGroup = EnemyAudioMixer;
        EnemyAudio.minDistance = 2f;
        EnemyAudio.pitch = 0.75f;
        growlPlayed = false;
        deathGrowlPlayed = false;
        RB.isKinematic = true;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        Player = GameObject.Find("PlayerMain");
        gameObject.tag = "Enemy";
        Agent = gameObject.GetComponent<NavMeshAgent>();
        Agent.speed = MovementSpeed;
        EnemyAnimator = GetComponent<Animator>();
        BoxCollider = GetComponent<BoxCollider>();
        CurrentHealth = MaxHealth;
    }


    public void GetDistance()
    {
        DistanceToPlayer = Vector3.Distance(transform.position, Player.transform.position);
    }

    public void PlayGrowl()
    {
        int r = Random.Range(0, EnemyGrowl.Length);

        if (enemySound == EnemySound.Active && !growlPlayed)
        {
            EnemyAudio.PlayOneShot(EnemyGrowl[r]);
            growlPlayed = true;
        }
    }
     
    public void Step()
    {
        AudioClip clip = GetRandomClip();
        EnemyAudio.PlayOneShot(clip);
    }

    public AudioClip GetRandomClip()
    {
        return EnemyFootstep[UnityEngine.Random.Range(0, EnemyFootstep.Length)];
    }

    public void PlayDeathGrowl()
    {
        if (!deathGrowlPlayed)
        {
            EnemyAudio.PlayOneShot(EnemyDeathGrowl);
            deathGrowlPlayed = true;
        }
    }

    public void RotateTowards()
    {
        Quaternion lookRotation = Quaternion.LookRotation(Direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, 10 * Time.deltaTime);
    }

    public bool ReturnPlayerLife()
    {
        if (characterMotor.health >= 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}


