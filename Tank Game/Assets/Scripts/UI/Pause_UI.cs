using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class Pause_UI : MonoBehaviour
{
    [SerializeField] GameObject gameManager;
    [SerializeField] GameObject sceneManager;
    [SerializeField] GameObject settingsMenu;
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] GameObject[] objectsToToggle;
    [SerializeField] Button backButton;
    [SerializeField] Toggle fullscreenToggle;
    [SerializeField] Toggle vSyncToggle;
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider soundFXSlider;

    [SerializeField] AudioClip showSettingsAudio;
    [SerializeField] AudioClip hideSettingsAudio;


    float musicVolume;
    float soundFXVolume;

    public bool showingSettings;

    SpriteRenderer[] spriteRenderers;
    void Start()
    {
       
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && showingSettings)
        {
            HideSettingsMenu();
        }
    }
    public void ResumeButton()
    {
        gameManager.GetComponent<Game_Manager_UI>().ResumeGame();
    }
    public void TitleScreenButton()
    {
        gameManager.GetComponent<Game_Manager_UI>().ResumeGame();
        sceneManager.GetComponent<Scene_Manager>().LoadTitleScreen();
    }
    public void ShowPlayerUI()
    {

        spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        for (int i = 0; i < spriteRenderers.Length; i++)
        {
            Color color = spriteRenderers[i].color;
            color.a = 255.0f;
            spriteRenderers[i].color = color;
        }

    }
    public void HidePlayerUI()
    {        
        spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        for (int i = 0; i < spriteRenderers.Length; i++)
        {
            Color color = spriteRenderers[i].color;
            color.a = 0.0f;
            spriteRenderers[i].color = color;
        }
    }

    public void ToggleFullScreen(bool value)
    {
        Screen.fullScreen = value;
    }
    public void ToggleVSync(bool value)
    {
        if(value) QualitySettings.vSyncCount = 1;
        else QualitySettings.vSyncCount = 0;

    }

    
    public void ShowSettingsMenu()
    {
        Audio_Manager.instance.PlaySoundFXClip(showSettingsAudio, transform, 1);

        for (int i = 0; i < objectsToToggle.Length; i++)
        {
            objectsToToggle[i].SetActive(false);
        }
        
        settingsMenu.SetActive(true);

        showingSettings = true;
    }
    public void HideSettingsMenu()
    {
        Audio_Manager.instance.PlaySoundFXClip(hideSettingsAudio, transform, 1);

        settingsMenu.SetActive(false);

        for (int i = 0; i < objectsToToggle.Length; i++)
        {
            objectsToToggle[i].SetActive(true);
        }

        showingSettings = false;
    }
    


    public void SetMusicVolume(float level)
    {
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(level) * 20.0f);
        musicVolume = level;
    }
    public void SetSoundFXVolume(float level)
    {
        audioMixer.SetFloat("SoundFXVolume", Mathf.Log10(level) * 20.0f);
        soundFXVolume = level;
    }
    public void SaveSettings()
    {       
        PlayerPrefs.SetInt("FullscreenPreference", Screen.fullScreen ? 0 : 1);
        PlayerPrefs.SetInt("VSyncPreference", QualitySettings.vSyncCount > 0 ? 0 : 1);
        PlayerPrefs.SetFloat("MusicVolumePreference", musicVolume);
        PlayerPrefs.SetFloat("SFXVolumePreference", soundFXVolume);
        

    }

    public void LoadSettings()
    {


        if (PlayerPrefs.HasKey("FullscreenPreference"))
                Screen.fullScreen = PlayerPrefs.GetInt("FullscreenPreference") != 0 ? false : true;
            else Screen.fullScreen = true;

            fullscreenToggle.isOn = Screen.fullScreen;


            if (PlayerPrefs.HasKey("VSyncPreference"))
            QualitySettings.vSyncCount = PlayerPrefs.GetInt("VSyncPreference") != 0 ? 0 : 1;
        else QualitySettings.vSyncCount = 1;

        vSyncToggle.isOn = QualitySettings.vSyncCount != 0;


        if (PlayerPrefs.HasKey("MusicVolumePreference"))
            musicSlider.value = PlayerPrefs.GetFloat("MusicVolumePreference");
        else musicSlider.value = 1;


        if (PlayerPrefs.HasKey("SFXVolumePreference"))
            soundFXSlider.value = PlayerPrefs.GetFloat("SFXVolumePreference");
        else soundFXSlider.value = 1;

        
    }
}
