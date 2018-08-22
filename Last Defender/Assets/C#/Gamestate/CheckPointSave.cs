using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CheckPointSave : MonoBehaviour {

    //public string checkPointID = "Undefined";
    private GameManager _gameManager;
    private SerializableVector3 position;

	// Use this for initialization
	void Start ()
    {
       position = transform.position;
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        
        if (_gameManager.checkPoints.Contains(position))
        {
            Destroy(this.gameObject);
        }
        
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            AddToList();
            GameEvents.ReportSaveData();
        }
    }

    public void AddToList()
    {
        _gameManager.checkPoints.Add(position);
    }
}
