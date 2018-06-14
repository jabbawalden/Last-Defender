using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonProjectile : MonoBehaviour {

    private CharacterMotor _charMotor;

	// Use this for initialization
	void Start ()
    {
        _charMotor = GameObject.Find("_PlayerMove").GetComponent<CharacterMotor>();
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Player")
        {
            _charMotor.health--;
            Destroy(this.gameObject);
        } else if (collision.collider.tag == "Environment")
        {
            Destroy(this.gameObject);
        }
        
    }
}
