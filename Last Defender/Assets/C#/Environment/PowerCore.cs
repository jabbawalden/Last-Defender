using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerCore : MonoBehaviour
{
    public string powerCoreID = "Undefined";
    private GameManager _gameManager;
    //create reference
    private CharacterMotor _player;

    void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        if (_gameManager.usedPowerCore.Contains(powerCoreID))
        {
            Destroy(gameObject);
            return;
        }
        //set reference
        _player = GameObject.Find("PlayerMain").GetComponent<CharacterMotor>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //+1 to power cores collected
            _player.powerCoresCollected++;
            AddID();
            Destroy(this.gameObject, 1f);
        }
    }


    void AddID()
    {
        if (!_gameManager.usedPowerCore.Contains(powerCoreID))
        {
            _gameManager.usedPowerCore.Add(powerCoreID);
        }
    }
}