using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade_BulletLifespan : Upgrade
{
    
   public Upgrade_BulletLifespan()
   {
        type = UpgradeType.BULLET_LIFESPAN;
        limitValue = 4.0f;
        valueChange = 0.5f;
   }


}
