using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class XP_Bar : MonoBehaviour
{
    [SerializeField] Image XPBar;
    [SerializeField] TMP_Text XPBarText;
    Player_Stats playerStats;
    
    

    void Start()
    {
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Stats>();
    }

    // Update is called once per frame
    void Update()
    {
        XPBar.fillAmount = playerStats.XPInThisLevelCurrent / playerStats.XPInThisLevelTotal;
        
        XPBarText.text = "Lv. " + playerStats.currentLevel.ToString();
    }
}
