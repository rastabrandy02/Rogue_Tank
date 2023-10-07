using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Animations : MonoBehaviour
{
    [Header("Reshield")]
    [SerializeField] GameObject reshieldVFX;
    [SerializeField] float reshieldDistanceToPlayer = 0.75f;
    [SerializeField] float reshieldGenerationDuration = 0.5f;

    Coroutine reshieldRoutine;

    [Header("Tracks")]
    [SerializeField] GameObject leftTrack;
    [SerializeField] GameObject rightTrack;   
    Animator leftTrackAnimator;
    Animator rightTrackAnimator;

    [Header("Smoke")]
    [SerializeField] GameObject leftSmokeGenerator;
    [SerializeField] GameObject rightSmokeGenerator;
    [SerializeField] GameObject smokeVFX;
    [SerializeField] float smokeGenerationDuration;

    Coroutine smokeRoutine;

    [Header("Level Up")]
    [SerializeField] GameObject levelUpVFX;
   

    [Header("Death")]
    [SerializeField] GameObject deathExplosionVFX;

    [Header("Flash")]
    [SerializeField] Material originalMaterial;
    [SerializeField] Material flashMaterial;
    [SerializeField] float flashDuration;
    SpriteRenderer[] spriteRenderers;

    Coroutine flashRoutine;

    Player_Stats playerStats;

    void Start()
    {
        playerStats = GetComponent<Player_Stats>();
        spriteRenderers = GetComponentsInChildren<SpriteRenderer>();

        leftTrackAnimator = leftTrack.GetComponent<Animator>();
        rightTrackAnimator = rightTrack.GetComponent<Animator>();
       
        playerStats.audioSource.clip = playerStats.engineMovSFX;
        playerStats.audioSource.volume = 0.2f;
        playerStats.audioSource.Play();

        originalMaterial = new Material(originalMaterial);
        flashMaterial = new Material(flashMaterial);

        if (smokeRoutine != null) StopCoroutine(smokeRoutine);

        smokeRoutine = StartCoroutine(GenerateSmoke());

       
        
    }
    void Update()
    {
       
    }

    public void Forward()
    {
        leftTrackAnimator.SetInteger("Movement", 1);
        rightTrackAnimator.SetInteger("Movement", 1);

        
    }
    public void Backward()
    {
        leftTrackAnimator.SetInteger("Movement", -1);
        rightTrackAnimator.SetInteger("Movement", -1);

        
    }
    public void IdleTracks()
    {
        leftTrackAnimator.SetInteger("Movement", 0);
        rightTrackAnimator.SetInteger("Movement", 0);

        

    }
    public void Turn()
    {
        leftTrackAnimator.SetInteger("Movement", 1);
        rightTrackAnimator.SetInteger("Movement", 1);

        
    }
    public void ClockWise()
    {
        leftTrackAnimator.SetInteger("Movement", 1);
        rightTrackAnimator.SetInteger("Movement", -1);

        

    }
    public void CounterClockWise()
    {
        leftTrackAnimator.SetInteger("Movement", -1);
        rightTrackAnimator.SetInteger("Movement", 1);

        
    }


    public void Flash(Color color)
    {
        if (flashRoutine != null) StopCoroutine(flashRoutine);

        flashRoutine = StartCoroutine(FlashRoutine(color));
    }

    private IEnumerator FlashRoutine(Color color)
    {
        for (int i = 0; i < spriteRenderers.Length; i++)
        {
            spriteRenderers[i].material = flashMaterial;
        }

        flashMaterial.color = color;

        yield return new WaitForSeconds(flashDuration);

        for (int i = 0; i < spriteRenderers.Length; i++)
        {
            spriteRenderers[i].material = originalMaterial;
        }
        flashRoutine = null;
    }

    IEnumerator GenerateSmoke()
    {
        while(playerStats.isAlive)
        {
            Instantiate(smokeVFX, leftSmokeGenerator.transform.position, leftSmokeGenerator.transform.rotation);
            Instantiate(smokeVFX, rightSmokeGenerator.transform.position, rightSmokeGenerator.transform.rotation);
            yield return new WaitForSeconds(smokeGenerationDuration);
        }
        StopCoroutine(smokeRoutine);
    }
    public void StartReshield()
    {
        reshieldRoutine = StartCoroutine(GenerateReshieldEffect());
    }
    public void StopReshield()
    {
        if (reshieldRoutine != null)
            StopCoroutine(reshieldRoutine);
    }
    IEnumerator GenerateReshieldEffect()
    {
        while (playerStats.isAlive)
        {
            float randomOffset = Random.Range(0.0f, 360.0f);
            
            Vector2 pos = transform.position;
            float rad = (2 * Mathf.PI) + (randomOffset * Mathf.Deg2Rad);
            pos.x += Random.Range(0.0f, reshieldDistanceToPlayer) * Mathf.Cos(rad);
            pos.y += Random.Range(0.0f, reshieldDistanceToPlayer) * Mathf.Sin(rad);
            Instantiate(reshieldVFX, pos, Quaternion.identity);
            
            yield return new WaitForSeconds(reshieldGenerationDuration);
        }
        StopCoroutine(reshieldRoutine);
    }
    
    public void LevelUpAnimation()
    {
        
        Instantiate(levelUpVFX, transform.position, Quaternion.identity, transform);
        
    }
    
    public void DeathVFX()
    {
        
        Instantiate(deathExplosionVFX, transform.position, Quaternion.identity);
    }
}
