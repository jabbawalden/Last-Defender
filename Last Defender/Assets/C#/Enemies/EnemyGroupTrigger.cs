using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGroupTrigger : MonoBehaviour {

    [System.Serializable]
    public enum TriggerType {AttackTrue, SetActive}
    public TriggerType triggerType;

    public GameObject[] strongEnemies, fastEnemies, rangeEnemies;
    private AudioSource _audioSource;
    public bool playScream;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();

        if (triggerType == TriggerType.SetActive)
        {
            foreach (GameObject i in strongEnemies)
            {
                i.SetActive(false);
            }

            foreach (GameObject i in fastEnemies)
            {
                i.SetActive(false);
            }

            foreach (GameObject i in rangeEnemies)
            {
                i.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (triggerType == TriggerType.AttackTrue)
            {
                if (playScream)
                {
                    _audioSource.Play();
                    playScream = false;
                }

                foreach (GameObject i in strongEnemies)
                {
                    i.GetComponent<StrongDemon>().attackPlayer = true;
                }

                foreach (GameObject i in fastEnemies)
                {
                    i.GetComponent<FastDemon>().attackPlayer = true;
                }

                foreach (GameObject i in rangeEnemies)
                {
                    i.GetComponent<RangeDemon>().attackPlayer = true;
                }
            } 

            if (triggerType == TriggerType.SetActive)
            {
                if (playScream)
                {
                    _audioSource.Play();
                    playScream = false;
                }

                foreach (GameObject i in strongEnemies)
                {
                    i.SetActive(true);
                    i.GetComponent<StrongDemon>().attackPlayer = true;
                }

                foreach (GameObject i in fastEnemies)
                {
                    i.SetActive(true);
                    i.GetComponent<FastDemon>().attackPlayer = true;
                }

                foreach (GameObject i in rangeEnemies)
                {
                    i.SetActive(true);
                    i.GetComponent<RangeDemon>().attackPlayer = true;
                }
            }
       
        }
       
    }

	
}
