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
        if (other.tag == "Player")
        {
            _demonController.playerInRange = true;
        }
    }
}
