using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Manager_Spawner : MonoBehaviour
{
    
    
    [SerializeField]
    float timeBetweenSpawns;
    [SerializeField]
    float distanceToPlayer;
    [SerializeField]
    GameObject enemy;
    [SerializeField]
    GameObject sniperEnemy;
    [SerializeField]
    Vector2 spawnBoundaries;
    [SerializeField] AudioClip spawnSound;

    [SerializeField]
    bool canSpawn;

    GameObject player;
    float timeBetweenSpawnsIncrement = 2.0f;
    float maxTimeBetweenSpawns = 30.0f;

    float nextSpawn;
    int currentWave;
    int numberOfEnemiesToSpawn;
    int numberOfSnipersToSpawn = 1;
    int waveLevel = 1;
    int enemyLevel = 1;

    void Start()
    {
        player = GetComponent<Game_Manager_Leveling>().player;
        nextSpawn = 0;
        numberOfEnemiesToSpawn = waveLevel;
    }

    
    void Update()
    {
        if(Time.time > nextSpawn && canSpawn)
        {
            SpawnEnemies(enemy, numberOfEnemiesToSpawn);
            currentWave++;
            nextSpawn = Time.time + timeBetweenSpawns;           
            if(currentWave % 3 == 1)
            {              
                IncreaseWaveDifficulty();
            }
            if(currentWave % 6 == 0)
            {
                SpawnEnemies(sniperEnemy, numberOfSnipersToSpawn);
            }

           
        }
    }

    void SpawnEnemies(GameObject enemy, int n)
    {
        float randomOffset = Random.Range(0.0f, 360.0f);
        for(int i = 0; i < n; i++)
        {
            Vector2 pos = player.transform.position;
            pos = CalculateSpawnPos(n, i, randomOffset);

            
            GameObject go = Instantiate(enemy, pos, Quaternion.identity);

            Audio_Manager.instance.PlaySoundFXClip(spawnSound, go.transform, 1.0f);

        }
        
       
    }
    Vector2 CalculateSpawnPos(int n, int i, float randomOffset)
    {
        Vector2 pos = player.transform.position;
        float rad = (2 * Mathf.PI / n * i) + (randomOffset * Mathf.Deg2Rad);
        pos.x += distanceToPlayer * Mathf.Cos(rad);
        pos.y += distanceToPlayer * Mathf.Sin(rad);
        if (pos.x > spawnBoundaries.x || pos.x < -spawnBoundaries.x || pos.y > spawnBoundaries.y || pos.y < -spawnBoundaries.y)
        {
            randomOffset = Random.Range(0.0f, 360.0f);
            pos = CalculateSpawnPos(n, i, randomOffset);
        }
        return pos;
    }
    void IncreaseWaveDifficulty()
    {      
        numberOfEnemiesToSpawn++;
        enemyLevel++;
        if (timeBetweenSpawns < maxTimeBetweenSpawns) timeBetweenSpawns += timeBetweenSpawnsIncrement;
    }

    public int GetEnemyLevel()
    {
        return enemyLevel;
    }
}
