using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameEvents
{
    public static event Action EventEnemyHit = null;
    public static event Action EventSaveData = null;
    public static event Action EventDoorCheck = null;
    //save item lists
    //save ammo counts
    //save player position
    //checkpoint reached uses SaveData but only once - then use boolean and set to true, cannot be triggered again.
    //save bool state of each checkpoint
    public static event Action EventPauseGame = null;
    public static event Action EventUnpauseGame = null;
    public static event Action EventPlayerDead = null;
    public static event Action EventEnemyDeath = null;

    public static void ReportEnemyHit() 
    {
        if (EventEnemyHit != null)
            EventEnemyHit();

    }
    
    public static void ReportSavedData()
    {

    }

    public static void ReportPausedGame()
    {

    }

    public static void ReportUnpauseGame()
    {

    }

    public static void ReportPlayerDead()
    {
        if (EventPlayerDead != null)
            EventPlayerDead(); 
    }

    public static void DoorCheck()
    {
        if (EventDoorCheck != null)
            EventDoorCheck();
    }

    public static void EnemyDeath()
    {
        if (EventEnemyDeath != null)
            EventEnemyDeath();
    }
}
