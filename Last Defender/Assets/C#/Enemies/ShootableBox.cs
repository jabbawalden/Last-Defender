using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootableBox : MonoBehaviour {

    public StrongDemon strongDemon;
    
    private void Start()
    {
        strongDemon = transform.parent.GetComponent<StrongDemon>();
    }

}
