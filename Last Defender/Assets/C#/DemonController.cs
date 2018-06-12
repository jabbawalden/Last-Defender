using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DemonController : MonoBehaviour {

    public NavMeshAgent agent;
    private GameObject _player;
    public float distance;

    [SerializeField] int type;
    //1 = speed, 2 = strong, 3 = range

	// Use this for initialization
	void Start ()
    {
        _player = GameObject.Find("_PlayerMove");
	}
	
	// Update is called once per frame
	void Update ()
    {
        EnemyFollow(type);
	}

    private void EnemyFollow(int c)
    {
        var targetposition = (transform.position - _player.transform.position).normalized * distance + _player.transform.position;
        //find direction, * distance with player position added.

        if (c == 1)
        {
            agent.SetDestination(_player.transform.position);
            agent.speed = 8;
        }

        if (c == 2)
        {
            agent.SetDestination(_player.transform.position);
            agent.speed = 4;
        }

        if (c == 3)
        {
            agent.SetDestination(targetposition);
            agent.speed = 5;
        }
    }
}
