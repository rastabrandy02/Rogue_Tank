using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade_TurnSpeed : Upgrade
{
    
   public Upgrade_TurnSpeed()
   {
        type = UpgradeType.TURN_SPEED;
        limitValue = 75.0f;
        valueChange = 10.0f;
   }


}
