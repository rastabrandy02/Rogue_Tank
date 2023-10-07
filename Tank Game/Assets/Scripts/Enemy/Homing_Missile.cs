using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Homing_Missile : Enemy_Bullet
{
    [SerializeField] float turnSpeed = 5.0f;
    [SerializeField] float maxSpeed = 4.0f;

    Transform target;
   
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startTime = Time.time;
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }
    protected override void Update()
    {
        base.Update();
        SetForce();
        Turn();
       
    }
    void SetForce()
    {
        
        if (rb.velocity.magnitude < maxSpeed)
        {
            Vector2 direction = new Vector2(target.position.x - transform.position.x, target.position.y - transform.position.y).normalized;
            rb.AddForce(direction * force, ForceMode2D.Force);
        }
        
    }
    void Turn()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90.0f;
        Quaternion finalRotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, angle), turnSpeed * Time.deltaTime);
        rb.MoveRotation(finalRotation);

        rb.velocity = direction * rb.velocity.magnitude;
    }
}
