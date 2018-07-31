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
    public enum EnemyState { Idle, Shout, Run, Attack, Death, Hit}
    public enum EnemySound { Active, Silent}

    public EnemyState enemyState;
    public EnemySound enemySound;

    public Animator EnemyAnimator;
    public AudioSource EnemyAudio;
    public AudioClip[] EnemyGrowl;
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

    public void PlayDeathGrowl()
    {
        if (!deathGrowlPlayed)
        {
            EnemyAudio.PlayOneShot(EnemyDeathGrowl);
            deathGrowlPlayed = true;
        }
        
    }

    public void HitActivate()
    {
        StartCoroutine(HitBehaviour());
    }

    IEnumerator HitBehaviour()
    {
        if (enemyState == EnemyState.Run)
        {
            if (CurrentHealth <= 0)
            {
                enemyState = EnemyState.Death;
            }
            else
            {
                Agent.speed = 0;
                EnemyAnimator.SetBool("RunToHit", true);
                yield return new WaitForSeconds(0.5f);
                EnemyAnimator.SetBool("RunToHit", false);
                Agent.speed = MovementSpeed;
            }

        }

        if (enemyState == EnemyState.Attack)
        {
            if (CurrentHealth <= 0)
            {
                enemyState = EnemyState.Death;
            }
            else
            {
                Agent.speed = 0;
                EnemyAnimator.SetBool("AttackToHit", true);
                yield return new WaitForSeconds(0.5f);
                EnemyAnimator.SetBool("AttackToHit", false);
                Agent.speed = MovementSpeed;
            }

        }

        if (enemyState == EnemyState.Idle)
        {
            if (CurrentHealth <= 0)
            {
                enemyState = EnemyState.Death;
            }
            else
            {
                Agent.speed = 0;
                EnemyAnimator.SetBool("IdleToHit", true);
                yield return new WaitForSeconds(0.5f);
                EnemyAnimator.SetBool("IdleToHit", false);
                Agent.speed = MovementSpeed;
            }

        }

    }

}


