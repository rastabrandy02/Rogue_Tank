using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Game_Manager_Leveling : MonoBehaviour
{
    
    
    public GameObject player;

    Upgrade_Engine upgradeEngine;
    Upgrade_TurnSpeed upgradeTurnSpeed;
    Upgrade_Turret_Rotation_Speed upgradeTurretRotationSpeed;

    Upgrade_MaxHealth upgradeHealth;
    Upgrade_MaxShield upgradeMaxShield;
    Upgrade_ReloadTime upgradeReloadTime;

    Upgrade_Bullet_Power upgradeBulletPower;   
    Upgrade_BulletRange upgradeBulletRange;
   

    List<Upgrade> availableUpgrades;

    

    Game_Manager_UI managerUI;
    void Awake()
    {
        

        upgradeEngine = new Upgrade_Engine();
        upgradeTurnSpeed = new Upgrade_TurnSpeed();
        upgradeTurretRotationSpeed = new Upgrade_Turret_Rotation_Speed();

        upgradeHealth = new Upgrade_MaxHealth();
        upgradeMaxShield = new Upgrade_MaxShield();
        upgradeReloadTime = new Upgrade_ReloadTime();

        upgradeBulletPower = new Upgrade_Bullet_Power();       
        upgradeBulletRange = new Upgrade_BulletRange();
        


        availableUpgrades = new List<Upgrade>();

        availableUpgrades.Add(upgradeEngine);
        availableUpgrades.Add(upgradeTurnSpeed);
        availableUpgrades.Add(upgradeTurretRotationSpeed);

        availableUpgrades.Add(upgradeHealth);
        availableUpgrades.Add(upgradeMaxShield);
        availableUpgrades.Add(upgradeReloadTime);

        availableUpgrades.Add(upgradeBulletPower);
        availableUpgrades.Add(upgradeBulletRange);
        
    }
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        managerUI = GetComponent<Game_Manager_UI>();

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlayerLevelUp()
    {

        Upgrade upgradeOptionA = new Upgrade();
        Upgrade upgradeOptionB = new Upgrade();
        

        if (availableUpgrades.Count >= 2)
        {
            SearchUpgrade(ref upgradeOptionA);
            SearchUpgrade(ref upgradeOptionB);
            while (upgradeOptionA ==  upgradeOptionB)
            {
              SearchUpgrade(ref upgradeOptionA);
              SearchUpgrade(ref upgradeOptionB);
            }
                       
            managerUI.ActivateLevelUpUI(ref upgradeOptionA, ref upgradeOptionB);
        }
        else if(availableUpgrades.Count == 1)
        {
            SearchUpgrade(ref upgradeOptionA);
            managerUI.ActivateLevelUpUI(ref upgradeOptionA, ref upgradeOptionA);
        }
        
 
    }

    void SearchUpgrade(ref Upgrade upgrade)
    {
        if (availableUpgrades.Count == 0)
        {
            upgrade = null;
            return;
        }
 
        Upgrade listElement = availableUpgrades[Random.Range(0,availableUpgrades.Count)];

        if (listElement.GetTier() < listElement.GetMaxTier())
        {
            upgrade = listElement;           
            return;
        }
        else
        {           
            availableUpgrades.Remove(listElement);           
            SearchUpgrade(ref upgrade);
        }
    }

    
    public void LevelUpUpgrade(ref Upgrade upgrade)
    {
        Player_Stats playerStats = player.GetComponent<Player_Stats>();
        switch (upgrade.GetUpgradeType())
        {
            case UpgradeType.ENGINE:
                {
                    upgradeEngine.GetMaxSpeedUpgrade().IncreaseStat(ref playerStats.maxSpeed);
                    upgradeEngine.GetAccelerationUpgrade().IncreaseStat(ref playerStats.acceleration);
                    upgradeEngine.TierUpUpgrade();
                }
                break;
            case UpgradeType.TURN_SPEED: upgrade.IncreaseStat(ref playerStats.turnSpeed);

                break;
            case UpgradeType.TURRET_ROTATION_SPEED: upgrade.IncreaseStat(ref playerStats.turretRotationSpeed);

                break;
            case UpgradeType.MAX_HEALTH:
                {
                    float oldMaxHP = playerStats.maxHealth;
                    upgrade.IncreaseStat(ref playerStats.maxHealth);
                    playerStats.ReplenishHealthLevelUp(oldMaxHP);
                }

                break;
            case UpgradeType.MAX_SHIELD:
                {
                    float oldShield = playerStats.shield;
                    upgrade.IncreaseStat(ref playerStats.maxShield);
                    playerStats.ReplenishShieldLevelUp(oldShield);
                }
                
                break;
            case UpgradeType.RELOAD_TIME: upgrade.DecreaseStat(ref playerStats.reloadTime);
                
                break;
            case UpgradeType.BULLET_POWER:
                {
                    upgradeBulletPower.GetBaseDamageUpgrade().IncreaseStat(ref playerStats.bulletBaseDamage);
                    upgradeBulletPower.GetForceUpgrade().IncreaseStat(ref playerStats.bulletForce);
                    upgradeBulletPower.TierUpUpgrade();
                }
                
                break;
            case UpgradeType.BULLET_RANGE: upgrade.IncreaseStat(ref playerStats.bulletRange);
               
                break;
            default: break;
        }
        if (upgrade.GetTier() == upgrade.GetMaxTier()) availableUpgrades.Remove(upgrade);

    }
}
