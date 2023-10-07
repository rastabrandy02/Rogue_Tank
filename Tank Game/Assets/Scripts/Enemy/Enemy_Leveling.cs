using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Leveling : MonoBehaviour
{
    [SerializeField] float healthIncreasePercentage;
    [SerializeField] float shieldIncreasePercentage;
    [SerializeField] float damageIncreasePercentage;

    Enemy_Stats enemyStats;
    Game_Manager_Spawner gmSpawner;
    
    void Start()
    {
        enemyStats = GetComponent<Enemy_Stats>();
        gmSpawner = GameObject.FindWithTag("Game Manager").GetComponent<Game_Manager_Spawner>();
        int levelsToLevelUp = gmSpawner.GetEnemyLevel();
        for (int i = 0; i < levelsToLevelUp - 1; i++)
        {
            LevelUp();
        }
        enemyStats.health = enemyStats.maxHealth;
        enemyStats.shield = enemyStats.maxShield;
    }

    
    void Update()
    {
        
    }

    public void LevelUp()
    {
        enemyStats.level++;
        enemyStats.maxHealth += enemyStats.maxHealth * healthIncreasePercentage * 0.01f;
        enemyStats.maxShield += enemyStats.maxShield * shieldIncreasePercentage * 0.01f;
        enemyStats.baseDamage += enemyStats.baseDamage * damageIncreasePercentage * 0.01f;
        enemyStats.enemyBullet.GetComponent<Enemy_Bullet>().SetDamage(enemyStats.baseDamage);
    }
}
