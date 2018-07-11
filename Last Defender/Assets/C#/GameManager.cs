using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    Menu,
    Start,
    Pause,
    Dead,
    Win
        
}

//inherits from Singleton class
public class GameManager : Singleton<GameManager> {

    public GameState gameState;
    
    public List<string> deadEnemies = new List<string>();
    public List<string> usedAmmo = new List<string>();
    public List<string> usedPowerCore = new List<string>();
    public List<string> usedHealthPack = new List<string>();


}
