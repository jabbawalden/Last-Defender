using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FieldOfView : MonoBehaviour
{
    //work in progress script
    public float fieldOfViewAngle = 110f;
    public bool playerInSight;
    public Vector3 personLastSighting;

    private NavMeshAgent nav;
    private SphereCollider col;
    private CharacterMotor _player;
    private Vector3 previousSighting;

    private void Awake()
    {
        nav = GetComponent<NavMeshAgent>();
        col = GetComponent<SphereCollider>();
        _player = GameObject.Find("_PlayerMover").GetComponent<CharacterMotor>();
    }

    private void Update()
    {
        
    }
}
