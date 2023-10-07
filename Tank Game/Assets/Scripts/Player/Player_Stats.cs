using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Stats : MonoBehaviour
{
    [Header("Movement")]
    public float maxSpeed = 10.0f;
    public float acceleration = 7.0f;
    public float turnSpeed = 25f;
    public float turretRotationSpeed = 30.0f;

    [Header("Combat")]
    public float health = 500;
    public float maxHealth = 500;
    public float shield = 500;
    public float maxShield = 500;
    public float reloadTime = 1f;  
    public bool isAlive = true;
   
    public GameObject playerBullet;
    public Transform shootingPoint;
    public Transform turret;

    public bool hasToReshield = false;
    float timeToStartReshield = 5.0f;
    float timeSinceLastHit = 0f;
    float reshieldDuration = 1.0f;
    float reshieldPercentage = 0.05f;

    
    Coroutine reshieldRoutine;

    [Header("Bullet")]
    public float bulletForce = 2;
    public float bulletRange = 3;
    public float bulletBaseDamage = 100;

    [Header("Leveling")]
    public int currentLevel = 1;
    public float currentXP = 0;  
    public float XPToLevelUp = 1;
    public float XPInThisLevelTotal = 1;
    public float XPInThisLevelCurrent = 0;

    [Header("UI")]
    public Damage_Pop_Up_Generator damagePopUpGenerator;

    Color healthPopUpColor = Color.red;
    Color shieldPopUpColor = Color.cyan;

    Player_Animations playerAnimations;

    [Header("SoundFX")]
    public AudioSource audioSource;
    public AudioClip shieldDamageClip;
    public AudioClip healthDamageClip;
    public AudioClip shootingSFX;
    public AudioClip engineMovSFX;

    [Header("Managers")]
    public GameObject gameManager;
    public GameObject sceneManager;
    

    void Awake()
    {
        gameManager = GameObject.FindGameObjectWithTag("Game Manager");
        sceneManager = GameObject.FindGameObjectWithTag("Scene Manager");

        health = maxHealth;
        shield = maxShield;

        playerAnimations = GetComponent<Player_Animations>();
        
        
    }
    

    void Update()
    {
        if (health > maxHealth) health = maxHealth;
        if (shield > maxShield) shield = maxShield;

        if (shield < 0) shield = 0;

        if (timeSinceLastHit >= timeToStartReshield && shield < maxShield && !hasToReshield)
        {
            hasToReshield = true;
            reshieldRoutine = StartCoroutine(Reshield());
            playerAnimations.StartReshield();
        }
        
        timeSinceLastHit += Time.deltaTime;

       
    }



    public void TakeDamage(float baseDamage, float angleImpactMult)
    {
        timeSinceLastHit = 0.0f;
        if (reshieldRoutine != null) StopCoroutine(reshieldRoutine);
        playerAnimations.StopReshield();
        hasToReshield = false;

        float rawDamageToHealth = (baseDamage * angleImpactMult) + angleImpactMult * 50 - Mathf.Abs( 0.2f * shield);
        float rawDamageToShield = baseDamage;

        if (rawDamageToHealth > 0 && angleImpactMult >= 0.25f)
        {
            health -= rawDamageToHealth;

        }
        else rawDamageToHealth = 0;

        if (angleImpactMult > 0.1f)
        {
            shield -= rawDamageToShield;
        }
        else rawDamageToShield = 0;

        if (angleImpactMult >= 0.75f)
        {
            playerAnimations.Flash(healthPopUpColor);
            Audio_Manager.instance.PlaySoundFXClip(healthDamageClip, transform, 0.75f);
        }
        else
        {
            playerAnimations.Flash(shieldPopUpColor);
            Audio_Manager.instance.PlaySoundFXClip(shieldDamageClip, transform, 0.75f);
        } 

        damagePopUpGenerator.CreatePopUp(transform.position + new Vector3(-0.5f,0.0f,0.0f), rawDamageToHealth, healthPopUpColor);
        damagePopUpGenerator.CreatePopUp(transform.position + new Vector3(0.5f, 0.0f, 0.0f), rawDamageToShield, shieldPopUpColor);

    }
    IEnumerator Reshield()
    {
        while(shield < maxShield)
        {
            shield += maxShield * reshieldPercentage;           
            yield return new WaitForSeconds(reshieldDuration);
        }
        hasToReshield = false;
        playerAnimations.StopReshield();
        
    }
    public void ReplenishHealthLevelUp(float oldMaxHP)
    {      
        float hpRate = health / oldMaxHP;
        health = hpRate * maxHealth;
    }
    public void ReplenishShieldLevelUp(float oldMaxShield)
    {
        float shieldRate = shield / oldMaxShield;
        shield = shieldRate * maxShield;
    }

}
