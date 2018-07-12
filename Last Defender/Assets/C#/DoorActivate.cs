using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class DoorActivate : MonoBehaviour {

    [System.Serializable]
    public enum DoorType {automatic, powerAcquired}
    public Light plight1, plight2;
    public DoorType doorType;

    [SerializeField]
    private Animator _animator;
    [SerializeField]
    private AudioSource _doorSFX;

    public bool powerLevelReached;
    public int powerLevelRequirement;
    public bool doorIsOpening;
    private CharacterMotor _player;
    [SerializeField]
    private GameObject _restorePower;
    [SerializeField]
    private GameObject _powerCoreRequired;
    private bool _playerInRange;

    private void Start()
    {
        doorIsOpening = false;
        powerLevelReached = false;
        _player = GameObject.Find("PlayerMain").GetComponent<CharacterMotor>();
 
        _restorePower.SetActive(false);
        _powerCoreRequired.SetActive(false);
    }

    private void Update()
    {
        if (doorType == DoorType.powerAcquired)
        {
            if(_playerInRange)
            {
                if (_player.powerCoresCollected < powerLevelRequirement)
                {
                    _powerCoreRequired.SetActive(true);
                }
                else if (_player.powerCoresCollected >= powerLevelRequirement && !powerLevelReached)
                {
                    _restorePower.SetActive(true);
                }
            }
            else if (!_playerInRange)
            {
                _powerCoreRequired.SetActive(false);
                _restorePower.SetActive(false);
            }
          

            if (_player.powerCoresCollected >= powerLevelRequirement)
            {
                if (Input.GetKeyDown(KeyCode.R))
                {
                    _restorePower.SetActive(true);
                    powerLevelReached = true;
                    plight1.color = Color.green;
                    plight2.color = Color.green;
                    _animator.SetBool("DoorActive", true);
                    _doorSFX.Play();
                    StartCoroutine(DoorOpenCheck());
                }
               
            }
            else if (!powerLevelReached)
            {
                plight1.color = Color.red;
                plight2.color = Color.red;
            }

        }
        else if (doorType == DoorType.automatic)
        {
            plight1.color = Color.green;
            plight2.color = Color.green;
        }
            
    }

    private void OnTriggerEnter(Collider other)
    {
       
        if (other.CompareTag("Player") /*|| other.CompareTag("Enemy")*/)
        {
            _playerInRange = true;

            if (doorType == DoorType.powerAcquired)
            {
                

                if (powerLevelReached && !doorIsOpening)
                {
                    _animator.SetBool("DoorActive", true);
                    _doorSFX.Play();
                    StartCoroutine(DoorOpenCheck());
                }
               
            }
            else if (doorType == DoorType.automatic && !doorIsOpening)
            {
                _animator.SetBool("DoorActive", true);
                _doorSFX.Play();
                StartCoroutine(DoorOpenCheck());
            }
          
        }
    }

    IEnumerator DoorOpenCheck()
    {
        doorIsOpening = true;
        yield return new WaitForSeconds(1f);
        doorIsOpening = false;
    }

    private void OnTriggerExit(Collider other)
    {
        _playerInRange = false;
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
