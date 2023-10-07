using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Death_VFX : MonoBehaviour
{
    GameObject player;
    Scene_Manager sceneManager;
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        sceneManager = player.GetComponent<Player_Stats>().sceneManager.GetComponent<Scene_Manager>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void DestroyPlayer()
    {
        player.SetActive(false);
    }
    public void LoadDeathScene()
    {
        sceneManager.LoadDeathScene();
    }
}
