using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonProjectileExplosion : MonoBehaviour {

    [SerializeField] private AudioClip[] _audioClip;
    private AudioSource _audioSource;

	// Use this for initialization
	void Start ()
    {
        int r = Random.Range(0, _audioClip.Length);
        _audioSource = GetComponent<AudioSource>();
        _audioSource.PlayOneShot(_audioClip[r]); 
        StartCoroutine(DestroyThisObject());
	}
	
    IEnumerator DestroyThisObject()
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }
}
