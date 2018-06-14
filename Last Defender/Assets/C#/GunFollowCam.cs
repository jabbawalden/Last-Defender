using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunFollowCam : MonoBehaviour {

    public Camera camPos;

	// Use this for initialization
	void Start ()
    {
        transform.position = camPos.transform.position;
        transform.rotation = camPos.transform.rotation;
    }
	
	// Update is called once per frame
	void Update ()
    {
        transform.position = camPos.transform.position;
        transform.rotation = camPos.transform.rotation;
    }
}
