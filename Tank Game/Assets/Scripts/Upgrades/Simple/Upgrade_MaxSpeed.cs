using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade_MaxSpeed : Upgrade
{
    
   public Upgrade_MaxSpeed()
   {
        type = UpgradeType.DEFAULT;
        limitValue = 25.0f;
        valueChangeA = 5.0f;
   }


}
