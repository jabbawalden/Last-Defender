using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitPlayer : MonoBehaviour {

    public StrongDemon strongDemon;
    private CharacterMotor characterMotor;

    private void Start()
    {
        characterMotor = GameObject.Find("PlayerMain").GetComponent<CharacterMotor>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (strongDemon.playerStrike)
            characterMotor.health -= strongDemon.EnemyDamage;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        
    }
}
