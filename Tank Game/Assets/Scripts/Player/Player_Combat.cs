using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Combat : MonoBehaviour
{
    Player_Stats playerStats;
 
    float nextShot;

    void Start()
    {
        nextShot = Time.time;
        playerStats = GetComponent<Player_Stats>();
        
    }

    
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && Time.time > nextShot)
        {
            Shoot();
            Reload();
            
        }
        if(playerStats.health <= 0)
        {
            Die();
        }
    }
    void Shoot()
    {
        Instantiate(playerStats.playerBullet, playerStats.shootingPoint.position, playerStats.turret.rotation);
    }
    void Reload()
    {
        nextShot = Time.time + playerStats.reloadTime;
    }
    void Die()
    {
        Debug.Log("GAME OVER");
    }
    
}
