using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade_MaxHealth : Upgrade
{
    
   public Upgrade_MaxHealth()
   {
        type = UpgradeType.MAX_HEALTH;
        limitValue = 2000.0f;
        valueChangeA = 340.0f;

        title = "ARMOR PLATING";
        statNameA = "Max Health: ";
    }


}
