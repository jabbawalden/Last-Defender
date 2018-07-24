using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{

    public GameObject level;

    private void Awake()
    {
        level.SetActive(false);
    }

    //if player is within trigger, all other areas will be setactive, otherwise not active
    private void OnTriggerEnter(Collider other)
    {
        
       
    }

    private void OnTriggerStay(Collider other)
    {
        if (!level.activeInHierarchy && other.CompareTag("Player"))
        {
            level.SetActive(true);
        }

        if (other.CompareTag("Enemy"))
        {
            other.transform.parent = level.transform;
        }
    } 

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            level.SetActive(false);
        }

        
        if (other.CompareTag("Enemy"))
        {
            other.transform.parent = null;
        }
        
    }
}
