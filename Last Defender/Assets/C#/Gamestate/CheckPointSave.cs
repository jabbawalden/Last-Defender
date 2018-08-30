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

        if (_gameManager.gm_checkPoints.Contains(position))
        {
            Destroy(gameObject);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            AddToList();
            GameEvents.ReportSaveData();
            _gameManager.newCheckPointTest = gameObject.transform.position;
            StartCoroutine(DestroyCheckPoint());
        }
    }

    public void AddToList()
    {
        _gameManager.gm_checkPoints.Add(position);
    }

    IEnumerator DestroyCheckPoint()
    {
        yield return new WaitForSeconds(2f);
        Destroy(this.gameObject);
    }
}
