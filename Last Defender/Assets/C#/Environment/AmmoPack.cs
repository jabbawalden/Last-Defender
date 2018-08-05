using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPack : MonoBehaviour {

    private GameManager _gameManager;
    private UIManager _uIManager;
    public string ammoID = "Undefined";
    private PShoot _characterShoot;
    private CharacterLook _characterLook;
    private CharacterMotor _characterMotor;

    // Use this for initialization
    void Start ()
    {
        _uIManager = GameObject.Find("UI").GetComponent<UIManager>();
        _characterShoot = GameObject.Find("PlayerMain").GetComponent<PShoot>();
        _characterLook = GameObject.Find("Camera").GetComponent<CharacterLook>();
        _characterMotor = GameObject.Find("PlayerMain").GetComponent<CharacterMotor>();

        if (ammoID == "Undefined")
        {
            Debug.LogError("Ammo ID not generated");
        }

        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        if (_gameManager.usedAmmo.Contains(ammoID))
        {
            Destroy(gameObject);
            return;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {     
            AddID();
            _uIManager.uIammoRefill.SetActive(true);
            _characterMotor.canMove = false;
            _characterLook.canLook = false;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            _characterShoot.inAmmoMode = true;
            Destroy(gameObject);
            //turn on AmmoReload
            /*
            if (ammoType == AmmoType.blastCannon)
            {
                _pShoot.bAmmo += bAmmount;
                
            }
            else if (ammoType == AmmoType.hyperCannon)
            {
                _pShoot.hAmmo += hAmount;
            }
            else if (ammoType == AmmoType.miniCannon)
            {
                _pShoot.mAmmo += mAmount;
            }
            */
        }
    }

    void AddID ()
    {
        if (!_gameManager.usedAmmo.Contains(ammoID))
        {
            _gameManager.usedAmmo.Add(ammoID);
        }
    }
}
