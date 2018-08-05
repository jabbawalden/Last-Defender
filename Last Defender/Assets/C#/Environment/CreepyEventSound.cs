using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreepyEventSound : MonoBehaviour {

    public AudioSource audioSource;
    public bool finished;
    public GameObject light1, light2;

    private void Start()
    {
        finished = false;
    }

    private void OnEnable()
    {
        GameEvents.EventCreepyMomentOne += PlayAudio;
    }

    private void OnDisable()
    {
        GameEvents.EventCreepyMomentOne -= PlayAudio;
    }

    public void PlayAudio()
    {
        audioSource.Play();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && finished == false)
        {
            GameEvents.CreepyMomentOne();
            finished = true;
            light1.SetActive(false);
            light2.SetActive(false);
        }
    }

}
