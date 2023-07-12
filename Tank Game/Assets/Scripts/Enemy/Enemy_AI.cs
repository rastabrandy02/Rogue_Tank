using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_AI : MonoBehaviour
{
    Enemy_Stats enemyStats;

    [SerializeField] LayerMask playerLayerMask;

    Transform target;
    float distanceToTarget;
    float nextShot;

    delegate void State();
    State movementState;
    State combatState;

    void Start()
    {
        enemyStats = GetComponent<Enemy_Stats>();      
        StartMyCorroutines();

        nextShot = Time.time + enemyStats.reloadTime;
        movementState = Idle;
        combatState = Idle;
    }

    void Update()
    {
        if (enemyStats.health <= 0)
        {
            movementState = Die;
           
        }
        if (target == null)
        {
            movementState = Idle;
            combatState = Idle;
            return;
        }
        else if (target != null)
        {            
            RotateTurret();                    
        }
       
        movementState();
        combatState();
    }
    void Idle()
    {
       
    }
    void Die()
    {
        Instantiate(enemyStats.XPOrb, transform.position, Quaternion.identity);
        Destroy(gameObject);
       
    }
    void StartMyCorroutines()
    {
        StartCoroutine(DetectionCorroutine());
        StartCoroutine(DirectionCorroutine());
        StartCoroutine(DistanceCorroutine());
        StartCoroutine(StateCorroutine());
       

    }
    IEnumerator DetectionCorroutine()
    {
        yield return new WaitForSeconds(0.5f);
        DetectTarget();
        StartCoroutine(DetectionCorroutine());
        
    }

    IEnumerator DirectionCorroutine()
    {
        yield return new WaitForSeconds(0.01f);
        
        if(target != null)
        {
            TurnToPlayer();
            
        }
        StartCoroutine(DirectionCorroutine());
    }

    IEnumerator DistanceCorroutine()
    {
        yield return new WaitForSeconds(0.1f);
        if(target != null)
        {
            CheckDistance();
            
        }
        StartCoroutine(DistanceCorroutine());
    }
    IEnumerator StateCorroutine()
    {
        yield return new WaitForSeconds(0.1f);
       
        if (target != null)
        {
            CheckState();
            
        }
        StartCoroutine(StateCorroutine());
    }
    void CheckState()
    {
        
        if (enemyStats.health <= 0)
        {
            movementState = Die;
            return;
        }
        if (distanceToTarget > enemyStats.maxDistanceToPlayer )
        {
            movementState = MoveTowards;
            combatState = Idle;
        }

        if(distanceToTarget < enemyStats.minDistanceToPlayer)
        {
            movementState = MoveAway;
        }
        if (distanceToTarget < enemyStats.maxDistanceToPlayer)
        {
            combatState = Attack;
        }
    }
    void CheckDistance()
    {
        distanceToTarget = Vector2.Distance(transform.position, target.position);
    }
    void DetectTarget()
    {
        if (target == null) CheckPlayerInRange();
        else if (target != null) CheckIfPlayerOutOfRange();

    }
    void CheckPlayerInRange()
    {
        Collider2D collision = Physics2D.OverlapCircle(transform.position, enemyStats.detectionRange, playerLayerMask);
        if (collision != null)
        {
            target = collision.transform;
        }
    }

    void CheckIfPlayerOutOfRange()
    {
        if (Vector2.Distance(transform.position, target.position) > enemyStats.detectionRange)
        {
            target = null;
        }
    }

    void RotateTurret()
    {
        Vector3 direction = target.position - enemyStats.turret.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90.0f;
        Quaternion finalRotation = Quaternion.RotateTowards(enemyStats.turret.rotation, Quaternion.Euler(0, 0, angle), enemyStats.turretRotationSpeed * Time.deltaTime);
        enemyStats.turret.rotation = finalRotation;
    }

    void TurnToPlayer()
    {
        Vector3 direction = target.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90.0f;
        Quaternion finalRotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, angle), enemyStats.turnSpeed * Time.deltaTime);
        enemyStats.rb.MoveRotation(finalRotation);        
    }

    void MoveTowards()
    {
        if (enemyStats.rb.velocity.magnitude < enemyStats.maxSpeed)
        {
            enemyStats.rb.AddForce(transform.up * enemyStats.acceleration, ForceMode2D.Force);
        }        
    }
    void MoveAway()
    {
        if (enemyStats.rb.velocity.magnitude < enemyStats.maxSpeed)
        {
            enemyStats.rb.AddForce(-transform.up * enemyStats.acceleration, ForceMode2D.Force);
        }        
    }

    void Attack()
    {
        if (Time.time > nextShot)
        {
            Shoot();
            Reload();
        }      
    }
    void Shoot()
    {
        Instantiate(enemyStats.enemyBullet, enemyStats.shootingPoint.position, enemyStats.turret.rotation);
    }
    void Reload()
    {
        nextShot = Time.time + enemyStats.reloadTime;
    }
    
}
