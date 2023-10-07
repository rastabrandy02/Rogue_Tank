using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_AI : MonoBehaviour
{
    [SerializeField] protected LayerMask playerLayerMask;

    [SerializeField] protected Transform firstShootingPoint;


    protected Enemy_Stats enemyStats;
    protected Enemy_Animations enemyAnimations;

    

    protected Transform target;
    protected float distanceToTarget;
    protected float nextAttack;

    protected delegate void State();
    protected State movementState;
    protected State combatState;

    protected virtual void Start()
    {
        enemyStats = GetComponent<Enemy_Stats>();      
        enemyAnimations = GetComponent<Enemy_Animations>();
        StartMyCorroutines();

        nextAttack = Time.time + enemyStats.reloadTime;
        movementState = Idle;
        combatState = Idle;

        
        
    }

    protected virtual void Update()
    {

        if (target == null)
        {
            combatState = Idle;
            return;
        }
        else if (target != null)
        {
            RotateTurret();
        }      
        combatState();
    }
    private void FixedUpdate()
    {
        if (target == null)
        {
            movementState = Idle;
            return;
        }
        movementState();
    }
    protected void Idle()
    {
       
    }

    protected void StartMyCorroutines()
    {
        StartCoroutine(DetectionCorroutine());
        StartCoroutine(DirectionCorroutine());
        StartCoroutine(DistanceCorroutine());
        StartCoroutine(StateCorroutine());
       

    }
    protected IEnumerator DetectionCorroutine()
    {
        yield return new WaitForSeconds(0.5f);
        DetectTarget();
        StartCoroutine(DetectionCorroutine());
        
    }

    protected IEnumerator DirectionCorroutine()
    {
        yield return new WaitForSeconds(0.01f);
        
        if(target != null)
        {
            TurnToPlayer();
            enemyAnimations.Turn();
        }
        StartCoroutine(DirectionCorroutine());
    }

    protected IEnumerator DistanceCorroutine()
    {
        yield return new WaitForSeconds(0.1f);
        if(target != null)
        {
            CheckDistance();
            
        }
        StartCoroutine(DistanceCorroutine());
    }
    protected IEnumerator StateCorroutine()
    {
        yield return new WaitForSeconds(0.1f);
       
        if (target != null)
        {
            CheckState();
            
        }
        StartCoroutine(StateCorroutine());
    }
    protected void CheckState()
    {
        
        if (enemyStats.health <= 0)
        {
            movementState = Idle;
            enemyAnimations.IdleTracks();
            return;
        }
        if (distanceToTarget > enemyStats.maxDistanceToPlayer )
        {
            movementState = MoveTowards;
            combatState = Idle;
            enemyAnimations.Forward();
        }

        if(distanceToTarget < enemyStats.minDistanceToPlayer)
        {
            movementState = MoveAway;
            enemyAnimations.Backward();
        }
        if (distanceToTarget < enemyStats.maxDistanceToPlayer)
        {
            combatState = Attack;
        }
    }
    protected void CheckDistance()
    {
        distanceToTarget = Vector2.Distance(transform.position, target.position);
    }
    protected void DetectTarget()
    {
        if (target == null) CheckPlayerInRange();
        else if (target != null) CheckIfPlayerOutOfRange();

    }
    protected void CheckPlayerInRange()
    {
        Collider2D collision = Physics2D.OverlapCircle(transform.position, enemyStats.detectionRange, playerLayerMask);
        if (collision != null)
        {
            target = collision.transform;
        }
    }

    protected void CheckIfPlayerOutOfRange()
    {
        if (Vector2.Distance(transform.position, target.position) > enemyStats.detectionRange)
        {
            target = null;
        }
    }

    protected void RotateTurret()
    {
        Vector3 direction = target.position - enemyStats.turret.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90.0f;
        Quaternion finalRotation = Quaternion.RotateTowards(enemyStats.turret.rotation, Quaternion.Euler(0, 0, angle), enemyStats.turretRotationSpeed * Time.deltaTime);
        enemyStats.turret.rotation = finalRotation;
    }

    protected void TurnToPlayer()
    {
        Vector3 direction = target.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90.0f;
        Quaternion finalRotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, angle), enemyStats.turnSpeed * Time.deltaTime);
        enemyStats.rb.MoveRotation(finalRotation);        
    }

    protected void MoveTowards()
    {
        if (enemyStats.rb.velocity.magnitude < enemyStats.maxSpeed)
        {
            enemyStats.rb.AddForce(transform.up * enemyStats.acceleration, ForceMode2D.Force);
        }        
    }
    protected void MoveAway()
    {
        if (enemyStats.rb.velocity.magnitude < enemyStats.maxSpeed)
        {
            enemyStats.rb.AddForce(-transform.up * enemyStats.acceleration, ForceMode2D.Force);
        }        
    }

    protected virtual void Attack()
    {
        if (Time.time > nextAttack)
        {
            Shoot(firstShootingPoint.transform);
            Reload();
        }      
    }
    protected void Shoot(Transform shootingPoint)
    {
        GameObject g = Instantiate(enemyStats.enemyBullet, shootingPoint.position, enemyStats.turret.rotation);
        g.GetComponent<Enemy_Bullet>().SetDamage(enemyStats.baseDamage);
        Audio_Manager.instance.PlaySoundFXClip(enemyStats.shootingSFX, transform, 0.5f);
    }
    protected void Reload()
    {
        nextAttack = Time.time + enemyStats.reloadTime;
    }
    
}
