using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    [SerializeField] private AudioClip _powerCore;
    [SerializeField] private AudioClip _healPack;
    [SerializeField] private AudioClip _upgrade;

    private AudioSource _audioSource;
    // Use this for initialization
    void Start ()
    {
        _audioSource = GetComponent<AudioSource>();
	}

    private void OnEnable()
    {
        GameEvents.EventPlayerHeal += PlayHealPack;
        GameEvents.EventPowerCore += PlayPowerCore;
        GameEvents.EventAmmoRefill += PlayAmmoRefill;
    }

    private void OnDisable()
    {
        GameEvents.EventPlayerHeal -= PlayHealPack;
        GameEvents.EventPowerCore -= PlayPowerCore;
        GameEvents.EventAmmoRefill -= PlayAmmoRefill;
    }


    public void PlayPowerCore()
    {
        _audioSource.PlayOneShot(_powerCore);
    }

    public void PlayHealPack()
    {
        _audioSource.PlayOneShot(_healPack);
    }

    public void PlayAmmoRefill()
    {
        _audioSource.PlayOneShot(_upgrade);
    }
}
