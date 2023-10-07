using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade_Engine : Upgrade
{
    public Upgrade_MaxSpeed maxSpeedUpgrade;
    public Upgrade_Acceleration accelerationUpgrade;

    public Upgrade_Engine()
    {
        type = UpgradeType.ENGINE;
        maxSpeedUpgrade = new Upgrade_MaxSpeed();
        accelerationUpgrade = new Upgrade_Acceleration();

        title = "ENGINE";
        statNameA = "Max Speed: ";
        statNameB = "Acceleration: ";
        valueChangeA = maxSpeedUpgrade.GetValueChange();
        valueChangeB = accelerationUpgrade.GetValueChange();

        isComposite = true;
    }

    public Upgrade_MaxSpeed GetMaxSpeedUpgrade()
    {
        return maxSpeedUpgrade;
    }
    public Upgrade_Acceleration GetAccelerationUpgrade()
    {
        return accelerationUpgrade;
    }

    
    
}
