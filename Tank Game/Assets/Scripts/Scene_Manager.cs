using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Scene_Manager : MonoBehaviour
{
    [SerializeField] Pause_UI pauseUI;
    [SerializeField] AudioClip titleSceneTheme;
    [SerializeField] AudioClip gameplaySceneTheme;
    [SerializeField] AudioClip gameOverSceneTheme;
    [SerializeField] AudioClip deactivatedSFX;

    Scene currentScene;

    void Awake()
    {
        
    }
    void Start()
    {        
        currentScene = SceneManager.GetActiveScene();
        if(pauseUI != null) pauseUI.LoadSettings();

        PlaySceneMusic();
    }

    
    void Update()
    {
        
    }

    public void LoadGameplayScene()
    {
        SceneManager.LoadSceneAsync("GameplayScene");
        SceneManager.UnloadSceneAsync(currentScene);
        PlaySceneMusic();
    }
    public void LoadDeathScene()
    {
        SceneManager.LoadSceneAsync("DeathScene");
        SceneManager.UnloadSceneAsync(currentScene);
        PlaySceneMusic();
    }
    public void LoadTitleScreen()
    {
        SceneManager.LoadSceneAsync("TitleScene");
        SceneManager.UnloadSceneAsync(currentScene);
        PlaySceneMusic();
    }
    public void QuitGame()
    {
        Application.Quit();
       
    }

    void PlaySceneMusic()
    {
        switch (currentScene.name)
        {
            case "GameplayScene":
                {
                    Audio_Manager.instance.PlayMusic(gameplaySceneTheme, 0.5f);
                    break;
                }
            case "DeathScene":
                {
                    Audio_Manager.instance.PlayMusic(gameOverSceneTheme, 0.5f);
                    Audio_Manager.instance.PlaySoundFXClip(deactivatedSFX, transform, 1.0f);
                    break;
                }
            case "TitleScene":
                {
                    Audio_Manager.instance.PlayMusic(titleSceneTheme, 0.5f);
                    break;
                }
            default: break;
        }
    }
}
