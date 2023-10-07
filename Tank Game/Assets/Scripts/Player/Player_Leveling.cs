using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player_Leveling : MonoBehaviour
{
     
    Player_Stats playerStats;
    Player_Animations playerAimations;
    float previousXPToLevelUp;

    bool IsLeveling = false;

    void Start()
    {
        
        playerStats = GetComponent<Player_Stats>();
        playerAimations = GetComponent<Player_Animations>();
       
        playerStats.XPToLevelUp = playerStats.currentLevel;
        playerStats.XPInThisLevelTotal = playerStats.currentLevel;
    }

    
    void Update()
    {
       
        if (playerStats.currentXP >= playerStats.XPToLevelUp && !IsLeveling)
        {
            IsLeveling = true;
            playerAimations.LevelUpAnimation();
        }
        playerStats.XPInThisLevelCurrent = playerStats.currentXP - previousXPToLevelUp;
    }

    public void LevelUp()
    {
        previousXPToLevelUp = playerStats.XPToLevelUp;

        playerStats.currentLevel++;
        playerStats.XPToLevelUp += playerStats.currentLevel / 2;

        playerStats.XPInThisLevelTotal = playerStats.XPToLevelUp - previousXPToLevelUp;
       
        
        playerStats.gameManager.GetComponent<Game_Manager_Leveling>().PlayerLevelUp();
        IsLeveling = false;
    }
}
