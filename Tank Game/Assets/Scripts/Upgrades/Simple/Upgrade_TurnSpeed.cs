using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade_TurnSpeed : Upgrade
{
    
   public Upgrade_TurnSpeed()
   {
        type = UpgradeType.TURN_SPEED;
        limitValue = 75.0f;
        valueChangeA = 16.7f;

        title = "TRACKS";
        statNameA = "Turn Speed: ";
    }


}
