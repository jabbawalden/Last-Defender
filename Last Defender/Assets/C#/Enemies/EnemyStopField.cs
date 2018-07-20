using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStopField : MonoBehaviour {
    [System.Serializable]
    public enum EnemyType {StrongDemon, RangeDemon, FastDemon}
    public EnemyType enemyType;

    private StrongDemon strongDemon;

	// Use this for initialization
	void Start ()
    {
        if (enemyType == EnemyType.StrongDemon)
        {
            strongDemon = transform.parent.GetChild(2).GetComponent<StrongDemon>();
        }
    }

    private void Update()
    {
        transform.position = strongDemon.transform.position;
    }

}
