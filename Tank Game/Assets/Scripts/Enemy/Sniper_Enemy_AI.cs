using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sniper_Enemy_AI : Enemy_AI
{
    [SerializeField] Transform secondShootingPoint;
    

    float shootDelay = 0.5f;
    bool shootingSide = false;
    
    protected override void Attack()
    {
        if (Time.time > nextAttack)
        {
            StartCoroutine(ShootRoutine(shootingSide, true));

            Reload();
        }

        IEnumerator ShootRoutine(bool side, bool repeat)
        {
           if(side)
           {
                Shoot(firstShootingPoint);
                yield return new WaitForSeconds(shootDelay);
                
           }
           else
           {
                Shoot(secondShootingPoint);
                yield return new WaitForSeconds(shootDelay);
                
           }

           if (repeat)
           {
                shootingSide = !shootingSide;
                StartCoroutine(ShootRoutine(shootingSide, false));
           }


        }
        
    }
    

   
}
