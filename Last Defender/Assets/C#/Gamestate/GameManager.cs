using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private void Start()
    {
        gameState = GameState.Play;
    }

    private void OnEnable()
    {
        GameEvents.EventPlayerDead += StateDead;
    }

    private void OnDisable()
    {
        GameEvents.EventPlayerDead -= StateDead;
    }

    public void StateDead()
    {
        gameState = GameState.Dead;
    }
    //player data
    //player position
    //player armour
    //player movement speed
    //player light power

}
