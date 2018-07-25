using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairKit : MonoBehaviour {

    public GameObject _playerUpgrades;
    private PShoot _characterShoot;
    private CharacterLook _characterLook;

	// Use this for initialization
	void Start ()
    {
        _characterShoot = GameObject.Find("PlayerMain").GetComponent<PShoot>();
        _characterLook = GameObject.Find("Camera").GetComponent<CharacterLook>();
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _playerUpgrades.SetActive(true);
            _characterLook.canLook = false;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Destroy(gameObject);
            _characterShoot.canFire = false;
        }
            
    }   

}
