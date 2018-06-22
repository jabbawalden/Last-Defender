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

    private void OnTriggerStay(Collider other)
    {
        if (!level.activeInHierarchy)
            level.SetActive(true);

        
    } 

    private void OnTriggerExit(Collider other)
    {
        level.SetActive(false);
    }
}
