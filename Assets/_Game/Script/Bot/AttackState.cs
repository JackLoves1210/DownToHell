using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IState<Bot>
{
    public void OnEnter(Bot t)
    {
        
    }
    public void OnExecute(Bot t)
    {
        t.FollowTarget();
        if (t.isShootable && t.isCanATT)
        {
            t.fireCoolDown -= Time.deltaTime;
            if (t.fireCoolDown < 0)
            {
                t.fireCoolDown = t.timeBtwFire;
                //t.EnemyFireBullet();
                t.OnHit();
            }
        }
    }

    public void OnExit(Bot t)
    {

    }

}
