using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUpgrades : MonoBehaviour {

    private CharacterMotor _characterMotor;
    public GameObject playerUpgrades;
	// Use this for initialization
	void Start ()
    {
        playerUpgrades.SetActive(false);
        _characterMotor = GameObject.Find("PlayerMain").GetComponent<CharacterMotor>();
	}
	
    public void MobilityUpgrade()
    {
        _characterMotor.speed += 1;
        playerUpgrades.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        _characterMotor.canShoot = true;
    }

    public void LightPowerUpgrade()
    {
        _characterMotor.maxLightPower += 200;
        playerUpgrades.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        _characterMotor.canShoot = true;
    }

    public void ArmourUpgrade()
    {
        _characterMotor.armour += 0.5f;
        playerUpgrades.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        _characterMotor.canShoot = true;
    }
    
}
