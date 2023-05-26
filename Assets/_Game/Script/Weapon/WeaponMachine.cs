using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponMachine : Weapon
{
    public override void OnRest()
    {
        base.OnRest();
        timeBettwenShoot = 0.25f;
        timeBulletAlive = 0.3f;
        damageWeapon = 2.5f;
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
