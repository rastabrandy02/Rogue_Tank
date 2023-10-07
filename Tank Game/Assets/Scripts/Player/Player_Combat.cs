using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Combat : MonoBehaviour
{
    Player_Stats playerStats;
    Player_Animations playerAnimations;
 
    float nextShot;

    Game_Manager_UI gameManagerUI;

    void Start()
    {        
        nextShot = Time.time;
        playerStats = GetComponent<Player_Stats>();
        playerAnimations = GetComponent<Player_Animations>();
        gameManagerUI = playerStats.gameManager.GetComponent<Game_Manager_UI>();

    }

    
    void Update()
    {
        if((Input.GetMouseButtonDown(0) && Time.time > nextShot) && !gameManagerUI.isGamePaused)
        {
            Shoot();
            Reload();
            
        }
        if(playerStats.health <= 0 && playerStats.isAlive)
        {
            playerStats.isAlive = false;
            Die();
        }
    }
    void Shoot()
    {
        Instantiate(playerStats.playerBullet, playerStats.shootingPoint.position, playerStats.turret.rotation);
        Audio_Manager.instance.PlaySoundFXClip(playerStats.shootingSFX, transform, 0.5f);
    }
    void Reload()
    {
        nextShot = Time.time + playerStats.reloadTime;
    }
    void Die()
    {
        playerAnimations.DeathVFX();
        
    }
    
}
