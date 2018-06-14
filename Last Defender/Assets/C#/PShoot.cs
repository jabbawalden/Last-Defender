using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PShoot : MonoBehaviour {

    private AudioSource _audioSource; 
    [SerializeField] private AudioClip[] _blastCannonSFX;
    [SerializeField] private AudioClip[] _miniCannonSFX;
    [SerializeField] private AudioClip[] _hyperBlasterSFX;
    public int currentDamage;
    public int blastDamage, miniDamage, hyperDamage;
    [SerializeField] private GameObject _hyperCannon, _BlastCannon, _hyperBlaste;

    [SerializeField] private float _fireRate; 
    private float _nextFire;

    public float hCannon, bCannon, sGun;

    [SerializeField] private GameObject _cameraPos;

    //to tell which weapon is active
    public bool bCannonFire, miniCannonFire, hyperBlasterFire;

    [SerializeField] private int currentWeapon;
    [SerializeField] private RayCastShoot _rayCastShoot;

    // Use this for initialization
    void Start ()
    {
        _cameraPos = GameObject.Find("Camera");
        bCannonFire = false;
        miniCannonFire = false;
        hyperBlasterFire = false;
        _audioSource = GetComponent<AudioSource>();
        //StartCoroutine(DebugUpdate());
	}

    /*
    IEnumerator DebugUpdate()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            Debug.Log(_cameraPos.transform.position);
        }
        
    }
    */

	// Update is called once per frame
	void Update ()
    {
        PInputWepChange();
        WeaponShootInput();
        
	}
    
    private void WeaponShootInput()
    {
        if (bCannonFire)
        {
            BlastCannonWep();
        }

        if (miniCannonFire)
        {
            MiniCannonWep();
        }

        if (hyperBlasterFire)
        {
            HyperBlasterWep(); 
        }
    }

    private void PInputWepChange()
    {
        //weapon 1
        if (Input.GetKeyDown("1") && !bCannonFire)
        {
            WeaponChange(1);
        }
        
        //weapon 2
        if (Input.GetKeyDown("2") && !miniCannonFire)
        {
            WeaponChange(2);
        }

        //weapon 3
        if (Input.GetKeyDown("3") && !hyperBlasterFire)
        {
            WeaponChange(3);
        }
    }

    //if currentweapon is not == new weapon, deactivate current weapon and set bool to false.
    void DeActivateWeapon(int c)
    {
        if (c == 1)
        {
            bCannonFire = false;
        }

        if (c == 2)
        {
            miniCannonFire = false;
        }

        if (c == 3)
        {
            hyperBlasterFire = false;
        }
    }

    void WeaponChange(int weapon)
    {
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
            //animate weapon
            StartCoroutine(WeaponTransitionAction(1));
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
            //animate weapon

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
        }
  
    }


    //Hyper Cannon behaviour
    private void BlastCannonWep()
    {
        _fireRate = 0.35f;

        if (Input.GetMouseButtonDown(0) && Time.time > _nextFire)
        {
            _nextFire = Time.time + _fireRate;
            PlaySound(1);
            _rayCastShoot.RayShoot(1);
        }
       
    }

    //Blast Cannon behaviour
    private void MiniCannonWep()
    {
        _fireRate = 0.15f;

        if (Input.GetMouseButton(0) && Time.time > _nextFire)
        {
            _nextFire = Time.time + _fireRate;
            PlaySound(2);
            _rayCastShoot.RayShoot(1);
        }
    }

    //Shotgun behaviour
    private void HyperBlasterWep()
    {
        _fireRate = 1.1f;

        if (Input.GetMouseButton(0) && Time.time > _nextFire)
        {
            _nextFire = Time.time + _fireRate;
            PlaySound(3);
            _rayCastShoot.RayShoot(1);
        }
    }

    void PlaySound(int c)
    {
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


    }

    IEnumerator WeaponTransitionAction(int c)
    {
        yield return new WaitForSeconds(0.5f);

        if (c == 1)
        {
            bCannonFire = true;
        }
        if (c == 2)
        {
            miniCannonFire = true;
        }
        if (c == 3)
        {
            hyperBlasterFire = true;
        }

        StopCoroutine(WeaponTransitionAction(1));
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
