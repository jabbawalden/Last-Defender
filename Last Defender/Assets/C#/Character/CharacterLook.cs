﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterLook : MonoBehaviour {


    [SerializeField] private float _sensitivity;
    [SerializeField] private float _smoothing;
    private Vector2 _mouseLook;
    private Vector2 _smoothV;
    public bool canLook;

    private GameObject _character;
    private CharacterMotor _pCharMotor;
    private GameManager _gameManager;

    private void OnEnable()
    {
        GameEvents.EventPlayerDead += PlayerLookDisable;
        GameEvents.EventGameWon += PlayerLookDisable;
    }

    private void OnDisable()
    {
        GameEvents.EventPlayerDead -= PlayerLookDisable;
        GameEvents.EventGameWon -= PlayerLookDisable;
    }

    // Use this for initialization
    void Start ()
    {
        _character = this.transform.parent.gameObject;
        _pCharMotor = GameObject.Find("PlayerMain").GetComponent<CharacterMotor>();
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        if (_gameManager.gameState == GameState.Menu)
        {
            canLook = false;
        }
        else if (_gameManager.gameState == GameState.Play)
        {
            canLook = true;
        }


    }
	
	// Update is called once per frame
	void Update ()
    {
        if (_pCharMotor.health >= 1 && canLook)
        {
            CanMouseLook();
        }

    }

    public void PlayerLookDisable()
    {
        canLook = false;
    }

    private void CanMouseLook()
    {
      
        //set input to getaxisraw
        var inputA = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxis("Mouse Y"));
        //getaxisraw vector2 takes two floats, sensitivity * smoothing
        inputA = Vector2.Scale(inputA, new Vector2(_sensitivity * _smoothing, _sensitivity * _smoothing));

        _smoothV.x = Mathf.Lerp(_smoothV.x, inputA.x, 1f / _smoothing);
        _smoothV.y = Mathf.Lerp(_smoothV.y, inputA.y, 1f / _smoothing);

        _mouseLook += _smoothV;
        //mouse locks past this point - stops full rotation.
        _mouseLook.y = Mathf.Clamp(_mouseLook.y, -65, 65);

        transform.localRotation = Quaternion.AngleAxis(-_mouseLook.y, Vector3.right);
        _character.transform.localRotation = Quaternion.AngleAxis(_mouseLook.x, Vector3.up);

    }
}
