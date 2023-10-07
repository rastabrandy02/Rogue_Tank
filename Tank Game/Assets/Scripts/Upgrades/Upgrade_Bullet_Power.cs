using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade_Bullet_Power : Upgrade
{
    public Upgrade_BulletBaseDamage baseDamageUpgrade;
    public Upgrade_BulletForce forceUpgrade;

    public Upgrade_Bullet_Power()
    {
        type = UpgradeType.BULLET_POWER;
        baseDamageUpgrade = new Upgrade_BulletBaseDamage();
        forceUpgrade = new Upgrade_BulletForce();

        title = "BULLET POWER";
        statNameA = "Base Dmg: ";
        statNameB = "Force: ";
        valueChangeA = baseDamageUpgrade.GetValueChange();
        valueChangeB = forceUpgrade.GetValueChange();

        isComposite = true;
    }

    public Upgrade_BulletBaseDamage GetBaseDamageUpgrade()
    {
        return baseDamageUpgrade;
    }
    public Upgrade_BulletForce GetForceUpgrade()
    {
        return forceUpgrade;
    }

}
