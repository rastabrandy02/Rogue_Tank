using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Game_Manager : MonoBehaviour
{
    
    public event EventHandler OnPlayerLevelUp;
    GameObject player;
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlayerLevelUp(UpgradeType type)
    {
        LevelUpUpgrade(type);
    }
    void LevelUpUpgrade(UpgradeType type)
    {
        Upgrade upgrade;

        switch(type)
        {
            case UpgradeType.MAX_SPEED:
                {
                    upgrade = new Upgrade_MaxSpeed();
                    upgrade.IncreaseStat(ref player.GetComponent<Player_Stats>().maxSpeed);
                    
                }
                break;
            default: break;
        }
        
    }
}
