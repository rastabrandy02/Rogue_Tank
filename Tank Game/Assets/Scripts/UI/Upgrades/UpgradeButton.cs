using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeButton : MonoBehaviour
{
    [Header("Tier Sprites")]

    [SerializeField] Image buttonImage;

    [SerializeField] Sprite bronzeSprite;
    [SerializeField] Sprite silverSprite;
    [SerializeField] Sprite goldSprite;


    [Header("Text")]
    [SerializeField] GameObject statGroupB;

    [SerializeField] TMP_Text title;
    [SerializeField] TMP_Text statNameTextA;
    [SerializeField] TMP_Text statValueA;
    [SerializeField] TMP_Text upgradedStatA;

    [SerializeField] TMP_Text statNameTextB;
    [SerializeField] TMP_Text statValueB;
    [SerializeField] TMP_Text upgradedStatB;

    [Header("Icons")]
    [SerializeField] Image upgradeIcon;
    [SerializeField] Sprite engineIcon;
    [SerializeField] Sprite tracksIcon;
    [SerializeField] Sprite turretIcon;
    [SerializeField] Sprite armourPlatingIcon;
    [SerializeField] Sprite batteryIcon;
    [SerializeField] Sprite reloadSystemIcon;
    [SerializeField] Sprite bulletPowerIcon;
    [SerializeField] Sprite opticLensIcon;
    Game_Manager_UI gameManagerUI;
    Game_Manager_Leveling gameManagerLeveling;
    Player_Stats playerStats;

    Upgrade upgrade;

    float valueChangeA;
    float valueChangeB;

    bool isComposite;

    void Start()
    {
       
       
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ActivateButton()
    {
        gameManagerLeveling.LevelUpUpgrade(ref upgrade);
        gameManagerUI.DeactivateLevelUpUI();
    }

    public void SetParent( Upgrade_UI parent)
    {
        
        parent.GetRefGameManagerUI(ref gameManagerUI);
        parent.GetRefGameManagerLeveling(ref gameManagerLeveling);
        parent.GetRefGamePlayerStats(ref playerStats);

       
    }
    public void SetUpgrade(ref Upgrade upgrade)
    {
        this.upgrade = upgrade;
        isComposite = upgrade.IsComposite();
        SetButton();
    }
    public void SetButton()
    {
        
        switch (upgrade.GetTier())
        {
            case 0:
                {
                    buttonImage.sprite = bronzeSprite;
                } break;
            case 1:
                {
                    buttonImage.sprite = silverSprite;
                } break;
            case 2:
                {
                    buttonImage.sprite = goldSprite;
                } break;

                default: break;
        }

        GetStatText(upgrade);

        switch (upgrade.GetUpgradeType())
        {
            case UpgradeType.ENGINE:
                {                                      
                    statValueA.text = playerStats.maxSpeed.ToString();
                    upgradedStatA.text = (playerStats.maxSpeed + valueChangeA).ToString();
                                      
                    statValueB.text = playerStats.acceleration.ToString();
                    upgradedStatB.text = (playerStats.maxSpeed + valueChangeB).ToString();

                    upgradeIcon.sprite = engineIcon;
                } break;
            case UpgradeType.TURN_SPEED:
                {
                    statValueA.text = playerStats.turnSpeed.ToString();
                    upgradedStatA.text = (playerStats.turnSpeed + valueChangeA).ToString();

                    upgradeIcon.sprite = tracksIcon;
                }
                break;
            case UpgradeType.TURRET_ROTATION_SPEED:
                {
                    statValueA.text = playerStats.turretRotationSpeed.ToString();
                    upgradedStatA.text = (playerStats.turretRotationSpeed + valueChangeA).ToString();

                    upgradeIcon.sprite = turretIcon;
                }
                break;
            case UpgradeType.MAX_HEALTH:
                {
                    statValueA.text = playerStats.maxHealth.ToString();
                    upgradedStatA.text = (playerStats.maxHealth + valueChangeA).ToString();

                    upgradeIcon.sprite = armourPlatingIcon;
                }
                break;
            case UpgradeType.MAX_SHIELD:
                {
                    statValueA.text = playerStats.maxShield.ToString();
                    upgradedStatA.text = (playerStats.maxShield + valueChangeA).ToString();

                    upgradeIcon.sprite = batteryIcon;
                }
                break;
            case UpgradeType.RELOAD_TIME:
                {
                    statValueA.text = playerStats.reloadTime.ToString();
                    upgradedStatA.text = (playerStats.reloadTime - valueChangeA).ToString();

                    upgradeIcon.sprite = reloadSystemIcon;
                }
                break;
            case UpgradeType.BULLET_POWER:
                {
                    statValueA.text = playerStats.bulletBaseDamage.ToString();
                    upgradedStatA.text = (playerStats.bulletBaseDamage + valueChangeA).ToString();

                    statValueB.text = playerStats.bulletForce.ToString();
                    upgradedStatB.text = (playerStats.bulletForce + valueChangeB).ToString();

                    upgradeIcon.sprite = bulletPowerIcon;
                }
                break;
            case UpgradeType.BULLET_RANGE:
                {
                    statValueA.text = playerStats.bulletRange.ToString();
                    upgradedStatA.text = (playerStats.bulletRange + valueChangeA).ToString();

                    upgradeIcon.sprite = opticLensIcon;
                }
                break;          
            default: break;
        }
    }

    void GetStatText(Upgrade upgrade)
    {
        title.text = upgrade.GetTitle();
        statGroupB.SetActive(isComposite);

        statNameTextA.text = upgrade.GetStatName(0);
        valueChangeA = upgrade.GetValueChange(0);

        if (!isComposite) return;
        else
        {
            statNameTextB.text = upgrade.GetStatName(1);
            valueChangeB = upgrade.GetValueChange(1);
        }
    }

   
}
