using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S8ETrigger : MonoBehaviour {

    public DoorActivate doorActive;
    public GameObject e1, e2, e3;
    private AudioSource _audioSource;
    [SerializeField] private AudioClip _audioClip;
    public bool hasActivated;

	// Use this for initialization
	void Start ()
    {
        hasActivated = false;
        _audioSource = GetComponent<AudioSource>();
        e1.SetActive(false);
        e2.SetActive(false);
        e3.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && doorActive.doorType == DoorActivate.DoorType.triggered && !hasActivated)
        {
            _audioSource.PlayOneShot(_audioClip);
            StartCoroutine(EventDelay());
            hasActivated = true;
        }
    }

    IEnumerator EventDelay()
    {
        yield return new WaitForSeconds(1);
        e1.SetActive(true);
        e2.SetActive(true);
        e3.SetActive(true);
        doorActive.OpenDoor();
    }
}
