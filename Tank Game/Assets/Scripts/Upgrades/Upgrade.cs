using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum UpgradeType
{
    ENGINE,
    TURN_SPEED,
    TURRET_ROTATION_SPEED,

    MAX_HEALTH,
    MAX_SHIELD,
    RELOAD_TIME,

    BULLET_POWER,  
    BULLET_RANGE,   


    DEFAULT

};
public class Upgrade 
{
    protected float limitValue;
    protected float valueChangeA;
    protected float valueChangeB;

    protected UpgradeType type;

    int tier;
    int maxTier;

    protected string title;
    protected string statNameA;
    protected string statNameB;

    protected bool isComposite;
    
    public Upgrade()
    {
        
        type = UpgradeType.DEFAULT;
        limitValue = 0;
        valueChangeA = 0;
        valueChangeB = 0;
        tier = 0;
        maxTier = 3;
        title = "DEFAULT";
        statNameA = "default";
        statNameB = "default";
        isComposite = false;
        
    }

    
    

    public void IncreaseStat(ref float stat)
    {
        if (stat + valueChangeA <= limitValue)
        {
            stat += valueChangeA;
            TierUpUpgrade();
        }

        else
        {
            stat = limitValue;
            TierUpUpgrade();
        }
    }

    public void DecreaseStat (ref float stat)
    {
        if (stat - valueChangeA >= limitValue)
        {
            stat -= valueChangeA;
            TierUpUpgrade();
        }

        else
        {
            stat = limitValue;
            TierUpUpgrade();
        }
    }

    public void TierUpUpgrade()
    {
        if (tier < maxTier) tier++;
    }
    public int GetTier()
    {
        return tier;
    }
    public int GetMaxTier()
    {
        return maxTier;
    }
    public UpgradeType GetUpgradeType()
    {
        return type;
    }
    public float GetValueChange(int index = 0)
    {
        switch (index)
        {
            case 0: return valueChangeA;
            case 1: return valueChangeB;
            default: return 0.0f;
        }
    }
    public string GetTitle()
    {
        return title;
    }
    public string GetStatName(int index = 0)
    {
        switch(index)
        {
            case 0: return statNameA;                
            case 1: return statNameB;
            default: return ""; 
        }
    }
    public bool IsComposite()
    {
        return isComposite;
    }
}
