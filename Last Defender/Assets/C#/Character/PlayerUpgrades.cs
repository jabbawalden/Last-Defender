using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUpgrades : MonoBehaviour {

    private CharacterMotor _characterMotor;
    private PShoot _pShoot;
    public GameObject playerUpgrades;
    private CharacterLook _characterLook;
	// Use this for initialization

	void Start ()
    {
        playerUpgrades.SetActive(false);
        _characterMotor = GameObject.Find("PlayerMain").GetComponent<CharacterMotor>();
        _pShoot = GameObject.Find("PlayerMain").GetComponent<PShoot>();
        _characterLook = GameObject.Find("Camera").GetComponent<CharacterLook>();
    }
	
    public void MobilityUpgrade()
    {
        _characterMotor.speed += 0.019f;
        _characterMotor.jump += 0.4f;
        _characterMotor.canMove = true;
        playerUpgrades.SetActive(false);
        _characterLook.canLook = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        _pShoot.canFire = true;
    }

    public void LightPowerUpgrade()
    {
        _characterMotor.maxLightPower += 200;
        _characterMotor.canMove = true;
        playerUpgrades.SetActive(false);
        _characterLook.canLook = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        _pShoot.canFire = true;
    }

    public void ArmourUpgrade()
    {
        _characterMotor.armour += 0.5f;
        _characterMotor.canMove = true;
        playerUpgrades.SetActive(false);
        _characterLook.canLook = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        _pShoot.canFire = true;
    }
 
}
