using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Healthbar : MonoBehaviour
{
    Slider healthbar;
    [SerializeField] Transform parent;
    [SerializeField] Image shieldbar;
    [SerializeField] Vector3 offset;

    Player_Stats playerStats;
    Camera cam;


    void Start()
    {
        cam = Camera.main;
        healthbar = GetComponentInChildren<Slider>();
        playerStats = GetComponentInParent<Player_Stats>();
    }


    void Update()
    {
        healthbar.value = playerStats.health / playerStats.maxHealth;
        shieldbar.fillAmount = playerStats.shield / playerStats.maxShield;
        transform.rotation = cam.transform.rotation;

        transform.position = parent.position + offset;


    }
}
