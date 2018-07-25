using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGroupTrigger : MonoBehaviour {

    public GameObject[] strongEnemies, fastEnemies, rangeEnemies;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
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
       
    }

	
}
