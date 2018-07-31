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
    public AudioClip doorClip;

    public bool powerLevelReached;
    public int powerLevelRequirement;
    public bool doorIsOpening;
    private CharacterMotor _player;
    private UIManager _uIManager;
    public bool playerInRange;
    public bool unlocked;
    public bool open;

    private void Start()
    {
        open = false;
        doorIsOpening = false;
        powerLevelReached = false;
        _player = GameObject.Find("PlayerMain").GetComponent<CharacterMotor>();
        _uIManager = GameObject.Find("UI").GetComponent<UIManager>();

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


    IEnumerator DoorCheck()
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

    public void OpenDoor()
    {
        if (!doorIsOpening)
        {
            _animator.SetBool("DoorActive", true);
            _doorSFX.PlayOneShot(doorClip);
            StartCoroutine(DoorCheck());
            _uIManager.DoorPowerDisplay("", Color.black);
            open = true;
        }
       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _player.currentDoorActive = this;
            _player.canOpenDoor = true;

            switch (doorState)
            {
                case DoorState.unlocked:
                        _uIManager.DoorPowerDisplay("Open Door (Q)", Color.blue);
                    break;
                case DoorState.locked:
                    if (powerLevelReached)
                    {
                        _uIManager.DoorPowerDisplay("Restore power (Q)", Color.cyan);
                    }
                    else
                    {
                        _uIManager.DoorPowerDisplay("Power cores required", Color.red);
                    }
                    break;
            }
        }
    }

    /*
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _player.currentDoorActive = this;

            switch (doorState)
            {
                case DoorState.unlocked:
                    if (!doorIsOpening)
                    {
                        _animator.SetBool("DoorActive", true);
                        _doorSFX.PlayOneShot(doorClip);
                        StartCoroutine(DoorCheck());
                        _uIManager.DoorPowerDisplay("", Color.black);
                    }
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
    */

    private void OnTriggerExit(Collider other)
    {

        //playerInRange = false;
        if (other.CompareTag("Player"))
        {
            _uIManager.DoorPowerDisplay("", Color.clear);
            _player.canOpenDoor = false;
            if (doorState == DoorState.unlocked && open)
            {
                StartCoroutine(DoorCloseBehaviour());
            }

        }

    }

    IEnumerator DoorCloseBehaviour()
    {
        yield return new WaitForSeconds(0.5f);
        _animator.SetBool("DoorActive", false);
        _doorSFX.Play();
        open = false;
    }
}
