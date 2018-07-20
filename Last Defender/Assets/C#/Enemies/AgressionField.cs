﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgressionField : MonoBehaviour {

    private StrongDemon strongDemon;

    private void Update()
    {
        strongDemon = transform.parent.GetChild(2).GetComponent<StrongDemon>();
        transform.position = strongDemon.transform.position;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
            strongDemon.AgressionFieldActive = true;
    }
     
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            strongDemon.AgressionFieldActive = false;
    }
    
}
