using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PShoot : MonoBehaviour {

    private AudioSource _audioSource; 
    [SerializeField] private AudioClip[] _blastCannonSFX;
    [SerializeField] private AudioClip[] _miniCannonSFX;
    [SerializeField] private AudioClip[] _hyperBlasterSFX;
    [SerializeField] private AudioClip[] _rezoidSFX;
    public float currentDamage;
    public float blastDamage, miniDamage, hyperDamage, rezoidDamage;
    [SerializeField] private GameObject _blastCannon, _miniCannon, _hyperBlaster, _rezoid;

    public int bAmmo, mAmmo, hAmmo;
    public bool beginWeapon;

    [SerializeField] private float _fireRate; 
    private float _nextFire;

    public float hCannon, bCannon, sGun;
    public bool blastCannonPickUp, miniCannonPickUp, hyperBlasterPickUp;

    //to tell which weapon is active
    public bool bCannonFire, miniCannonFire, hyperBlasterFire, rezoidFire;
    public bool canFire;
    public bool inAmmoMode;

    public int currentWeapon;
    [SerializeField] private RayCastShoot _rayCastShoot;
    private GameManager _gameManager;
    private UIManager _uiManager;

    private void OnEnable()
    {
        GameEvents.EventPlayerDead += PlayerShootDisable;  
        GameEvents.EventGameWon += PlayerShootDisable;
        //GameEvents.EventSaveGameData += SendData;
    }
     
    private void OnDisable()
    {
        GameEvents.EventPlayerDead -= PlayerShootDisable;
        GameEvents.EventGameWon -= PlayerShootDisable;
        //GameEvents.EventSaveGameData -= SendData;
    }

    // Use this for initialization
    void Start ()
    {
        _uiManager = GameObject.Find("UI").GetComponent<UIManager>();
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        
        beginWeapon = true;
        inAmmoMode = false;
        bCannonFire = false;
        miniCannonFire = false;
        hyperBlasterFire = false;
        rezoidFire = false;
        _audioSource = GetComponent<AudioSource>();

        blastCannonPickUp = true;
        hyperBlasterPickUp = true;
        miniCannonPickUp = true;
        Debug.Log(System.DateTime.Now.Ticks + " p");

        if (_gameManager.gameState == GameState.Menu)
        {
            canFire = false;
        }
        else if (_gameManager.gameState == GameState.Play)
        {
            canFire = true;
        }

        if (GameManager.Instance.shouldLoad)
            GameEvents.ReportLoadLastSave();
    }

    public void SendData()
    {
        _gameManager.gm_blastCannonPickUp = blastCannonPickUp;
        _gameManager.gm_miniCannonPickUp = miniCannonPickUp;
        _gameManager.gm_hyperBlasterPickUp = hyperBlasterPickUp;
        _gameManager.gm_bAmmo = bAmmo;
        _gameManager.gm_mAmmo = mAmmo;
        _gameManager.gm_hAmmo = hAmmo;
    }

    // Update is called once per frame
    void Update ()
    {
        PInputWepChange();
        WeaponShootInput();
        if (beginWeapon)
        {
            WeaponChange(4);
            beginWeapon = false;
        }
	}
    
    public void PlayerShootDisable()
    {
        canFire = false;
    }

    private void WeaponShootInput()
    {
        if (bCannonFire && !inAmmoMode)
        {
            BlastCannonWep();
        }

        if (miniCannonFire && !inAmmoMode)
        {
            MiniCannonWep();
        }

        if (hyperBlasterFire && !inAmmoMode)
        {
            HyperBlasterWep();
        }

        if (rezoidFire && !inAmmoMode)
        {
            RezoidWep();
        }

    }

    //input for changing weapon
    private void PInputWepChange()
    {
        //weapon 1
        //check if weapon not selected before changing
        if (Input.GetKeyDown("2") && !bCannonFire && blastCannonPickUp)
        {
            WeaponChange(1);
            _uiManager.GunDisplay(2);
            //blast to active state
        }
        
        //weapon 2
        if (Input.GetKeyDown("3") && !miniCannonFire && miniCannonPickUp)
        {
            WeaponChange(2);
            _uiManager.GunDisplay(3);
            //mini to active state
        }

        //weapon 3
        if (Input.GetKeyDown("4") && !hyperBlasterFire && hyperBlasterPickUp)
        {
            WeaponChange(3);
            _uiManager.GunDisplay(4);
            //hyper to active state
        }

        if (Input.GetKeyDown("1") && !rezoidFire)
        {
            WeaponChange(4);
            _uiManager.GunDisplay(1);
            //rezoid to active state
            
        }
    }

    //if currentweapon is not == new weapon, deactivate current weapon and set bool to false.
    void DeActivateWeapon(int c)
    {
        //returns to idle state must very extremely quick
        if (c == 1)
        {
            bCannonFire = false;
            //rezoid back to idle state 
        }

        if (c == 2)
        {
            miniCannonFire = false;
            //blast back to idle state
        }

        if (c == 3)
        {
            hyperBlasterFire = false;
            //mini back to idle state
        }

        if (c == 4)
        {
            rezoidFire = false;
            //hyper back to idle state
        }
    }

    void WeaponChange(int weapon)
    {
        //check current weapon then set new current weapon via DeActivation method
        //check weapon int upon call
        //set relevant weapon bool to true
        //animate weapon + weapon delay
        if (weapon == 1)
        {
            print("weapon 1");
            currentDamage = blastDamage;

            if (weapon != currentWeapon)
            {
                DeActivateWeapon(currentWeapon);
                currentWeapon = weapon;
            } else
            {
                currentWeapon = weapon;
            }
            StartCoroutine(WeaponTransitionAction(1));
            bCannonFire = true;
        }

        if (weapon == 2)
        {
            print("weapon 2");
            currentDamage = miniDamage;

            if (weapon != currentWeapon)
            {
                DeActivateWeapon(currentWeapon);
                currentWeapon = weapon;
            }
            else
            {
                currentWeapon = weapon;
            }
            StartCoroutine(WeaponTransitionAction(2));
            miniCannonFire = true;

        }

        if (weapon == 3)
        {
            print("weapon 3");
            currentDamage = hyperDamage;

            if (weapon != currentWeapon)
            {
                DeActivateWeapon(currentWeapon);
                currentWeapon = weapon;
            }
            else
            {
                currentWeapon = weapon;
            }
            StartCoroutine(WeaponTransitionAction(3));
            hyperBlasterFire = true;
        }

        if (weapon == 4)
        {
            print("Weapon 4");
            currentDamage = rezoidDamage;

            if (weapon != currentWeapon)
            {
                DeActivateWeapon(currentWeapon);
                currentWeapon = weapon;
            }
            else
            {
                currentWeapon = weapon;
            }
            StartCoroutine(WeaponTransitionAction(3));
            rezoidFire = true;
        }

    }

    
    //Hyper Cannon behaviour
    private void BlastCannonWep()
    {
        _fireRate = 0.35f;

        //check canFire, player input and firerate
        if (canFire && Input.GetMouseButtonDown(0) && Time.time > _nextFire && bAmmo >= 1)
        {
            bAmmo--;
            _nextFire = Time.time + _fireRate;
            PlaySound(1);
            _rayCastShoot.RayShoot(1);
        }
       
    }

    //Blast Cannon behaviour
    private void MiniCannonWep()
    {
        _fireRate = 0.1f;

        //check canFire, player input and firerate
        if (canFire && Input.GetMouseButton(0) && Time.time > _nextFire && mAmmo >= 1)
        {
            mAmmo--;
            _nextFire = Time.time + _fireRate;
            PlaySound(2);
            _rayCastShoot.RayShoot(1);
        }
    }

    //Shotgun behaviour
    private void HyperBlasterWep()
    {
        _fireRate = 0.9f;

        //check canFire, player input and firerate
        if (canFire && Input.GetMouseButtonDown(0) && Time.time > _nextFire && hAmmo >= 1)
        {
            _nextFire = Time.time + _fireRate;
            hAmmo--;
            PlaySound(3);
            _rayCastShoot.RayShoot(1);
        }
    }

    //Rezoid Behaviour
    private void RezoidWep()
    {
        _fireRate = 0.29f;

        if (canFire && Input.GetMouseButtonDown(0) && Time.time > _nextFire)
        {
            _nextFire = Time.time + _fireRate;
            PlaySound(4);
            _rayCastShoot.RayShoot(1);
        }
    }

    void PlaySound(int c)
    {
        //play sound depending on c upon call
        //randomise audioclip to play
        if (c == 1)
        {
            int n = Random.Range(0, _blastCannonSFX.Length);
            _audioSource.PlayOneShot(_blastCannonSFX[n]);
        }

        if (c == 2)
        {
            int n = Random.Range(0, _miniCannonSFX.Length);
            _audioSource.PlayOneShot(_miniCannonSFX[n]);
        }

        if (c == 3)
        {
            int n = Random.Range(0, _hyperBlasterSFX.Length);
            _audioSource.PlayOneShot(_hyperBlasterSFX[n]);
        }

        if (c == 4)
        {
            int n = Random.Range(0, _hyperBlasterSFX.Length);
            _audioSource.PlayOneShot(_rezoidSFX[n]);
        }
    }

    IEnumerator WeaponTransitionAction(int c)
    {


        if (c == 1)
        {
            //animate BlastCannon
        }
        if (c == 2)
        {
            //animate miniCannon
        }
        if (c == 3)
        {
            //animate HyperBlaster
        }

        if (c == 4)
        {
            //animate rezoid
        }

        //fire delay during animation
        canFire = false;
        yield return new WaitForSeconds(0.4f);
        canFire = true;

        StopCoroutine(WeaponTransitionAction(c));
    }

    void RayTagReturn()
    {
        RaycastHit hit;

        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 1000f))
        {
            Debug.Log(hit.collider.gameObject.tag);
        }

    }
    
}
