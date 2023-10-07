using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade_BulletRange : Upgrade
{
    
   public Upgrade_BulletRange()
   {
        type = UpgradeType.BULLET_RANGE;
        limitValue = 7.5f;
        valueChangeA = 1.5f;

        title = "OPTICS LENS";
        statNameA = "Range: ";
    }


}
