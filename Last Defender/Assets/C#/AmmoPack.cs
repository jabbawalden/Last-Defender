using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPack : MonoBehaviour {

    private PShoot _pShoot;
    [SerializeField] private float bAmmount, mAmount, hAmount;
    [System.Serializable]

    public enum AmmoType {blastCannon, miniCannon, hyperCannon};

    public AmmoType ammoType;
    
	// Use this for initialization
	void Start ()
    {
        _pShoot = GameObject.Find("_PlayerMove").GetComponent<PShoot>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
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
            Destroy(this.gameObject, 0.15f);
        }
    }
}
