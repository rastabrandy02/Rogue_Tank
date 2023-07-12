using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade_MaxShield : Upgrade
{
    
   public Upgrade_MaxShield()
   {
        type = UpgradeType.MAX_SHIELD;
        limitValue = 200.0f;
        valueChange = 25.0f;
   }


}
