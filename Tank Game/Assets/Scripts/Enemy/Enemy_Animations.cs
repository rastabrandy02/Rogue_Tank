using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Animations : MonoBehaviour
{
    [Header("Reshield")]
    [SerializeField] GameObject reshieldVFX;
    [SerializeField] float reshieldDistanceToCenter = 0.75f;
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

    [Header("Death")]
    [SerializeField] GameObject deathExplosionVFX;

    [Header("Flash")]
    [SerializeField] Material originalMaterial;
    [SerializeField]  Material flashMaterial;  

    [SerializeField]  float flashDuration;

    SpriteRenderer[] spriteRenderers;
    Coroutine flashRoutine;

    Enemy_Stats enemyStats;

    void Start()
    {
        enemyStats = GetComponent<Enemy_Stats>();
        spriteRenderers = GetComponentsInChildren<SpriteRenderer>();

        leftTrackAnimator = leftTrack.GetComponent<Animator>();
        rightTrackAnimator = rightTrack.GetComponent<Animator>();

        enemyStats.audioSource.clip = enemyStats.engineMovSFX;
        enemyStats.audioSource.volume = 0.2f;
        enemyStats.audioSource.Play();

        Audio_Manager.instance.PlaySoundFXClip(enemyStats.spawnSFX, transform, 1.0f);

        originalMaterial = new Material(originalMaterial);
        flashMaterial = new Material(flashMaterial);

        if (smokeRoutine != null) StopCoroutine(smokeRoutine);
        smokeRoutine = StartCoroutine(GenerateSmoke());
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
        for(int i = 0; i < spriteRenderers.Length;i++)
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
        while (enemyStats.isAlive)
        {
            float randomOffset = Random.Range(0.0f, 360.0f);

            Vector2 pos = transform.position;
            float rad = (2 * Mathf.PI) + (randomOffset * Mathf.Deg2Rad);
            pos.x += Random.Range(0.0f, reshieldDistanceToCenter) * Mathf.Cos(rad);
            pos.y += Random.Range(0.0f, reshieldDistanceToCenter) * Mathf.Sin(rad);
            Instantiate(reshieldVFX, pos, Quaternion.identity);

            yield return new WaitForSeconds(reshieldGenerationDuration);
        }
        StopCoroutine(reshieldRoutine);
    }
    IEnumerator GenerateSmoke()
    {
        while (enemyStats.isAlive)
        {
            Instantiate(smokeVFX, leftSmokeGenerator.transform.position, leftSmokeGenerator.transform.rotation);
            Instantiate(smokeVFX, rightSmokeGenerator.transform.position, rightSmokeGenerator.transform.rotation);
            yield return new WaitForSeconds(smokeGenerationDuration);
        }
        StopCoroutine(smokeRoutine);
    }
    public void DeathVFX()
    {
        Instantiate(deathExplosionVFX, transform.position, Quaternion.identity);
    }

}
