using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Stats : MonoBehaviour
{
    [Header("Movement")]
    public float maxSpeed = 4.0f;
    public float acceleration = 1.0f;
    public float turnSpeed = 15f;
    public float turretRotationSpeed = 100.0f;

    [Header("Combat")]
    public float health;
    public float maxHealth;
    public float shield;
    public float maxShield;
    public float reloadTime = 0.5f;
   
    public GameObject playerBullet;
    public Transform shootingPoint;
    public Transform turret;

    [Header("Bullet")]
    public float bulletForce;
    public float bulletLifespan;
    public float bulletBaseDamage;

    [Header("Leveling")]
    public int currentLevel = 1;
    public float currentXP = 0;
    public float XPToLevelUp;

    

    void Awake()
    {
        
        health = maxHealth;
        shield = maxShield;
    }

    void Update()
    {
        if (health > maxHealth) health = maxHealth;
        if (shield > maxShield) shield = maxShield;
    }



    public void TakeDamage(float baseDamage, float angleImpactMult)
    {
        float rawDamageToHealth = baseDamage * angleImpactMult - 0.1f * shield;
        float rawDamageToShield = 2* baseDamage * angleImpactMult;

        if (rawDamageToHealth > 0 && angleImpactMult >= 0.5f)
        {
            health -= rawDamageToHealth;
        }

        if (angleImpactMult > 0.1f)
        {
            shield -= rawDamageToShield;
        }

    }

}
