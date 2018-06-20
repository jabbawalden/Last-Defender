using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerCore : MonoBehaviour
{
    //create reference
    private CharacterMotor _player;

    void Start()
    {
        //set reference
        _player = GameObject.Find("PlayerMain").GetComponent<CharacterMotor>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            //+1 to power cores collected
            _player.powerCoresCollected++;
            Destroy(this.gameObject, 1f);
        }
    }

}