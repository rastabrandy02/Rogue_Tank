using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade_BulletBaseDamage : Upgrade
{
    
   public Upgrade_BulletBaseDamage()
   {
        type = UpgradeType.BULLET_BASE_DAMAGE;
        limitValue = 3f;
        valueChange = 0.5f;
   }


}
