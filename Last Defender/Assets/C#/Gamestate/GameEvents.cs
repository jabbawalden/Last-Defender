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
    public static event Action EventGameWon = null; 
    public static event Action EventUnpauseGame = null;
    public static event Action EventPlayerDead = null;
    public static event Action EventEnemyDeath = null;
    public static event Action EventPlayerHit = null;
    public static event Action EventItemAcquired = null;
    public static event Action EventPowerCore = null;
    public static event Action EventCreepyMomentOne = null;
    public static event Action EventAmmoRefill = null;
    public static event Action<DoorActivate>EventPlayerDoorCheck = null;
   
    public static void ReportEventPlayerDoorCheck(DoorActivate instance)
    {
        if (EventPlayerDoorCheck != null)
        {
            EventPlayerDoorCheck(instance);
        }
    }

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

    public static void ReportGameWon()
    {
        if (EventGameWon != null)
            EventGameWon();
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


    public static void PlayerEventHit()
    {
        if (EventPlayerHit != null)
            EventPlayerHit();
    }

    public static void ItemAcquired()
    {
        if (EventItemAcquired != null)
            EventItemAcquired();
    }

    public static void PowerCore()
    {
        if (EventPowerCore != null)
            EventPowerCore();
    }

    public static void CreepyMomentOne()
    {
        if (EventCreepyMomentOne != null)
            EventCreepyMomentOne();
    }

    public static void AmmoRefillPlayer()
    {
        if (EventAmmoRefill != null)
            EventAmmoRefill();
    }
}
