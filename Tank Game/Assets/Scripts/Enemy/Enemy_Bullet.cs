using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Bullet : MonoBehaviour
{
    [SerializeField] protected float force;
    [SerializeField] protected float lifespan;
    protected float baseDamage;

    [SerializeField] protected GameObject explosionFX;


    protected Rigidbody2D rb;
    protected float startTime;

    bool canHit = true;
    float hitTime = 0.25f;
    float hitTimer;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        SetForce();
        
        startTime = Time.time;
    }
    protected virtual void Update()
    {
        if(Time.time > startTime + lifespan)
        {
            Destroy(gameObject);
            Instantiate(explosionFX, transform.position, Quaternion.identity);
        }

        hitTimer += Time.deltaTime;
        if (hitTimer > hitTime)
            canHit = true;
    }
    void SetForce()
    {
        rb.AddForce(transform.up * force, ForceMode2D.Impulse);
    }
    public void SetDamage(float damage)
    {
        baseDamage = damage;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {

        if (other.gameObject.CompareTag("Player") && canHit)
        {
            canHit = false;
            foreach (ContactPoint2D hitPos in other.contacts)
            {
                float impactAngleMult;
                Vector3 targetOrientation = hitPos.collider.gameObject.transform.up;

                Vector3 orientation = transform.up;
                foreach (Collider2D col in other.gameObject.GetComponents<Collider2D>())
                {
                    if (other.collider == col)
                    {
                        if (other.collider.offset.x == 0) impactAngleMult = Mathf.Abs(Vector3.Dot(orientation, targetOrientation));
                        else impactAngleMult = 1.00f - Mathf.Abs(Vector3.Dot(orientation, targetOrientation));
                        other.gameObject.GetComponent<Player_Stats>().TakeDamage(baseDamage, impactAngleMult);
                    }

                }

            }

        }

        Instantiate(explosionFX, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
