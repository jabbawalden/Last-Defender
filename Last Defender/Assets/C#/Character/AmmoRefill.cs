using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoRefill : MonoBehaviour {


    private CharacterMotor _characterMotor;
    private PShoot _pShoot;
    public GameObject ammoRefill;
    private CharacterLook _characterLook;
    public int blastCannonA, miniCannonA, hyperBlasterA; 

    // Use this for initialization
    void Start ()
    {
        ammoRefill.SetActive(false);
        _characterMotor = GameObject.Find("PlayerMain").GetComponent<CharacterMotor>();
        _pShoot = GameObject.Find("PlayerMain").GetComponent<PShoot>();
        _characterLook = GameObject.Find("Camera").GetComponent<CharacterLook>();
    }
	
    public void BlastCannonRefill()
    {
        ammoRefill.SetActive(false);
        _characterMotor.canMove = true;
        _pShoot.bAmmo += blastCannonA;
        _characterLook.canLook = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        _pShoot.canFire = true;
    }

    public void MiniCannonRefill()
    {
        ammoRefill.SetActive(false);
        _characterMotor.canMove = true;
        _pShoot.mAmmo += miniCannonA;
        _characterLook.canLook = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        _pShoot.canFire = true;
    }

    public void HyperCannonRefill()
    {
        ammoRefill.SetActive(false);
        _characterMotor.canMove = true;
        _pShoot.hAmmo += hyperBlasterA;
        _characterLook.canLook = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        _pShoot.canFire = true;
    }
}
