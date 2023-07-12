using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade_MaxSpeed : Upgrade
{
    
   public Upgrade_MaxSpeed()
   {
        type = UpgradeType.MAX_SPEED;
        limitValue = 20.0f;
        valueChange = 5.0f;
   }


}
