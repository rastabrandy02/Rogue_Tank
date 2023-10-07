using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade_MaxShield : Upgrade
{
    
   public Upgrade_MaxShield()
   {
        type = UpgradeType.MAX_SHIELD;
        limitValue = 2000.0f;
        valueChangeA = 340.0f;

        title = "BATTERY";
        statNameA = "Max Shield: ";
    }


}
