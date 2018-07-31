﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    [SerializeField] private Text _healthDisplay;
    [SerializeField] private Text _ammoDisplay;
    [SerializeField] private Text _lightPowerDisplay;
    [SerializeField] private Text _doorPowerDisplay;
    public Text interactE;
    private CharacterMotor _characterMotor;
    private PShoot _pShoot;
    [SerializeField] private Text bCAmmoDisplay, mCAmmoDisplay, hBAmmoDisplay;
    public AmmoRefill ammoRefill;
    public GameObject UIammoRefill, UIplayerUpgrades;
    public GameObject SuitInstructions;

    private void OnEnable()
    {
        
    }
    private void OnDisable()
    {
        
    }

    void Start()
    {
        SuitInstructions.SetActive(false);
        interactE.gameObject.SetActive(false);
        UIammoRefill.SetActive(false);
        UIplayerUpgrades.SetActive(false);
        _characterMotor = GameObject.Find("PlayerMain").GetComponent<CharacterMotor>();
        _pShoot = GameObject.Find("PlayerMain").GetComponent<PShoot>();
        DoorPowerDisplay("", Color.black);
        interactE.text = "";
        bCAmmoDisplay.text = "Blast Cannon + " + ammoRefill.blastCannonA;
        mCAmmoDisplay.text = "Mini Cannon + " + ammoRefill.miniCannonA;
        hBAmmoDisplay.text = "Hyper Blaster + " + ammoRefill.hyperBlasterA;
    }


    void Update()
    {
        //Health UI
        _healthDisplay.text = "HEALTH: " + _characterMotor.health;

        //Light UI
        _lightPowerDisplay.text = "POWER: " + _characterMotor.lightPower;

        AmmoDisplay();
        SuitInstructionsController();

    }

    void AmmoDisplay()
    {

        //Ammo UI
        if (_pShoot.bCannonFire)
            _ammoDisplay.text = "Blast Cannon: " + _pShoot.bAmmo;

        if (_pShoot.miniCannonFire)
            _ammoDisplay.text = "Mini Cannon: " + _pShoot.mAmmo;

        if (_pShoot.hyperBlasterFire)
            _ammoDisplay.text = "Hyper Blaster: " + _pShoot.hAmmo;

        if (_pShoot.rezoidFire)
            _ammoDisplay.text = "Rezoid: Infinite";
    }

    public void DoorPowerDisplay(string powerState, Color colour)
    {
        _doorPowerDisplay.text = powerState;
        _doorPowerDisplay.color = colour;
    }

    public void SuitInstructionsController()
    {
        if (SuitInstructions.activeInHierarchy == false)
        {
            interactE.text = "Interact (E)";
        }
        else if (SuitInstructions.activeInHierarchy == true)
        {
            interactE.text = "Exit Application (E)";
        }
    }
    
}
