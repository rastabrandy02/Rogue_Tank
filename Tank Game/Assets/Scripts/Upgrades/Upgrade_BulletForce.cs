using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade_BulletForce : Upgrade
{
    
   public Upgrade_BulletForce()
   {
        type = UpgradeType.BULLET_FORCE;
        limitValue = 10.0f;
        valueChange = 1.0f;
   }


}
