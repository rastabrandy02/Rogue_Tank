using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Start_Button : MonoBehaviour
{
   
    Button button;
   
    void Start()
    {
        button = GetComponent<Button>();
    }

    
    void Update()
    {
        
    }

    void LoadGameScene()
    {        
        SceneManager.LoadScene("GameplayScene");
    }
}
