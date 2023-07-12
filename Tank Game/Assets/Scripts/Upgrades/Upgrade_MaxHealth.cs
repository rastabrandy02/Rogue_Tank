using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade_MaxHealth : Upgrade
{
    
   public Upgrade_MaxHealth()
   {
        type = UpgradeType.MAX_HEALTH;
        limitValue = 200.0f;
        valueChange = 25.0f;
   }


}
