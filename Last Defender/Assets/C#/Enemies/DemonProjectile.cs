using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonProjectile : MonoBehaviour {

    private CharacterMotor _charMotor;
    public float projectileDamage;
    public GameObject explosion;

	// Use this for initialization
	void Start ()
    {
        _charMotor = GameObject.Find("PlayerMain").GetComponent<CharacterMotor>();
        StartCoroutine(DestroyProjectile());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator DestroyProjectile()
    {
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            
            GameEvents.PlayerEventHit();
            Instantiate(explosion, transform.position, Quaternion.identity);
            _charMotor.health -= projectileDamage - _charMotor.armor;
            Destroy(gameObject);
        }
        else if (other.CompareTag("Environment"))
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

}
