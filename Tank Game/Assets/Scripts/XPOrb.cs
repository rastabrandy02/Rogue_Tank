using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XPOrb : MonoBehaviour
{
    
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

   
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            other.gameObject.GetComponent<Player_Stats>().currentXP += 0.5f;
            
        }
    }
}
