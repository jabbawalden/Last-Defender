﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUpgrades : MonoBehaviour {

    private CharacterMotor _characterMotor;
    private PShoot _pShoot;
    public GameObject playerUpgrades;
	// Use this for initialization
	void Start ()
    {
        playerUpgrades.SetActive(false);
        _characterMotor = GameObject.Find("PlayerMain").GetComponent<CharacterMotor>();
        _pShoot = GameObject.Find("PlayerMain").GetComponent<PShoot>();
    }
	
    public void MobilityUpgrade()
    {
        _characterMotor.speed += 0.021f;
        playerUpgrades.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        _pShoot.canFire = true;
    }

    public void LightPowerUpgrade()
    {
        _characterMotor.maxLightPower += 200;
        playerUpgrades.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        _pShoot.canFire = true;
    }

    public void ArmourUpgrade()
    {
        _characterMotor.armour += 0.5f;
        playerUpgrades.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        _pShoot.canFire = true;
    }
 
}
