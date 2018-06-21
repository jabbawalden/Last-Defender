using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class DoorActivate : MonoBehaviour {

    [System.Serializable]
    public enum DoorType {automatic, powerAcquired}

    public DoorType doorType;

    [SerializeField]
    private Animator _animator;
    [SerializeField]
    private AudioSource _doorSFX;

    public bool powerLevelReached;
    public int powerLevelRequirement;

    private CharacterMotor _player;

    private void Start()
    {
        powerLevelReached = false;
        _player = GameObject.Find("PlayerMain").GetComponent<CharacterMotor>();
    }

    private void Update()
    {
        if (_player.powerCoresCollected == powerLevelRequirement)
        {
            powerLevelReached = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        print(other.tag);
        if (other.CompareTag("Player") || other.CompareTag("Enemy"))
        {
            if (doorType == DoorType.powerAcquired)
            {
                if (powerLevelReached)
                {
                    _animator.SetBool("DoorActive", true);
                    _doorSFX.Play();
                }
               
            }
            else
            {
                _animator.SetBool("DoorActive", true);
                _doorSFX.Play();
            }
          
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (doorType == DoorType.powerAcquired)
            {
                if (powerLevelReached)
                {
                    _animator.SetBool("DoorActive", false);
                    _doorSFX.Play();
                }
            }
            else
            {
                _animator.SetBool("DoorActive", false);
                _doorSFX.Play();
            } 
            
        }
    }

}
