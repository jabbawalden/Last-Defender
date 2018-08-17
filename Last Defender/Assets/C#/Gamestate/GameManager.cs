using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public enum GameState
{
    Menu,
    Play,
    Pause,
    Dead,
    Won,
    Save,
    Load
        
}

//inherits from Singleton class
public class GameManager : Singleton<GameManager> {

    public GameState gameState;
    
    public List<string> deadEnemies = new List<string>();
    public List<string> usedAmmo = new List<string>();
    public List<string> usedPowerCore = new List<string>();
    public List<string> usedHealthPack = new List<string>();
    public List<string> usedRepairKits = new List<string>();
    public List<string> triggeredEnemyGroups = new List<string>();
    public List<SerializableVector3> checkPoints = new List<SerializableVector3>();
    public int gm_PowerCores;
    public DataContainer data;
    public List<SerializableVector3> test = new List<SerializableVector3>();
    
    private CharacterMotor _characterMotor;
    private PShoot _pShoot;
    
    private void Start()
    {
        _characterMotor = GameObject.Find("PlayerMain").GetComponent<CharacterMotor>();
        _pShoot = GameObject.Find("PlayerMain").GetComponent<PShoot>();
        gameState = GameState.Play;
        
    }

    private void Update()
    {
        test = checkPoints;
    }

    private void OnEnable()
    {
        GameEvents.EventPlayerDead += StateDead;
        GameEvents.EventSaveGameData += SaveData;
        GameEvents.EventLoadLastSave += LoadData;
    }

    private void OnDisable()
    {
        GameEvents.EventPlayerDead -= StateDead;
        GameEvents.EventSaveGameData -= SaveData;
        GameEvents.EventLoadLastSave -= LoadData;
    }

    public void StateDead()
    {
        gameState = GameState.Dead;
    }

    public void DeleteData()
    {
        if (File.Exists(Application.persistentDataPath + "/LastDefenderSavedFile.dat"))
        {
            File.Delete(Application.persistentDataPath + "/LastDefenderSavedFile.dat");
            //delete1 = true;
            //g1exists = false;
        }

        Debug.Log("DataDeleted");
    }

    //SaveFunction
    public void SaveData()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/LastDefenderSavedFile.dat");
        DataContainer data = new DataContainer();

        Debug.Log("DataSaved");

        data.data_DeadEnemies = deadEnemies;
        data.data_UsedAmmo = usedAmmo;
        data.data_UsedPowerCore = usedPowerCore;
        data.data_UsedHealthPack = usedHealthPack;
        data.data_UsedRepairKits = usedRepairKits;
        data.data_TriggeredEnemyGroups = triggeredEnemyGroups;
        //data.data_checkPoints = checkPoints;
        //data.data_PlayerPosition = new SerializableVector3(checkPoints[checkPoints.Count - 1].x, checkPoints[checkPoints.Count - 1].y, checkPoints[checkPoints.Count - 1].z);
        data.data_gm_PowerCores = gm_PowerCores;
        data.data_CurrentHealth = _characterMotor.health;
        data.data_MaxHealth = _characterMotor.maxHealth;
        data.data_MaxLightPower = _characterMotor.maxLightPower;
        data.data_PlayerSpeed = _characterMotor.speed;
        data.data_PlayerJump = _characterMotor.jump;
        data.data_BCannonAmmo = _pShoot.bAmmo;
        data.data_MCannonAmmo = _pShoot.mAmmo;
        data.data_HBlasterAmmo = _pShoot.hAmmo;
        data.data_PlayerArmour = _characterMotor.armour;

        bf.Serialize(file, data);
        file.Close();
    }

    //LoadFunction
    public void LoadData()
    {
        if (File.Exists(Application.persistentDataPath + "/LastDefenderSavedFile.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(Application.persistentDataPath + "/LastDefenderSavedFile.dat");
            DataContainer data = new DataContainer();

            Debug.Log("DataLoaded");

            deadEnemies = data.data_DeadEnemies;
            usedAmmo = data.data_UsedAmmo;
            usedPowerCore = data.data_UsedPowerCore;
            usedHealthPack = data.data_UsedHealthPack;
            usedRepairKits = data.data_UsedRepairKits;
            triggeredEnemyGroups = data.data_TriggeredEnemyGroups;
            //checkPoints = data.data_checkPoints;
            //_characterMotor.transform.position = new Vector3(checkPoints[checkPoints.Count - 1].x, checkPoints[checkPoints.Count - 1].y, checkPoints[checkPoints.Count - 1].z);
            gm_PowerCores = data.data_gm_PowerCores;
            _characterMotor.health = data.data_CurrentHealth;
            _characterMotor.maxHealth = data.data_MaxHealth;
            _characterMotor.maxLightPower = data.data_MaxLightPower;
            _characterMotor.speed = data.data_PlayerSpeed;
            _characterMotor.jump = data.data_PlayerJump;
            _pShoot.bAmmo = data.data_BCannonAmmo;
            _pShoot.mAmmo = data.data_MCannonAmmo;
            _pShoot.hAmmo = data.data_HBlasterAmmo;
            _characterMotor.armour = data.data_PlayerArmour;

            file.Close();
        }
            
    }
}

[Serializable] 
public class DataContainer
{
    public List<string> data_DeadEnemies = new List<string>();
    public List<string> data_UsedAmmo = new List<string>();
    public List<string> data_UsedPowerCore = new List<string>();
    public List<string> data_UsedHealthPack = new List<string>();
    public List<string> data_UsedRepairKits = new List<string>();
    public List<string> data_TriggeredEnemyGroups = new List<string>();
    public List<SerializableVector3> data_checkPoints = new List<SerializableVector3>();
    public int data_gm_PowerCores;
    public float data_CurrentHealth;
    public float data_MaxHealth; 
    public float data_MaxLightPower;
    public float data_PlayerSpeed;
    public float data_PlayerJump;
    public int data_BCannonAmmo;
    public int data_MCannonAmmo;
    public int data_HBlasterAmmo;
    public float data_PlayerArmour;
}

[Serializable]
public struct SerializableVector3
{
    public float x;
    public float y;
    public float z;

    public SerializableVector3(float rX, float rY, float rZ)
    {
        x = rX;
        y = rY;
        z = rZ;
    }

    //converts back to Vector3
    public static implicit operator SerializableVector3(Vector3 rValue)
    {
        return new SerializableVector3(rValue.x, rValue.y, rValue.z);
    }
}

