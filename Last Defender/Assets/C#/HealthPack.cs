using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPack : MonoBehaviour {

    public string healthPackID = "Undefined"; 

    [SerializeField]
    private int healthAmount;

    private GameManager _gameManager;
    private CharacterMotor _characterMotor;

    private void Start()
    {
        _characterMotor = GameObject.Find("PlayerMain").GetComponent<CharacterMotor>();
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (_characterMotor.health < _characterMotor.maxHealth)
            {
                if (_characterMotor.health <= _characterMotor.maxHealth - healthAmount)
                {
                    _characterMotor.health += healthAmount;
                    AddID();
                    Destroy(gameObject);
                }
                else if (_characterMotor.health > _characterMotor.maxHealth - healthAmount)
                {
                    _characterMotor.health = _characterMotor.maxHealth;
                    AddID();
                    Destroy(gameObject);
                }
            }
            
        }
    }

    void AddID()
    {
        if (!_gameManager.usedHealthPack.Contains("healthPackID"))
        {
            _gameManager.usedHealthPack.Add(healthPackID);
        }
    }

}
