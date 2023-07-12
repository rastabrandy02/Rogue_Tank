using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player_Leveling : MonoBehaviour
{
     
    Player_Stats playerStats;
    Game_Manager gameManager;
    
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<Game_Manager>();
        playerStats = GetComponent<Player_Stats>();
       
        playerStats.XPToLevelUp = playerStats.currentLevel;
    }

    
    void Update()
    {
        if(playerStats.currentXP >= playerStats.XPToLevelUp)
        {
            LevelUp();
        }
        
    }

    public void LevelUp()
    {
        playerStats.currentLevel++;
        playerStats.XPToLevelUp += playerStats.currentLevel;
        gameManager.PlayerLevelUp(UpgradeType.MAX_SPEED);
    }
}
