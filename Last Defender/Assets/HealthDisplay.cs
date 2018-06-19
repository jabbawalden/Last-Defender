using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour {

    [SerializeField] private Text _healthDisplay;
    private CharacterMotor _characterMotor;

	void Start ()
    {
        _characterMotor = GameObject.Find("_PlayerMove").GetComponent<CharacterMotor>();
	}


	void Update ()
    {
        _healthDisplay.text = "HEALTH: " + _characterMotor.health;
	}
}
