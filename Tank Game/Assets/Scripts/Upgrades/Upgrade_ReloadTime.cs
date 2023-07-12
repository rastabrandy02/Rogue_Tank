using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade_ReloadTime : Upgrade
{
    
   public Upgrade_ReloadTime()
   {
        type = UpgradeType.RELOAD_TIME;
        limitValue = 0.4f;
        valueChange = 0.2f;
   }


}
