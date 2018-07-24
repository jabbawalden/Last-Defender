using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairKit : MonoBehaviour {

    public GameObject _playerUpgrades;
    private CharacterMotor _characterMotor;
	// Use this for initialization
	void Start ()
    {
        _characterMotor = GameObject.Find("PlayerMain").GetComponent<CharacterMotor>();
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _playerUpgrades.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Destroy(this);
            _characterMotor.canShoot = false;
        }
            
    }   

}
