using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Stats : MonoBehaviour
{
    [Header("Movement")]
    public float maxSpeed;
    public float acceleration;
    public float turretRotationSpeed;
    public float turnSpeed;
    public Transform turret;
    public Rigidbody2D rb;

    [Header("Combat")]
    public float maxHealth;
    public float health;
    public float maxShield;
    public float shield;
    public float reloadTime;
    public GameObject enemyBullet;
    public GameObject XPOrb;
    public Transform shootingPoint;

    [Header("AI")]
    public float detectionRange;
    public float minDistanceToPlayer;
    public float maxDistanceToPlayer;




    float minImpactAngleHealth = 0.5f;
    float minImpactAngleShield = 0.5f;
    void Start()
    {
       health = maxHealth;
       shield = maxShield;
    }

    void Update()
    {
       
    }

   

    public void TakeDamage(float baseDamage, float angleImpactMult)
    {
        float rawDamageToHealth = baseDamage *  angleImpactMult - 0.1f * shield;
        float rawDamageToShield = 2* baseDamage *  angleImpactMult;

        if(rawDamageToHealth > 0 && angleImpactMult >= 0.5f)
        {
            health -= rawDamageToHealth;
        }

        if(angleImpactMult > 0.1f)
        {
            shield -= rawDamageToShield;
        }
        
    }           
}
