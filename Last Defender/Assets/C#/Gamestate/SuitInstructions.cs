using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuitInstructions : MonoBehaviour {

    private UIManager _uiManager;
    private CharacterMotor _characterMotor;
	// Use this for initialization
	void Start ()
    {
        _uiManager = GameObject.Find("UI").GetComponent<UIManager>();
        _characterMotor = GameObject.Find("PlayerMain").GetComponent<CharacterMotor>();

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _uiManager.interactE.gameObject.SetActive(true);
            _characterMotor.instructionsE = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _uiManager.interactE.gameObject.SetActive(false);
            _characterMotor.instructionsE = false;
        }
            
    }
}
