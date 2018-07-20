using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitPlayer : MonoBehaviour {

    public StrongDemon strongDemon;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //do damage
            //call event
        }
    }

    private void OnTriggerExit(Collider other)
    {
        
    }
}
