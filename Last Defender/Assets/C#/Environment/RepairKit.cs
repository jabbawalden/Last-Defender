﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairKit : MonoBehaviour {

    public GameObject _playerUpgrades;
    private PShoot _characterShoot;
	// Use this for initialization
	void Start ()
    {
        _characterShoot = GameObject.Find("PlayerMain").GetComponent<PShoot>();
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _playerUpgrades.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Destroy(gameObject);
            _characterShoot.canFire = false;
        }
            
    }   

}
