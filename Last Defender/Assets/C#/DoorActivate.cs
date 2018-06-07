using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class DoorActivate : MonoBehaviour {

    [SerializeField]
    private Animator _animator;
    [SerializeField]
    private AudioSource _doorSFX;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            _animator.SetBool("DoorActive", true);
            _doorSFX.Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            _animator.SetBool("DoorActive", false);
            _doorSFX.Play();
        }
    }

}
