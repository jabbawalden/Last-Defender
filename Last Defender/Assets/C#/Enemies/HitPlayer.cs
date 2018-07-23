using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitPlayer : MonoBehaviour {

    [System.Serializable]
    public enum DemonHitType {StrongDemon, FastDemon, RangedDemon}
    public DemonHitType demonHitType;
    

    public StrongDemon strongDemon;
    public FastDemon fastDemon;
    private CharacterMotor characterMotor;

    private void Start()
    {
        characterMotor = GameObject.Find("PlayerMain").GetComponent<CharacterMotor>();

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
                    characterMotor.health -= strongDemon.EnemyDamage;
            }
         
            if (demonHitType == DemonHitType.FastDemon)
            {
                if (fastDemon.PlayerStrike)
                    characterMotor.health -= fastDemon.EnemyDamage;
            }
   

        }
    }

    private void OnTriggerExit(Collider other)
    {
        
    }
}
