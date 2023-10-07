using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XPOrb : MonoBehaviour
{
    [SerializeField] float XPRecieved;
    [SerializeField] float magnetDistance;
    [SerializeField] float magnetSpeed;
    [SerializeField] float maxTimeAlive;

    [SerializeField] AudioClip soundFX;
    GameObject player;

    Rigidbody2D rb;

    bool chasePlayer = false;

    float hitTime = 0.2f;
    float hitTimer;
    bool canHit = true;

    float timeAlive;
    
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
        if (distanceToPlayer <= magnetDistance) chasePlayer = true;
        if (chasePlayer)
        {
            rb.AddForce((player.transform.position - transform.position).normalized * magnetSpeed, ForceMode2D.Force);

            if (distanceToPlayer <= 0.5f) transform.position = player.transform.position;  
        }

        
        hitTimer += Time.deltaTime;
        timeAlive += Time.deltaTime;
        if (hitTimer > hitTime)
            canHit = true;

        if(timeAlive >= maxTimeAlive && chasePlayer)
        {
            chasePlayer = false;           
            transform.position += new Vector3(player.transform.position.x - transform.position.x,
                player.transform.position.y - transform.position.y, 0.0f).normalized * magnetSpeed * 2.5f * Time.deltaTime;
        }
    }

   
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && canHit)
        {
            Audio_Manager.instance.PlaySoundFXClip(soundFX, transform, 1.0f);
            canHit = false;
            Destroy(gameObject);
            other.gameObject.GetComponent<Player_Stats>().currentXP += XPRecieved;
            
        }
    }
}
