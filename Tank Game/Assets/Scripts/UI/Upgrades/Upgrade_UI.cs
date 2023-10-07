using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Upgrade_UI : MonoBehaviour
{
    [SerializeField] TMP_Text levelText;
    [SerializeField] GameObject buttonA;
    UpgradeButton buttonScriptA;
    [SerializeField] GameObject buttonB;
    UpgradeButton buttonScriptB;
    Upgrade upgradeA;
    Upgrade upgradeB;

    Game_Manager_UI gameManagerUI;
    Game_Manager_Leveling gameManagerLeveling;
    Player_Stats playerStats;

    
    void Start()
    {
       

       
    }

    
    void Update()
    {
        
    }
    public void InitUpgradeUI()
    {
       
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Stats>();
        gameManagerUI = playerStats.gameManager.GetComponent<Game_Manager_UI>();
        gameManagerLeveling = playerStats.gameManager.GetComponent<Game_Manager_Leveling>();
    }

    public void SetUpgradeButtons(ref Upgrade upgradeA, ref Upgrade upgradeB)
    {
        levelText.text = playerStats.currentLevel.ToString();
        buttonScriptA = buttonA.GetComponent<UpgradeButton>();
        buttonScriptA.SetParent( this);
        buttonScriptB = buttonB.GetComponent<UpgradeButton>();
        buttonScriptB.SetParent( this);

        buttonScriptA.SetUpgrade(ref upgradeA);
        buttonScriptB.SetUpgrade(ref upgradeB);

        this.upgradeA = upgradeA;
        this.upgradeB = upgradeB;

    }

    

    public void GetRefGameManagerUI(ref Game_Manager_UI gmUI)
    {
        gmUI =  gameManagerUI;
    }
    public void GetRefGameManagerLeveling(ref Game_Manager_Leveling gmL)
    {
       gmL=  gameManagerLeveling;
    }
    public void GetRefGamePlayerStats(ref Player_Stats stats)
    {
         stats = playerStats;
    }
}
