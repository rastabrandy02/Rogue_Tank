using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Bullet : MonoBehaviour
{
    [SerializeField] float force;
    [SerializeField] float lifespan;
    [SerializeField] float baseDamage;
    Rigidbody2D rb;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(transform.up * force, ForceMode2D.Impulse);
        Destroy(gameObject, lifespan);
    }
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
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

        Destroy(gameObject);
    }
}
