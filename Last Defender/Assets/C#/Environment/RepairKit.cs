﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairKit : MonoBehaviour {

    private GameManager _gameManager;
    public string repairKitID = "Undefined";
    public GameObject playerUpgrades;
    private PShoot _characterShoot;
    private CharacterLook _characterLook;
    private CharacterMotor _characterMotor;

    // Use this for initialization
    void Start ()
    {
        _characterShoot = GameObject.Find("PlayerMain").GetComponent<PShoot>();
        _characterLook = GameObject.Find("Camera").GetComponent<CharacterLook>();
        _characterMotor = GameObject.Find("PlayerMain").GetComponent<CharacterMotor>();
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        if (_gameManager.usedRepairKits.Contains(repairKitID))
        {
            Destroy(gameObject);
            return;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            AddID();
            _characterMotor.canMove = false;
            playerUpgrades.SetActive(true);
            _characterLook.canLook = false;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            _characterShoot.canFire = false;
            Destroy(gameObject);
            
        }
            
    }

    void AddID()
    {
        if (!_gameManager.usedRepairKits.Contains(repairKitID))
        {
            _gameManager.usedRepairKits.Add(repairKitID);
        }
    }

}
