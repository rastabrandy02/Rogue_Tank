using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade_ReloadTime : Upgrade
{
    
   public Upgrade_ReloadTime()
   {
        type = UpgradeType.RELOAD_TIME;
        limitValue = 0.4f;
        valueChangeA = 0.2f;

        title = "LOAD SYSTEM";
        statNameA = "Reload Time: ";
    }


}
