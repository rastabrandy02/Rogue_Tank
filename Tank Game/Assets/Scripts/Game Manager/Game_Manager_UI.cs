using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Manager_UI : MonoBehaviour
{
    [SerializeField] GameObject chooseUpgradeUI;
    [SerializeField] GameObject pauseUI;
    [SerializeField] AudioClip pauseClip;
    [SerializeField] AudioClip resumeClip;
    [SerializeField] AudioClip showMenuClip;




    public bool isGamePaused;

    
    void Start()
    {
        isGamePaused = false;
        chooseUpgradeUI.GetComponent<Upgrade_UI>().InitUpgradeUI();
        
    }

    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && !pauseUI.GetComponent<Pause_UI>().showingSettings && !chooseUpgradeUI.activeInHierarchy)
        {
            if (isGamePaused) ResumeGame();
            else PauseGame();
        }

    }
    public void PauseGame()
    {
        isGamePaused = true;
        pauseUI.SetActive(true);
        Audio_Manager.instance.PlaySoundFXClip(pauseClip, transform, 1);
        Time.timeScale = 0f;
    }
    public void ResumeGame()
    {
        isGamePaused = false;
        pauseUI.SetActive(false);
        Audio_Manager.instance.PlaySoundFXClip(resumeClip, transform, 1);
        Time.timeScale = 1f;
    }
    public void ActivateLevelUpUI(ref Upgrade upgradeA, ref Upgrade upgradeB)
    {
        isGamePaused = true;
        chooseUpgradeUI.SetActive(true);
        Time.timeScale = 0f;

        chooseUpgradeUI.GetComponent<Upgrade_UI>().SetUpgradeButtons(ref upgradeA, ref upgradeB);

        Audio_Manager.instance.PlaySoundFXClip(showMenuClip, transform, 1);
    }

    public void DeactivateLevelUpUI()
    {
        Audio_Manager.instance.PlaySoundFXClip(resumeClip, transform, 1);
        isGamePaused = false;
        chooseUpgradeUI.SetActive(false);
        Time.timeScale = 1f;
        
    }
}
