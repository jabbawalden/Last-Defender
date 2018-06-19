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

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.tag == "Player")
        {
            _charMotor.health--;
            Destroy(this.gameObject);
        } else if (other.collider.tag == "Environment")
        {
            Destroy(this.gameObject);
        }
        
    }
}
