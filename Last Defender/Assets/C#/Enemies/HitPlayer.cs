using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitPlayer : MonoBehaviour {

    [System.Serializable]
    public enum DemonHitType {StrongDemon, FastDemon, RangedDemon}
    public DemonHitType demonHitType;
    

    public StrongDemon strongDemon;
    public FastDemon fastDemon;
    private CharacterMotor _characterMotor;

    private void Start()
    {
        _characterMotor = GameObject.Find("PlayerMain").GetComponent<CharacterMotor>();

        if (strongDemon == null)
        {
            return;
        }
        if (fastDemon == null)
        {
            return;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            if (demonHitType == DemonHitType.StrongDemon)
            {
                if (strongDemon.PlayerStrike)
                {
                    _characterMotor.health -= strongDemon.EnemyDamage - _characterMotor.armor;
                    GameEvents.PlayerEventHit();
                }
                   
            }
         
            if (demonHitType == DemonHitType.FastDemon)
            {
                if (fastDemon.PlayerStrike)
                {
                    _characterMotor.health -= fastDemon.EnemyDamage - _characterMotor.armor;
                    GameEvents.PlayerEventHit();
                }
                    
            }

        }
    }

 

    private void OnTriggerExit(Collider other)
    {
        
    }
}
