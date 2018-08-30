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
    public int gm_PowerCores;
    public List<SerializableVector3> checkPoints = new List<SerializableVector3>();

    //data.data_CurrentHealth = _characterMotor.health;
    public float gm_health;
    //data.data_MaxHealth = _characterMotor.maxHealth;
    public float gm_maxHealth;
    //data.data_MaxLightPower = _characterMotor.maxLightPower;
    public float gm_maxLightPower;
    //data.data_PlayerSpeed = _characterMotor.speed;
    public float gm_speed;
    //data.data_PlayerJump = _characterMotor.jump;
    public float gm_jump;
    //data.data_BCannonAmmo = _pShoot.bAmmo;
    public int gm_bAmmo;
    //data.data_MCannonAmmo = _pShoot.mAmmo;
    public int gm_mAmmo;
    //data.data_HBlasterAmmo = _pShoot.hAmmo;
    public int gm_hAmmo;
    //data.data_PlayerArmour = _characterMotor.armour;
    public float gm_Armor;
    //data.data_bCannonPickUp = _pShoot.blastCannonPickUp;
    public bool gm_blastCannonPickUp;
    //data.data_mCannonPickUp = _pShoot.miniCannonPickUp;
    public bool gm_miniCannonPickUp;
    //data.data_hBlasterPickUp = _pShoot.hyperBlasterPickUp;
    public bool gm_hyperBlasterPickUp;

    public Vector3 startPos;
    public Vector3 newCheckPointTest;

    public float armorCheck;

    private CharacterMotor _characterMotor;
    private PShoot _pShoot;
    
    private void Start()
    {
        _characterMotor = GameObject.Find("PlayerMain").GetComponent<CharacterMotor>();
        _pShoot = GameObject.Find("PlayerMain").GetComponent<PShoot>();
        gameState = GameState.Play;
        
    }


    private void OnEnable()
    {
        GameEvents.EventPlayerDead += StateDead;
        GameEvents.EventSaveGameData += SaveData;
        //GameEvents.EventSaveGameData += SendData;
        GameEvents.EventLoadLastSave += LoadData;
        GameEvents.EventLoadLastSave += StartPosAssign;
    }

    private void OnDisable()
    {
        GameEvents.EventPlayerDead -= StateDead;
        GameEvents.EventSaveGameData -= SaveData;
        //GameEvents.EventSaveGameData -= SendData;
        GameEvents.EventLoadLastSave -= LoadData;
        GameEvents.EventLoadLastSave -= StartPosAssign;
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
        }

        Debug.Log("DataDeleted");
    }

    public void StartPosAssign()
    {
        startPos = new Vector3 (checkPoints[checkPoints.Count - 1].x, checkPoints[checkPoints.Count - 1].y, checkPoints[checkPoints.Count - 1].z);
    }
    
    public void SaveData()
    {
        _characterMotor.SendData();
        _pShoot.SendData();

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/LastDefenderSavedFile.dat");
        DataContainer data = new DataContainer();

        Debug.Log("DataSaved");

        data.data_Armor = gm_Armor; //gm_Armor;
        
        data.data_DeadEnemies = deadEnemies;
        data.data_UsedAmmo = usedAmmo;
        data.data_UsedPowerCore = usedPowerCore;
        data.data_UsedHealthPack = usedHealthPack;
        data.data_UsedRepairKits = usedRepairKits;
        data.data_TriggeredEnemyGroups = triggeredEnemyGroups;
        data.data_checkPoints = checkPoints;
        data.data_gm_PowerCores = gm_PowerCores;
        
        //data.data_CurrentHealth = _characterMotor.health;
        data.data_health = gm_health;
        //data.data_MaxHealth = _characterMotor.maxHealth;
        data.data_MaxHealth = gm_maxHealth;
        //data.data_MaxLightPower = _characterMotor.maxLightPower;
        data.data_MaxLightPower = gm_maxLightPower;
        //data.data_PlayerSpeed = _characterMotor.speed;
        data.data_PlayerSpeed = gm_speed;
        //data.data_PlayerJump = _characterMotor.jump;
        data.data_PlayerJump = gm_jump;

    
        
        //data.data_BCannonAmmo = _pShoot.bAmmo;
        data.data_bAmmo = gm_bAmmo;
        //data.data_MCannonAmmo = _pShoot.mAmmo;
        data.data_mAmmo = gm_mAmmo;
        //data.data_HBlasterAmmo = _pShoot.hAmmo;
        data.data_hAmmo = gm_hAmmo;
      
        //data.data_bCannonPickUp = _pShoot.blastCannonPickUp;
        data.data_blastCannonPickUp = gm_blastCannonPickUp;
        //data.data_mCannonPickUp = _pShoot.miniCannonPickUp;
        data.data_miniCannonPickUp = gm_miniCannonPickUp;
        //data.data_hBlasterPickUp = _pShoot.hyperBlasterPickUp;
        data.data_hyperBlasterPickUp = gm_hyperBlasterPickUp;
        

        bf.Serialize(file, data);
        file.Close();
    }

    void SendData()
    {
        //_characterMotor.SendData();
        //_pShoot.SendData();
    }

    //LoadFunction
    public void LoadData()
    {
        if (File.Exists(Application.persistentDataPath + "/LastDefenderSavedFile.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.OpenRead(Application.persistentDataPath + "/LastDefenderSavedFile.dat");
            DataContainer data = (DataContainer)bf.Deserialize(file);

            Debug.Log("DataLoaded");

            gm_Armor = data.data_Armor;

            
            deadEnemies = data.data_DeadEnemies;
            usedAmmo = data.data_UsedAmmo;
            usedPowerCore = data.data_UsedPowerCore;
            usedHealthPack = data.data_UsedHealthPack;
            usedRepairKits = data.data_UsedRepairKits;
            triggeredEnemyGroups = data.data_TriggeredEnemyGroups;
            gm_PowerCores = data.data_gm_PowerCores;
            
            //data.data_CurrentHealth = _characterMotor.health;
            gm_health = data.data_health;
            //data.data_MaxHealth = _characterMotor.maxHealth;
            gm_maxHealth = data.data_MaxHealth;
            //data.data_MaxLightPower = _characterMotor.maxLightPower;
            gm_maxLightPower = data.data_MaxLightPower;
            //data.data_PlayerSpeed = _characterMotor.speed;
            gm_speed = data.data_PlayerSpeed;
            //data.data_PlayerJump = _characterMotor.jump;
            gm_jump = data.data_PlayerJump;
            //_characterMotor.armour = data.data_PlayerArmour;
            

            
            checkPoints = data.data_checkPoints;
            //_characterMotor.health = data.data_CurrentHealth;
            //_characterMotor.maxHealth = data.data_MaxHealth;
            //_characterMotor.maxLightPower = data.data_MaxLightPower;
            //_characterMotor.speed = data.data_PlayerSpeed;
            //_characterMotor.jump = data.data_PlayerJump;
            //_pShoot.bAmmo = data.data_BCannonAmmo;
            gm_bAmmo = data.data_bAmmo;
            //_pShoot.mAmmo = data.data_MCannonAmmo;
            gm_mAmmo = data.data_mAmmo;
            //_pShoot.hAmmo = data.data_HBlasterAmmo;
            gm_hAmmo = data.data_hAmmo;

            //_pShoot.blastCannonPickUp = data.data_bCannonPickUp;
            gm_blastCannonPickUp = data.data_blastCannonPickUp;
            //_pShoot.miniCannonPickUp = data.data_mCannonPickUp;
            gm_miniCannonPickUp = data.data_miniCannonPickUp;
            //_pShoot.hyperBlasterPickUp = data.data_hBlasterPickUp;
            gm_hyperBlasterPickUp = data.data_hyperBlasterPickUp;


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
    public float data_health;
    public float data_MaxHealth; 
    public float data_MaxLightPower;
    public float data_PlayerSpeed;
    public float data_PlayerJump;
    public int data_bAmmo;
    public int data_mAmmo;
    public int data_hAmmo;
    public float data_Armor;
    public bool data_blastCannonPickUp, data_miniCannonPickUp, data_hyperBlasterPickUp; 
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


