﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterMotor : MonoBehaviour {

    public float health;
    public float maxHealth;
    public float armour;
    
    //movement
    public float speed;
    [SerializeField] private Rigidbody _rb;
    public float jump;

    //Light capabilities
    public GameObject spotLight;
    public float lightPower;
    public bool lightOn;
    public int powerCoresCollected = 0;
    public int lightRecoveryAmount;
    public float maxLightPower;
    public bool canOpenDoor;
    public bool canMove;

    private bool _cursorshown;

    float _translation;
    float _strafe;

    public bool canShoot;
    public DoorActivate currentDoorActive;
    public bool interactE;
    public bool instructionsE;

    private UIManager _uiManager;
    private PShoot _pShoot;
    private CharacterLook _characterLook;

    [SerializeField] private Animator _playerAnim;
    public GameObject[] hitPos;

   

    private void OnEnable()
    {
        GameEvents.EventPlayerDead += PlayerDead;
    }

    private void OnDisable()
    {
        GameEvents.EventPlayerDead -= PlayerDead;
    }
    // Use this for initialization
    void Start ()
    {
        health = maxHealth;
        canShoot = true;
        canMove = true;
        _cursorshown = false;
        lightRecoveryAmount = 1;
        lightOn = false;
        spotLight.SetActive(false);
        speed *= Time.deltaTime;
        interactE = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        _uiManager = GameObject.Find("UI").GetComponent<UIManager>();
        _pShoot = GetComponent<PShoot>();
        _characterLook = GameObject.Find("Camera").GetComponent<CharacterLook>();

    }
	
	// Update is called once per frame
	void Update ()
    {
        _translation = Input.GetAxis("Vertical") * speed;
        _strafe = Input.GetAxis("Horizontal") * speed;

        InteractSuitInstruction();

        if (health >= 1 && canMove)
        {
            MovementInput();
            LightEnable();
        }
        else if (health <= 0)
        {
            GameEvents.ReportPlayerDead();
        }
        
        //head bob
        if (IsWalking() && canMove)
        {
            _playerAnim.SetBool("IsWalking", true);
        }
        else
        {
            _playerAnim.SetBool("IsWalking", false);
        }

        if (currentDoorActive != null)
        {
            if (currentDoorActive.powerLevelReached && Input.GetKeyDown(KeyCode.Q) && canOpenDoor && !currentDoorActive.open)
            {
                currentDoorActive.DoorStateChange();
            }
            if (currentDoorActive.doorState == DoorActivate.DoorState.unlocked && Input.GetKeyDown(KeyCode.Q) && canOpenDoor && !currentDoorActive.open)
            {
                currentDoorActive.OpenDoor();
            }

        }
        
    }


    private void MovementInput()
    {
        //WASD movement
        //force
        transform.Translate(_strafe, 0, _translation);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (CanJump())
            {
                _rb.AddForce(jump * transform.up, ForceMode.Impulse);
            }
        }

        //add mouse back in during play
        if (Input.GetKeyDown(KeyCode.Escape) && !_cursorshown)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            _cursorshown = true;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && _cursorshown)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            _cursorshown = false;
        }

      
    }

    bool CanJump()
    {
        Ray ray = new Ray(transform.position, transform.up * -1);
        RaycastHit hit;

        //localscale.y is maxdistance of ray
        if (Physics.Raycast(ray, out hit, transform.localScale.y + 0.2f))
        {
            return true;
        }
        return false;
    }

 
    bool IsWalking()
    {
        if (Mathf.Abs(_strafe) == 0 && Mathf.Abs(_translation) == 0)
        {
            _playerAnim.SetBool("IsWalking", false);
            return false;
        }
        //animate head bob

        return true;
    }

    public void InteractSuitInstruction()
    {
        if (instructionsE)
        {
            if (Input.GetKeyDown(KeyCode.E) && _uiManager.SuitInstructions.activeInHierarchy == false)
            {
                canMove = false;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                _pShoot.canFire = false;
                _characterLook.canLook = false;
                _uiManager.SuitInstructions.SetActive(true);
            }
            else if (Input.GetKeyDown(KeyCode.E) && _uiManager.SuitInstructions.activeInHierarchy == true)
            {
                canMove = true;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                _pShoot.canFire = true;
                _characterLook.canLook = true;
                _uiManager.SuitInstructions.SetActive(false);
            }
        }
    }
    /*
    private IEnumerator Death()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(0);
        //upon death, data from last save point must be reloaded, otherwise enemies that were killed before a new save will not spawn
    }
    */

    private void LightEnable()
    {
        if (Input.GetMouseButtonDown(1) && !lightOn)
        {
            lightOn = true;
        }
        else if (Input.GetMouseButtonDown(1) && lightOn)
        {
            lightOn = false;
        }

        if (lightOn)
        {
            spotLight.SetActive(true);
            lightPower--;

        }
        else if (!lightOn)
        {
            spotLight.SetActive(false);

            if (lightPower < maxLightPower)
            {
                lightPower += lightRecoveryAmount;
            }
        }


        if (lightPower <= 0)
        {
            lightOn = false;
        }

    }


    public void PlayerDead()
    {
        Debug.Log("Player is dead");
        StartCoroutine(PlayerRestart());
        //animate camera death
        //add in blood image overlay
        //play sound
        //
    }

    IEnumerator PlayerRestart()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(0);
    }

}
