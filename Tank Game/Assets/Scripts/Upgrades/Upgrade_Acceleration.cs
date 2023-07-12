using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade_Acceleration : Upgrade
{
    
   public Upgrade_Acceleration()
   {
        type = UpgradeType.ACCELERATION;
        limitValue = 15f;
        valueChange = 2.5f;      
   }


}
