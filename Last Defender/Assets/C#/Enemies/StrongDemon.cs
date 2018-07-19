using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrongDemon : Enemy
{
    private void OnEnable()
    {
        //MAYBE an event for death
        
    }

    private void OnDisable()
    {
        
    }
    // Use this for initialization
    void Start ()
    {
        TriggerCollider.center = new Vector3(0.53f, 0.99f, 0.87f);
        TriggerCollider.size = new Vector3(1.87f, 1.99f, 2.31f);
    }

    protected override void MovementPattern()
    {
        Agent.SetDestination(Player.transform.position);
    }

    // Update is called once per frame
    void Update ()
    {
		if (Health <= 0)
        {
            OnKill();
            //can't call event here
        }
	}

    public void OnKill()
    {
        if (!gameManager.deadEnemies.Contains(enemyID))
        {
            gameManager.deadEnemies.Add(enemyID);
        }
    }
}
