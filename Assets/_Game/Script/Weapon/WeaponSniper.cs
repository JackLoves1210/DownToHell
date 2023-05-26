using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSniper : Weapon
{
    public override void OnRest()
    {
        base.OnRest();
        timeBettwenShoot = 1.2f;
        timeBulletAlive = 0.4f;
        size = 0;
    }
    public override void Shooting(Character character, Vector3 target)
    {
        base.Shooting(character, target);
        Bullet bullet = SimplePool.Spawn<Bullet>((PoolType)bulletType, character.TF.position, Quaternion.identity);
        bullet.damage = damageWeapon;
        bullet.timeAlive = timeBulletAlive;
        bullet.OnInit(character, target);
    }
}
