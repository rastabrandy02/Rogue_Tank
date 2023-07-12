using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade_Turret_Rotation_Speed : Upgrade
{
    
   public Upgrade_Turret_Rotation_Speed()
   {
        type = UpgradeType.TURRET_ROTATION_SPEED;
        limitValue = 100.0f;
        valueChange = 15.0f;
   }


}
