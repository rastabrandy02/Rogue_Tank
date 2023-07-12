using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum UpgradeType
{
    MAX_SPEED,
    ACCELERATION,
    TURN_SPEED,
    TURRET_ROTATION_SPEED,

    MAX_HEALTH,
    MAX_SHIELD,
    RELOAD_TIME,

    BULLET_FORCE,
    BULLET_LIFESPAN,
    BULLET_BASE_DAMAGE

};
public class Upgrade 
{
    protected float limitValue;
    protected float valueChange;

    protected UpgradeType type;
    protected Upgrade()
    {
        limitValue = 0;
        valueChange = 0;
        
        
    }
    

    public void IncreaseStat(ref float stat)
    {
        if(stat + valueChange <= limitValue)  stat += valueChange;
        
        else stat =  limitValue;
    }

    public void DecreaseStat (ref float stat)
    {
        if (stat - valueChange >= limitValue) stat += valueChange;

        else stat = limitValue;
    }
}
