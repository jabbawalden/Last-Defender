using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PShoot : MonoBehaviour {

    
    public AudioSource[] hyperCannon;
    public AudioSource[] blastCannon;
    public AudioSource[] shotgun;

    [SerializeField] private GameObject _hyperCannon, _BlastCannon, _Shotgun;

    [SerializeField] private float _fireRate; 
    private float _nextFire;

    public float hCannon, bCannon, sGun;

    [SerializeField] private GameObject _cameraPos;

    //to tell which weapon is active
    public bool hCannonFire, bCannonFire, shotGunFire;

    [SerializeField] private int currentWeapon;

    // Use this for initialization
    void Start ()
    {
        _cameraPos = GameObject.Find("Camera");
        hCannonFire = false;
        bCannonFire = false;
        shotGunFire = false;

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
        
        if (Input.GetMouseButton(0))
        {
            RayTagReturn();
        }
	}
    
    private void WeaponShootInput()
    {
        if (hCannonFire)
        {
            HyperCannonWep();
        }

        if (bCannonFire)
        {
            BlastCannonWep();
        }

        if (shotGunFire)
        {
            ShotgunWep(); 
        }
    }

    private void PInputWepChange()
    {
        //weapon 1
        if (Input.GetKeyDown("1") && !hCannonFire)
        {
            WeaponChange(1);
            //change firerate
            //change damage
        }
        
        //weapon 2
        if (Input.GetKeyDown("2") && !bCannonFire)
        {
            WeaponChange(2);
            //change firerate
            //change damage
        }

        //weapon 3
        if (Input.GetKeyDown("3") && !shotGunFire)
        {
            WeaponChange(3);
            //change firerate
            //change damage
        }
    }

    //if currentweapon is not == new weapon, deactivate current weapon and set bool to false.
    void DeActivateWeapon(int c)
    {
        if (c == 1)
        {
            hCannonFire = false;
        }

        if (c == 2)
        {
            bCannonFire = false;
        }

        if (c == 3)
        {
            shotGunFire = false;
        }
    }

    void WeaponChange(int weapon)
    {
        if (weapon == 1)
        {
            print("weapon 1");

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
    private void HyperCannonWep()
    {
        //firerate
        //_fireRate = hCannon;
        _fireRate = 0.5f;

        if (Input.GetMouseButtonDown(0) && Time.time > _nextFire)
        {
            _nextFire = Time.time + _fireRate;
            PlaySound();
        }
       
    }

    //Blast Cannon behaviour
    private void BlastCannonWep()
    {
        _fireRate = 0.1f;

        if (Input.GetMouseButtonDown(0) && Time.time > _nextFire)
        {
            _nextFire = Time.time + _fireRate;
            PlaySound();
        }
    }

    //Shotgun behaviour
    private void ShotgunWep()
    {
        _fireRate = 1.1f;

        if (Input.GetMouseButtonDown(0) && Time.time > _nextFire)
        {
            _nextFire = Time.time + _fireRate;
            PlaySound();
        }
    }

    void PlaySound()
    {
        int n = Random.Range(0, 3);
        
        //if 1 2 or 3, player certain sound
        if (hCannonFire)
        {
            hyperCannon[n].Play();
        }

        if (bCannonFire)
        {
            blastCannon[n].Play();
        }

        if (shotGunFire)
        {
            shotgun[n].Play();
        }
    }

    IEnumerator WeaponTransitionAction(int c)
    {
        yield return new WaitForSeconds(0.5f);

        if (c == 1)
        {
            hCannonFire = true;
        }
        if (c == 2)
        {
            bCannonFire = true;
        }
        if (c == 3)
        {
            shotGunFire = true;
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
