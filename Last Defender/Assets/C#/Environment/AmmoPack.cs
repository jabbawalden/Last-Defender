using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPack : MonoBehaviour {

    private GameManager _gameManager;
    public string ammoID = "Undefined";

    private PShoot _pShoot;
    [SerializeField] private float bAmmount, mAmount, hAmount;
    [System.Serializable]
    public enum AmmoType {blastCannon, miniCannon, hyperCannon};
    public AmmoType ammoType;
    
	// Use this for initialization
	void Start ()
    {
        _pShoot = GameObject.Find("PlayerMain").GetComponent<PShoot>();

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
            Destroy(gameObject);
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
