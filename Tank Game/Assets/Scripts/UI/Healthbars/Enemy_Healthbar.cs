using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy_Healthbar : MonoBehaviour
{
   
    Slider healthbar;
    [SerializeField] Transform parent;
    [SerializeField] Image shieldbar;
    [SerializeField] Vector3 offset;

    Enemy_Stats enemyStats;
    Camera cam;
    

    void Start()
    {
        cam = Camera.main;
        healthbar = GetComponentInChildren<Slider>();
        enemyStats = GetComponentInParent<Enemy_Stats>();
    }

    
    void Update()
    {
        healthbar.value = enemyStats.health / enemyStats.maxHealth;
        shieldbar.fillAmount = enemyStats.shield / enemyStats.maxShield;
        transform.rotation = cam.transform.rotation;
        
        transform.position = parent.position + offset;


    }
}
