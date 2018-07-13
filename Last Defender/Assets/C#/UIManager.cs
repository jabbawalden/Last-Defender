using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    [SerializeField] private Text _healthDisplay;
    [SerializeField] private Text _ammoDisplay;
    [SerializeField] private Text _lightPowerDisplay;
    [SerializeField] private Text _doorPowerDisplay; 
    private CharacterMotor _characterMotor;
    private PShoot _pShoot;

    private void OnEnable()
    {
        
    }
    private void OnDisable()
    {
        
    }

    void Start()
    {
        _characterMotor = GameObject.Find("PlayerMain").GetComponent<CharacterMotor>();
        _pShoot = GameObject.Find("PlayerMain").GetComponent<PShoot>();
        DoorPowerDisplay("");
    }


    void Update()
    {
        //Health UI
        _healthDisplay.text = "HEALTH: " + _characterMotor.health;

        //Light UI
        _lightPowerDisplay.text = "POWER: " + _characterMotor.lightPower;

        AmmoDisplay();

    }

    void AmmoDisplay()
    {

        //Ammo UI
        if (_pShoot.bCannonFire)
            _ammoDisplay.text = "AMMO: " + _pShoot.bAmmo;

        if (_pShoot.miniCannonFire)
            _ammoDisplay.text = "AMMO: " + _pShoot.mAmmo;

        if (_pShoot.hyperBlasterFire)
            _ammoDisplay.text = "AMMO: " + _pShoot.hAmmo;

        if (_pShoot.rezoidFire)
            _ammoDisplay.text = "AMMO: N/A";
    }

    public void DoorPowerDisplay(string powerState)
    {
        _doorPowerDisplay.text = powerState;

    }

    
}
