using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class DoorActivate : MonoBehaviour {

    [System.Serializable]
    public enum DoorState {unlocked, locked}
    public DoorState doorState;

    public Light plight1, plight2;
    [SerializeField]
    private Animator _animator;
    [SerializeField]
    private AudioSource _doorSFX;

    public bool powerLevelReached;
    public int powerLevelRequirement;
    public bool doorIsOpening;
    private CharacterMotor _player;
    private UIManager _uIManager;
    public bool playerInRange;
    public bool unlocked;

    private void Start()
    {
        doorIsOpening = false;
        powerLevelReached = false;
        _player = GameObject.Find("PlayerMain").GetComponent<CharacterMotor>();
        _uIManager = GameObject.Find("UI_InGame").GetComponent<UIManager>();

        switch (doorState)
        {
            case DoorState.unlocked:
                plight1.color = Color.green;
                plight2.color = Color.green;
                break;
            case DoorState.locked:
                plight1.color = Color.red;
                plight2.color = Color.red;
                break;
        }
    }

    private void Update()
    {
        if (_player.powerCoresCollected >= powerLevelRequirement )
        {
            powerLevelReached = true;
        }
        else
        {
            powerLevelReached = false;
        }
    }


    IEnumerator DoorOpenCheck()
    {
        doorIsOpening = true;
        yield return new WaitForSeconds(1f);
        doorIsOpening = false;
    }

    public void DoorStateChange()
    {
        doorState = DoorState.unlocked;
        plight1.color = Color.green;
        plight2.color = Color.green;
        print("DoorStateChange called");
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _player.currentDoorActive = this;

            switch (doorState)
            {
                case DoorState.unlocked:
                    _animator.SetBool("DoorActive", true);
                    _doorSFX.Play();
                    StartCoroutine(DoorOpenCheck());
                    _uIManager.DoorPowerDisplay("", Color.black);
                    break;
                case DoorState.locked:
                    if (powerLevelReached)
                    {
                        _uIManager.DoorPowerDisplay("Restore power (R)", Color.cyan);
                    }
                    else
                    {
                        _uIManager.DoorPowerDisplay("Power cores required", Color.red);
                    }
                    break;
            }
        }
      
    }

    private void OnTriggerExit(Collider other)
    {

        //playerInRange = false;
        if (other.CompareTag("Player"))
        {
            _uIManager.DoorPowerDisplay("", Color.clear);

            if (doorState == DoorState.unlocked)
            {
                _animator.SetBool("DoorActive", false);
                _doorSFX.Play();
            }

        }

        
    }

}
