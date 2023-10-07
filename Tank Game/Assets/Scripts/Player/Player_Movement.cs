using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    Player_Stats playerStats;
    Player_Animations playerAnimations;

    Rigidbody2D rb;
    Camera cam;    
    Vector3 mousePos;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerStats = GetComponent<Player_Stats>();
        playerAnimations = GetComponent<Player_Animations>();
        cam = Camera.main;
        
    }
    void Update()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        RotateTurret();
    }

    void FixedUpdate()
    {
        
        
        if (Input.GetKey(KeyCode.W))
        {
            Move(transform.up);
            playerAnimations.Forward();
        }
        if (Input.GetKey(KeyCode.S))
        {
            Move(-transform.up);
            playerAnimations.Backward();
        }
                 
        

        if (Input.GetKey(KeyCode.D))
        {
            RotateBody(playerStats.turnSpeed);
            if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S)) playerAnimations.ClockWise();
            else playerAnimations.Turn();
        }
        if (Input.GetKey(KeyCode.A))
        {
            RotateBody(-playerStats.turnSpeed);
            if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S)) playerAnimations.CounterClockWise();
            else playerAnimations.Turn();
        }

        if(!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S) && 
            !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            playerAnimations.IdleTracks();
        }
    }

    void Move(Vector2 direction)
    {
        if (rb.velocity.magnitude < playerStats.maxSpeed)
        {
            rb.AddForce(direction * playerStats.acceleration, ForceMode2D.Force);

        }
    }
    
    void RotateBody(float speed)
    {
        rb.rotation -= speed * Time.fixedDeltaTime;
        
    }
    void RotateTurret()
    {
        
        Vector3 direction = mousePos - playerStats.turret.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90.0f;        
        Quaternion finalRotation = Quaternion.RotateTowards(playerStats.turret.rotation, Quaternion.Euler(0, 0, angle), playerStats.turretRotationSpeed * Time.deltaTime);
        playerStats.turret.rotation = finalRotation;
    }
}
