using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Bullet : MonoBehaviour
{
    [SerializeField]
    GameObject explosionFX;
     Player_Stats playerStats;
     Rigidbody2D rb;

     Vector3 startPos;

    bool canHit = true;
    float hitTime = 0.1f;
    float hitTimer;
    void Start()
    {
        playerStats = GameObject.FindWithTag("Player").GetComponent<Player_Stats>();
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(transform.up * playerStats.bulletForce, ForceMode2D.Impulse);
        startPos = transform.position;
       
    }
    void Update()
    {
        if(Vector3.Distance(startPos, transform.position) >= playerStats.bulletRange)
        {
            Instantiate(explosionFX, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

        hitTimer += Time.deltaTime;
        if (hitTimer > hitTime)
            canHit = true;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        
        if(other.gameObject.CompareTag("Enemy") && canHit)
        {
            foreach(ContactPoint2D hitPos in other.contacts)
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
                        other.gameObject.GetComponent<Enemy_Stats>().TakeDamage(playerStats.bulletBaseDamage, impactAngleMult);
                    }

                }
                
            }

        }

        Instantiate(explosionFX, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
