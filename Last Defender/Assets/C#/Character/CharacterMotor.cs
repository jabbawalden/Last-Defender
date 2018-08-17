using System.Collections;
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
    public bool lightOn;
    //public int powerCoresCollected = 0;
    //public int lightRecoveryAmount;
    public float maxLightPower;
    public float lightPower;
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
    private GameManager _gameManager;

    [SerializeField] private Animator _playerAnim;
    public GameObject[] hitPos;

    private void OnEnable()
    {
        GameEvents.EventGameWon += PlayerWon;
    }

    private void OnDisable()
    {
        GameEvents.EventGameWon -= PlayerWon;
    }

    // Use this for initialization
    void Start ()
    {
        health = maxHealth - 10;
        canShoot = true;
        canMove = true;
        _cursorshown = false;
        lightOn = false;
        spotLight.SetActive(false);
        speed *= Time.deltaTime;
        interactE = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        _uiManager = GameObject.Find("UI").GetComponent<UIManager>();
        _pShoot = GetComponent<PShoot>();
        _characterLook = GameObject.Find("Camera").GetComponent<CharacterLook>();
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        //powerCoresCollected = _gameManager.gm_PowerCores;
    }
	
	// Update is called once per frame
	void Update ()
    {
        _translation = Input.GetAxis("Vertical") * speed;
        _strafe = Input.GetAxis("Horizontal") * speed;

        InteractSuitInstruction();

        if (canMove)
        {
            MovementInput();
            LightEnable();
        }
        
        if (health <= 0)
        {
            canMove = false;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            GameEvents.ReportPlayerDead();
            PlayerDead();
        }



        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_uiManager.menuShown == false)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                _cursorshown = true;
                canMove = false;
                _characterLook.canLook = false;
                _uiManager.inGameMenu.SetActive(true);
                _pShoot.canFire = false;
                _uiManager.menuShown = true;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                _cursorshown = false;
                canMove = true;
                _characterLook.canLook = true;
                _uiManager.inGameMenu.SetActive(false);
                _pShoot.canFire = true;
                _uiManager.menuShown = false;
            }
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
            if (currentDoorActive.powerLevelReached && Input.GetKeyDown(KeyCode.E) && canOpenDoor && !currentDoorActive.open)
            {
                currentDoorActive.DoorStateChange();
            }
            if (currentDoorActive.doorState == DoorActivate.DoorState.unlocked && Input.GetKeyDown(KeyCode.E) && canOpenDoor && !currentDoorActive.open)
            {
                currentDoorActive.OpenDoor();
            }

        }
        
    }

    public void PlayerWon()
    { 
        canMove = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
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
            if (Input.GetKeyDown(KeyCode.E) && _uiManager.suitInstructions.activeInHierarchy == false)
            {
                canMove = false;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                _pShoot.canFire = false;
                _characterLook.canLook = false;
                _uiManager.suitInstructions.SetActive(true);
            }
            else if (Input.GetKeyDown(KeyCode.E) && _uiManager.suitInstructions.activeInHierarchy == true)
            {
                canMove = true;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                _pShoot.canFire = true;
                _characterLook.canLook = true;
                _uiManager.suitInstructions.SetActive(false);
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
                lightPower += maxLightPower / 600;

                if (lightPower > maxLightPower)
                    lightPower = maxLightPower;
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
        _uiManager.deathScreen.SetActive(true);

    }


}
