using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Stats : MonoBehaviour
{
    Enemy_Animations enemyAnimations;


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
    public float baseDamage = 100.0f;
    public float reloadTime;
    public int level = 1;
    public GameObject enemyBullet;
    public GameObject XPOrb;
    public bool isAlive = true;

    float timeToStartReshield = 5.0f;
    float timeSinceLastHit = 0f;
    float reshieldDuration = 1.0f;
    float reshieldPercentage = 0.05f;
    bool hasToReshield = false;

    Coroutine reshieldRoutine;

    [Header("AI")]
    public float detectionRange;
    public float minDistanceToPlayer;
    public float maxDistanceToPlayer;
 
    [Header("UI")]
    public Damage_Pop_Up_Generator damagePopUpGenerator;
    public GameObject arrowPointer;
    GameObject canvas;

    [Header("SoundFX")]
    public AudioSource audioSource;
    public AudioClip shieldDamageClip;
    public AudioClip healthDamageClip;
    public AudioClip shootingSFX;
    public AudioClip engineMovSFX;
    public AudioClip spawnSFX;

    Color healthPopUpColor = Color.red;
    Color shieldPopUpColor = Color.cyan;


    void Start()
    {
       health = maxHealth;
       shield = maxShield;

       enemyAnimations = GetComponent<Enemy_Animations>();
       
       enemyBullet.GetComponent<Enemy_Bullet>().SetDamage(baseDamage);

       canvas = GameObject.FindGameObjectWithTag("Canvas");
       arrowPointer = Instantiate(arrowPointer, canvas.transform);
       arrowPointer.GetComponent<Enemy_Pointer>().SetTarget(transform);
    }

    void Update()
    {
        if (health > maxHealth) health = maxHealth;
        if (shield > maxShield) shield = maxShield;

        if (health <= 0) Die();
        if (shield < 0) shield = 0;

        if (timeSinceLastHit >= timeToStartReshield && shield < maxShield && !hasToReshield)
        {
            hasToReshield = true;
            reshieldRoutine = StartCoroutine(Reshield());
            enemyAnimations.StartReshield();
        }

        timeSinceLastHit += Time.deltaTime;

       
    }

   

    public void TakeDamage(float baseDamage, float angleImpactMult)
    {
        timeSinceLastHit = 0.0f;
        if (reshieldRoutine != null) StopCoroutine(reshieldRoutine);
        enemyAnimations.StopReshield();
        hasToReshield = false;

        float rawDamageToHealth = (baseDamage * angleImpactMult) + angleImpactMult * 50 - Mathf.Abs(0.2f * shield);
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

        if(angleImpactMult >= 0.75f)
        {
            enemyAnimations.Flash(healthPopUpColor);
            Audio_Manager.instance.PlaySoundFXClip(healthDamageClip, transform, 0.75f);
        }
        else 
        {
            enemyAnimations.Flash(shieldPopUpColor);
            Audio_Manager.instance.PlaySoundFXClip(shieldDamageClip, transform, 0.75f);
        }

        damagePopUpGenerator.CreatePopUp(transform.position + new Vector3(-0.5f, 0.0f, 0.0f), rawDamageToHealth, healthPopUpColor);
        damagePopUpGenerator.CreatePopUp(transform.position + new Vector3(0.5f, 0.0f, 0.0f), rawDamageToShield, shieldPopUpColor);

    }

    void Die()
    {
        isAlive = false;
        if (Random.Range(0, 2) % 2 == 0)
            XPOrb.GetComponent<SpriteRenderer>().flipX = true;
        else XPOrb.GetComponent<SpriteRenderer>().flipX = false;   
        Instantiate(XPOrb, transform.position, Quaternion.identity);

        Destroy(arrowPointer);
        enemyAnimations.DeathVFX();

        Destroy(gameObject);

    }

    IEnumerator Reshield()
    {
        while (shield < maxShield)
        {
            shield += maxShield * reshieldPercentage;
            yield return new WaitForSeconds(reshieldDuration);
        }
        hasToReshield = false;
        enemyAnimations.StopReshield();

    }
}
