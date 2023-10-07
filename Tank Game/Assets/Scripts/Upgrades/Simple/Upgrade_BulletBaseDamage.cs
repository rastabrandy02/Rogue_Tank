using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade_BulletBaseDamage : Upgrade
{
    
   public Upgrade_BulletBaseDamage()
   {
        type = UpgradeType.DEFAULT;
        limitValue = 300f;
        valueChangeA = 70f;
   }


}
