using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonProjectile : MonoBehaviour {

    private CharacterMotor _charMotor;

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
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            _charMotor.health--;
            Destroy(this.gameObject);
        }
        else if (other.tag == "Environment")
        {
            Destroy(this.gameObject);
        }
    }

}
