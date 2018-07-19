using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAgressionField : MonoBehaviour {

    [SerializeField] private DemonController _demonController;

    private void Update()
    {
        if (_demonController != null)
        {
            transform.position = _demonController.transform.position;
        }
        else
        {
            return;
        }
        
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _demonController.playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _demonController.playerInRange = false;
        }
    }
}
